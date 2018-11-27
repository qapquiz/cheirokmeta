using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Platformer {
    public sealed class PlatformerBootstrap {
        public static EntityArchetype PlayerArcheType;
        public static EntityArchetype OtherPlayerArcheType;
        public static EntityArchetype GrpcReceiverArcheType;

        public static MeshInstanceRenderer PlayerMeshRenderer;
        public static MeshInstanceRenderer OtherPlayerMeshRenderer;

        public static PlatformerSettings Settings;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize() {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            PlayerArcheType = entityManager.CreateArchetype(
                typeof(Position), typeof(PlayerInput), typeof(PlayerUpdatedPosition)
            );

            OtherPlayerArcheType = entityManager.CreateArchetype(
                typeof(Position), typeof(OtherPlayerData)
            );

            GrpcReceiverArcheType = entityManager.CreateArchetype(
                typeof(GrpcReceiverTag)
            );
        }

        public static void NewGame()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            Entity player = entityManager.CreateEntity(PlayerArcheType);

            entityManager.SetComponentData(player, new Position { Value = new float3(0.0f, 0.0f, 0.0f) });
            entityManager.SetComponentData(player, new PlayerInput {
                Move = new float3(0.0f, 0.0f, 0.0f)
            });
            entityManager.SetComponentData(player, new PlayerUpdatedPosition {
                Value = new float3(0.0f, 0.0f, 0.0f)
            });

            entityManager.AddSharedComponentData(player, PlayerMeshRenderer);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeAfterSceneLoad() {
            InitializeWithScene();
        }

        public static void InitializeWithScene() {
            var settingGO = GameObject.Find("Settings");
            Settings = settingGO?.GetComponent<PlatformerSettings>();

            PlayerMeshRenderer = GetPrototype("PlayerRenderPrototype");
            OtherPlayerMeshRenderer = GetPrototype("OtherPlayerRenderPrototype");

            World.Active.GetOrCreateManager<UpdatePlayerHud>().SetupGameObjects();
        }

        private static MeshInstanceRenderer GetPrototype(string protoName) {
            var proto = GameObject.Find(protoName);
            var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
            Object.Destroy(proto);
            return result;
        }
    }

}