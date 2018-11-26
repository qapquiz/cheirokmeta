using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Platformer {
    public static class EntityFactory {
        public static void CreateOtherPlayer(PlayerData otherPlayerData) {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            Entity otherPlayer = entityManager.CreateEntity(PlatformerBootstrap.OtherPlayerArcheType);
            entityManager.SetComponentData(otherPlayer, new OtherPlayerTag
            {
                ID = otherPlayerData.Id
            });
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
}
