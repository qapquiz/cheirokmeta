using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

namespace Platformer {
    public class PlatformerGrpcController {
        public struct EventPlayerConnected {
            public int ID;
            public float3 Position;
        }
        public struct EventPlayerPositionByID {
            public int ID;
            public float3 Position;
        }

        private static PlatformerGrpcController _instance;
        public static PlatformerGrpcController Instance {
            get {
                if (_instance == null) {
                    _instance = new PlatformerGrpcController();
                }

                _instance.ConnectToServer("68.183.225.171:5050");

                return _instance;
            }
        }

        public Channel Channel;
        public Platformer.PlatformerClient Client;
        public AsyncDuplexStreamingCall<PlayerPositionById, StreamResponse> DuplexStream;

        public NativeList<EventPlayerConnected> PlayerConnectedResponses = new NativeList<EventPlayerConnected>(Allocator.Persistent);
        public NativeHashMap<int, float3> PlayerPositionByIdResponses = new NativeHashMap<int, float3>(100, Allocator.Persistent);

        ~PlatformerGrpcController() {
            Shutdown();
        }

        public void ConnectToServer(string url) {
            Channel = new Channel(url, ChannelCredentials.Insecure);
            Client = new Platformer.PlatformerClient(Channel);
        }

        // RPC Services
        public ConnectResponse Connect(ConnectRequest request) {
            return Client.Connect(request);
        }

        public async void Stream() {
            try {
                var metadata = new Metadata {
                    {"player-id", PlatformerPlayerData.ID.ToString()}
                }; 

                DuplexStream = Client.Stream(headers: metadata);

                var responseReaderTask = Task.Run(async () => {
                    while (await DuplexStream.ResponseStream.MoveNext()) {
                        var response = DuplexStream.ResponseStream.Current;

                        switch (response.EventCase) {
                            case StreamResponse.EventOneofCase.Player:
                                // player connected to server
                                if (PlatformerPlayerData.ID != response.Player.Id) {
                                    PlayerConnectedResponses.Add(new EventPlayerConnected {
                                        ID = response.Player.Id,
                                        //Name = response.Player.Name,
                                        Position = new float3 {
                                            x = response.Player.Position.X,
                                            y = response.Player.Position.Y,
                                            z = 0.0f
                                        }
                                    });
                                }

                                break;
                            case StreamResponse.EventOneofCase.PlayerPositionById:
                                if (PlatformerPlayerData.ID != response.PlayerPositionById.Id) {
                                    PlayerPositionByIdResponses.TryAdd(
                                        key: response.PlayerPositionById.Id,
                                        item: new float3 {
                                            x = response.PlayerPositionById.Position.X,
                                            y = response.PlayerPositionById.Position.Y,
                                            z = 0.0f
                                        }
                                    );
                                }
                                break;
                        }
                    }
                });

                var en = World.Active.GetOrCreateManager<EntityManager>();
                en.CreateEntity(PlatformerBootstrap.GrpcReceiverArcheType);

                await responseReaderTask;
           } 
            catch (RpcException e) {
                UnityEngine.Debug.Log("RPC failed " + e);
            }
        }

        public async void Shutdown() {
            if (DuplexStream != null) {
                await DuplexStream.RequestStream.CompleteAsync();
                DuplexStream.Dispose();
            }

            Channel.ShutdownAsync().Wait();
            PlayerConnectedResponses.Dispose();
            PlayerPositionByIdResponses.Dispose();

            _instance = null;
        }
    }
}
