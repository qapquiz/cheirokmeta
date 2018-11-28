using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
using Platformer.Components;

namespace Platformer {
    public class SyncOtherPlayerPositionSystem : JobComponentSystem {
        struct OtherPlayer {
            public readonly int Length;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<OtherPlayerData> OtherPlayerDatas;

        }

        [Inject] private OtherPlayer _otherPlayer;

        private struct SyncOtherPlayerPositionJob : IJobParallelFor {
            public ComponentDataArray<Position> positions;
            [ReadOnly] public ComponentDataArray<OtherPlayerData> otherPlayerDatas;
           
            public void Execute(int index) {
                if (PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Length == 0) {
                    return;
                }

                int id = otherPlayerDatas[index].ID;
                if (PlatformerGrpcController.Instance.PlayerPositionByIdResponses.TryGetValue(id, out float3 position)) {
                    positions[index] = new Position {
                        Value = position
                    };

                    PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Remove(id);
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps) {
            return new SyncOtherPlayerPositionJob {
                positions = _otherPlayer.Positions,
                otherPlayerDatas = _otherPlayer.OtherPlayerDatas,
            }.Schedule(_otherPlayer.Length, 64, inputDeps);
        }
    }
}
