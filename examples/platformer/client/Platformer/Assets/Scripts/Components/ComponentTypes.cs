using Unity.Entities;
using Unity.Mathematics;

namespace Platformer.Components {
    public struct GrpcReceiverTag : IComponentData {}

    public struct OtherPlayerData : IComponentData {
        public int ID;
    }

    public struct PlayerInput : IComponentData {
        public float3 Move;
    }

    public struct PlayerUpdatedPosition : IComponentData {
        public float3 Value;
    }

    public struct MoveSpeed : IComponentData {
        public float Value;
    }
}