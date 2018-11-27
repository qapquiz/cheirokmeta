using UnityEngine;

namespace Platformer {
    public class PlatformerManager : MonoBehaviour {
        void Start() {
            //PlatformerBootstrap.NewGame();
        }

        private void OnApplicationQuit() {
            PlatformerGrpcController.Instance.Shutdown();
        }
    }
}
