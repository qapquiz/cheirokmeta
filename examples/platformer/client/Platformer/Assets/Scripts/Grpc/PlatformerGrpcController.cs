using Grpc.Core;

namespace Platformer {
    public class PlatformerGrpcController {

        private static PlatformerGrpcController _instance;
        public static PlatformerGrpcController Instance {
            get {
                if (_instance == null) {
                    _instance = new PlatformerGrpcController();
                }

                _instance.Connect("127.0.0.1:5050");

                return _instance;
            }
        }

        public Channel Channel;
        public Platformer.PlatformerClient Client;

        public void Connect(string url) {
            Channel = new Channel(url, ChannelCredentials.Insecure);
            Client = new Platformer.PlatformerClient(Channel);
        }

        public CreateRoomResponse CreateRoom(CreateRoomRequest request) {
            return Client.CreateRoom(request);
        }

        public JoinRoomResponse JoinRoom(JoinRoomRequest request) {
            return Client.JoinRoom(request);
        }


        public void Shutdown() {
            Channel.ShutdownAsync().Wait();
            _instance = null;
        }
    }
}
