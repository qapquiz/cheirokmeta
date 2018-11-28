using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Platformer.Components;

namespace Platformer {
    public static class EntityFactory {
        public static void CreateOtherPlayer(PlayerData otherPlayerData) {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            Entity otherPlayer = entityManager.CreateEntity(PlatformerBootstrap.OtherPlayerArcheType);

            entityManager.SetComponentData(otherPlayer, new OtherPlayerData
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

        public static void CreateOtherPlayer(EntityCommandBuffer commandBuffer, PlayerData otherPlayerData) {
            commandBuffer.CreateEntity(PlatformerBootstrap.OtherPlayerArcheType);
            commandBuffer.SetComponent(new OtherPlayerData {
                ID = otherPlayerData.Id
            });
            commandBuffer.SetComponent(new Position {
                Value = new float3(
                    otherPlayerData.Position.X,
                    otherPlayerData.Position.Y,
                    0.0f
                )
            });
            commandBuffer.AddSharedComponent(PlatformerBootstrap.OtherPlayerMeshRenderer);
        }
    }
}
