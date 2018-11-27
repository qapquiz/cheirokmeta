using Unity.Entities;
using Unity.Transforms;

namespace Platformer {

    public class GrpcReceiverSystem : ComponentSystem {
        private struct Data {
            public readonly int Length;
            public ComponentDataArray<GrpcReceiverTag> Tags;
        }

        [Inject] private Data _data;

        protected override void OnUpdate() {
            if (
                PlatformerGrpcController.Instance.PlayerConnectedResponses.Length == 0 &&
                PlatformerGrpcController.Instance.PlayerPositionByIdResponses.Length == 0
            ) {
                return;
            }

            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            for (int i = 0; i < _data.Length; i++) {
                for (int index = 0; index < PlatformerGrpcController.Instance.PlayerConnectedResponses.Length; index++) {
                    var entity = entityManager.CreateEntity(PlatformerBootstrap.OtherPlayerArcheType);
                    entityManager.SetComponentData(entity, new OtherPlayerData {
                        ID = PlatformerGrpcController.Instance.PlayerConnectedResponses[index].ID
                    });
                    entityManager.SetComponentData(entity, new Position {
                        Value = PlatformerGrpcController.Instance.PlayerConnectedResponses[index].Position
                    });

                    entityManager.AddSharedComponentData(entity, PlatformerBootstrap.OtherPlayerMeshRenderer);
                }

                PlatformerGrpcController.Instance.PlayerConnectedResponses.Clear();
            }
        }
    }
}