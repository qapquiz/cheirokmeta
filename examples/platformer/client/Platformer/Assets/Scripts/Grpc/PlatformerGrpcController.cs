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
        public int PlayerID;
        public AsyncDuplexStreamingCall<PlayerPositionById, StreamResponse> DuplexStream;
 
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
                    {"player-id", PlayerID.ToString()}
                };

                DuplexStream = Client.Stream(headers: metadata);

                var responseReaderTask = Task.Run(async () => {
                    while (await DuplexStream.ResponseStream.MoveNext()) {
                        var response = DuplexStream.ResponseStream.Current;

                        switch (response.EventCase) {
                            case StreamResponse.EventOneofCase.Player:
                                // player connected to server
                                var player = response.Player.Clone();

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
            await DuplexStream.RequestStream.CompleteAsync();
            DuplexStream.Dispose();
            _instance = null;
        }
    }
}
