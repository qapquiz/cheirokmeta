using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
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

        public Button ConnectButton;

        public void SetupGameObjects() {
            ConnectButton = GameObject.Find("ConnectButton").GetComponent<Button>();
            ConnectButton.onClick.AddListener(() => {
                var connectResponse = PlatformerGrpcController.Instance.Connect(new ConnectRequest {
                    Name = "armariya"
                });

                CreateOtherPlayers(connectResponse);
            });
            ConnectButton.onClick.AddListener(PlatformerBootstrap.NewGame);
            ConnectButton.onClick.AddListener(() =>
            {
                PlatformerGrpcController.Instance.Stream();
            });
        }

        public void CreateOtherPlayers(ConnectResponse connectResponse) {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            foreach (var otherPlayerData in connectResponse.OtherPlayers)
            {
                Entity otherPlayer = entityManager.CreateEntity(PlatformerBootstrap.OtherPlayerArcheType);
                entityManager.SetComponentData(otherPlayer, new Position
                {
                    Value = new float3(
                        otherPlayerData.Position.X,
                        otherPlayerData.Position.Y,
                        0.0f
                    )
                });

                entityManager.AddSharedComponentData(otherPlayer, PlatformerBootstrap.OtherPlayerMeshRenderer);
            }
        }

        protected override void OnUpdate() {
            if (_players.Length > 0) {
                HideHud();
            } else {
                ShowHud();
            }
        }

        private void ShowHud() {
            if (ConnectButton != null && !ConnectButton.gameObject.activeSelf) {
                ConnectButton.gameObject.SetActive(true);
            }
        }

        private void HideHud() {
            ConnectButton?.gameObject.SetActive(false);
        }
    }
}
