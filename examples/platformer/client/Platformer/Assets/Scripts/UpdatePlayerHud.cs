using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer {
    [AlwaysUpdateSystem]
    public class UpdatePlayerHud : ComponentSystem {
        public struct PlayerData {
            public readonly int Length;
            [ReadOnly] public EntityArray Entity;
            [ReadOnly] public ComponentDataArray<PlayerInput> Player;
        }

        [Inject] PlayerData _players;

        public Button CreateRoomButton;

        public void SetupGameObjects() {
            CreateRoomButton = GameObject.Find("CreateRoomButton").GetComponent<Button>();
            CreateRoomButton.onClick.AddListener(() => {
                var response =  PlatformerGrpcController.Instance.CreateRoom(new CreateRoomRequest {});
                PlatformerGrpcController.Instance.JoinRoom(new JoinRoomRequest {
                    PlayerId = 1,
                    RoomId = response.RoomId
                });
            });
            CreateRoomButton.onClick.AddListener(PlatformerBootstrap.NewGame);
        }

        protected override void OnUpdate() {
            if (_players.Length > 0) {
                HideHud();
            } else {
                ShowHud();
            }
        }

        private void ShowHud() {
            if (CreateRoomButton != null && !CreateRoomButton.gameObject.activeSelf) {
                CreateRoomButton.gameObject.SetActive(true);
            }
        }

        private void HideHud() {
            CreateRoomButton?.gameObject.SetActive(false);
        }
    }
}
