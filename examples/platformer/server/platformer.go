package main

import (
	context "context"
	"errors"
	"flag"
	"fmt"
	"log"
	"net"

	pb "github.com/qapquiz/cheirokmeta/examples/platformer/server/platformer"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"
)

type user struct {
	id int32
}

type room struct {
	id   int32
	user []user
}

type platformerServer struct {
	roomID int32
	rooms  map[int32]room
}

var (
	port = flag.Int("port", 5050, "The server port")
)

func (server *platformerServer) CreateRoom(ctx context.Context, createRoomRequest *pb.CreateRoomRequest) (*pb.CreateRoomResponse, error) {
	server.roomID = server.roomID + 1
	server.rooms[server.roomID] = room{server.roomID, []user{}}

	return &pb.CreateRoomResponse{RoomId: server.roomID}, nil
}

func (server *platformerServer) JoinRoom(ctx context.Context, joinRoomRequest *pb.JoinRoomRequest) (*pb.JoinRoomResponse, error) {
	if room, ok := server.rooms[joinRoomRequest.RoomId]; ok {
		room.user = append(room.user, user{joinRoomRequest.UserId})
	} else {
		return &pb.JoinRoomResponse{IsSuccess: false}, errors.New("Room not found.")
	}

	return &pb.JoinRoomResponse{IsSuccess: true}, nil
}

func (server *platformerServer) SyncPosition(stream pb.Platformer_SyncPositionServer) error {
	for {
		in, err := stream.Recv()

	}
	return nil
}

func main() {
	flag.Parse()
	listen, err := net.Listen("tcp", fmt.Sprintf("localhost:%d", *port))
	if err != nil {
		log.Fatalf("failed to listen: %v", err)
	}

	grpcServer := grpc.NewServer()
	pb.RegisterPlatformerServer(grpcServer, &platformerServer{})
	reflection.Register(grpcServer)
	if err := grpcServer.Serve(listen); err != nil {
		log.Fatalf("failed to server: %v", err)
	}
}
