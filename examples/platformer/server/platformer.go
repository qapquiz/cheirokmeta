package main

import (
	context "context"
	"flag"
	"fmt"
	"io"
	"log"
	"net"
	"strconv"
	"sync"

	pb "github.com/qapquiz/cheirokmeta/examples/platformer/server/platformer"
	"google.golang.org/grpc"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/metadata"
	"google.golang.org/grpc/status"
)

type platformerServer struct {
	LatestPlayerID   int32
	PlayersMapWithID map[int32]*pb.PlayerData
	Players          []*pb.PlayerData

	Broadcast     chan pb.StreamResponse
	PlayerStreams map[int32]chan pb.StreamResponse

	playerStreamsMtx sync.RWMutex
}

var (
	port = flag.Int("port", 5050, "The server port")
)

func (s *platformerServer) Connect(ctx context.Context, connectRequest *pb.ConnectRequest) (*pb.ConnectResponse, error) {
	s.LatestPlayerID = s.LatestPlayerID + 1

	player := &pb.PlayerData{
		Id:   s.LatestPlayerID,
		Name: connectRequest.GetName(),
		Position: &pb.PlayerPosition{
			X: 0.0,
			Y: 0.0,
		},
	}

	s.Players = append(s.Players, player)
	s.PlayersMapWithID[s.LatestPlayerID] = player

	s.Broadcast <- pb.StreamResponse{
		Event: &pb.StreamResponse_Player{
			Player: &pb.StreamResponse_PlayerConnected{
				Id:   s.LatestPlayerID,
				Name: connectRequest.GetName(),
			},
		},
	}

	return &pb.ConnectResponse{
		Player:       player,
		IsSuccess:    true,
		OtherPlayers: s.Players,
	}, nil
}

func (s *platformerServer) Stream(streamServer pb.Platformer_StreamServer) error {
	playerIDHeader := "player-id"

	md, ok := metadata.FromIncomingContext(streamServer.Context())
	if !ok || len(md[playerIDHeader]) == 0 {
		log.Fatal("Cant get playerID from metadata")
	}

	playerIDString := md[playerIDHeader][0]
	playerID, err := strconv.ParseInt(playerIDString, 10, 32)

	if err != nil {
		log.Fatal("Cant convert PlayerID from string to int")
	}

	go s.sendBroadcastsFromServer(streamServer, int32(playerID))

	for {
		req, err := streamServer.Recv()
		if err == io.EOF {
			break
		} else if err != nil {
			return err
		}

		s.Broadcast <- pb.StreamResponse{
			Event: &pb.StreamResponse_PlayerPositionById{
				PlayerPositionById: &pb.PlayerPositionById{
					Id:       req.GetId(),
					Position: req.GetPosition(),
				},
			},
		}
	}

	<-streamServer.Context().Done()
	return streamServer.Context().Err()
}

func (s *platformerServer) systemBroadcast() {
	for res := range s.Broadcast {
		s.playerStreamsMtx.Lock()
		for _, stream := range s.PlayerStreams {
			select {
			case stream <- res:
				// no operation
			default:
				log.Printf("Client stream full!, dropping message")
			}
		}
		s.playerStreamsMtx.Unlock()
	}
}

func (s *platformerServer) sendBroadcastsFromServer(streamServer pb.Platformer_StreamServer, playerID int32) {
	stream := s.openStream(playerID)
	defer s.closeStream(playerID)

	for {
		select {
		case <-streamServer.Context().Done():
			return
		case res := <-stream:
			if s, ok := status.FromError(streamServer.Send(&res)); ok {
				switch s.Code() {
				case codes.OK:
					// no operation
				case codes.Unavailable, codes.Canceled, codes.DeadlineExceeded:
					log.Printf("Player id %d terminated connection", playerID)
				default:
					log.Printf("Failed to send to Player id: %d", playerID)
				}
			}
		}
	}
}

func (s *platformerServer) openStream(playerID int32) (stream chan pb.StreamResponse) {
	stream = make(chan pb.StreamResponse, 100)

	s.playerStreamsMtx.Lock()
	s.PlayerStreams[playerID] = stream
	s.playerStreamsMtx.Unlock()

	log.Printf("open stream for player id: %d", playerID)

	return
}

func (s *platformerServer) closeStream(playerID int32) {
	s.playerStreamsMtx.Lock()

	if stream, ok := s.PlayerStreams[playerID]; ok {
		delete(s.PlayerStreams, playerID)
		close(stream)
	}

	s.playerStreamsMtx.Unlock()

	log.Printf("close stream for player id: %d", playerID)
}

func main() {
	flag.Parse()
	listen, err := net.Listen("tcp", fmt.Sprintf("localhost:%d", *port))
	if err != nil {
		log.Fatalf("failed to listen: %v", err)
	}

	grpcServer := grpc.NewServer()

	server := &platformerServer{
		LatestPlayerID:   0,
		PlayersMapWithID: make(map[int32]*pb.PlayerData),
		Players:          []*pb.PlayerData{},

		Broadcast:     make(chan pb.StreamResponse, 1000),
		PlayerStreams: make(map[int32]chan pb.StreamResponse),
	}

	pb.RegisterPlatformerServer(grpcServer, server)

	go server.systemBroadcast()

	// reflection.Register(grpcServer)
	if err := grpcServer.Serve(listen); err != nil {
		log.Fatalf("failed to server: %v", err)
	}

}
