using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Platformer {
    [UpdateAfter(typeof(PlayerMoveSystem))]
    public class SyncPositionSystem : ComponentSystem {

        struct Player {
            public readonly int Length;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<PlayerUpdatedPosition> UpdatedPositions;
        }

        [Inject] private Player _player;

        protected override void OnUpdate() {
            if (PlatformerGrpcController.Instance.DuplexStream == null) {
                return;
            }

            for (int i = 0; i < _player.Length; i++) {
                Position position = _player.Positions[i];
                PlayerUpdatedPosition updatedPosition = _player.UpdatedPositions[i];

                if (_compareFloat3(position.Value, updatedPosition.Value)) {
                    continue;
                }

                PlatformerGrpcController.Instance.DuplexStream.RequestStream.WriteAsync(new PlayerPositionById {
                    Id = PlatformerPlayerData.ID,
                    Position = new PlayerPosition {
                        X = _player.Positions[i].Value.x,
                        Y = _player.Positions[i].Value.y
                    }
                });

                _player.UpdatedPositions[i] = new PlayerUpdatedPosition {
                    Value = new float3(_player.Positions[i].Value.x, _player.Positions[i].Value.y, 0)
                };
            


                UnityEngine.Debug.Log("UPDATE POSITION!");
            }
        }

        private bool _compareFloat3(float3 firstPosition, float3 secondPosition)
        {
            bool3 compareTwoPositions = firstPosition == secondPosition;

            return compareTwoPositions.x && compareTwoPositions.y && compareTwoPositions.z;
        }
    }
}

