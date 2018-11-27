using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Platformer {
    public class SyncOtherPlayerPositionSystem : ComponentSystem {

        struct OtherPlayer {
            public readonly int Length;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<OtherPlayerData> OtherPlayerDatas;
        }

        [Inject] private OtherPlayer _otherPlayer;

        protected override void OnUpdate() {
            if (PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Length == 0) {
                return;
            }

            for (int i = 0; i < _otherPlayer.Length; i++) {
                int id = _otherPlayer.OtherPlayerDatas[i].ID;

                if (PlatformerGrpcController.Instance.PlayerPositionByIdResponses.TryGetValue(id, out float3 position)) {
                    _otherPlayer.Positions[i] = new Position {
                        Value = position
                    };

                    PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Remove(id);
                }
                else {
                    continue;
                }
            }
        }
    }
}
