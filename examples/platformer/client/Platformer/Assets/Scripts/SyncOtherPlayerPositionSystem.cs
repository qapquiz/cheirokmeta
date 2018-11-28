using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

namespace Platformer {
    public class SyncOtherPlayerPositionSystem : JobComponentSystem {
        struct Data {
            public readonly int Length;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<OtherPlayerData> OtherPlayerDatas;

        }

        [Inject] private Data _data;

        private struct SyncOtherPlayerPositionJob : IJobParallelFor {
            public Data Data;
           
            public void Execute(int index) {
                if (PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Length == 0) {
                    return;
                }

                int id = Data.OtherPlayerDatas[index].ID;
                if (PlatformerGrpcController.Instance.PlayerPositionByIdResponses.TryGetValue(id, out float3 position)) {
                    Data.Positions[index] = new Position {
                        Value = position
                    };

                    PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Remove(id);
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps) {
            return new SyncOtherPlayerPositionJob {
                Data = _data
            }.Schedule(_data.Length, 64, inputDeps);
        }
    }
}
