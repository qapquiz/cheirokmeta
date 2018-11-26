using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace Platformer {
    public class PlatformerGrpcController {

        private static PlatformerGrpcController _instance;
        public static PlatformerGrpcController Instance {
            get {
                if (_instance == null) {
                    _instance = new PlatformerGrpcController();
                }

                _instance.ConnectToServer("127.0.0.1:5050");

                return _instance;
            }
        }

        public Channel Channel;
        public Platformer.PlatformerClient Client;
        public AsyncDuplexStreamingCall<PlayerPositionById, StreamResponse> DuplexStream;

        ~PlatformerGrpcController() {
            UnityEngine.Debug.Log("YOYOYO");
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

        public async Task Stream() {
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
                                var player = response.Player.Clone();
                                UnityEngine.Debug.Log(player);
                                break;
                            case StreamResponse.EventOneofCase.PlayerPositionById:
                                break;
                        }
                    }
                });

                await responseReaderTask;
                
           } 
            catch (RpcException e) {
                UnityEngine.Debug.Log("RPC failed " + e);
            }
        }

        public async void Shutdown() {
            Channel.ShutdownAsync().Wait();

            if (DuplexStream != null) {
                await DuplexStream.RequestStream.CompleteAsync();
                DuplexStream.Dispose();
            }
            _instance = null;
        }
    }
}
