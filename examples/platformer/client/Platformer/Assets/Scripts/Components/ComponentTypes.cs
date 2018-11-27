using Unity.Entities;
using Unity.Mathematics;

namespace Platformer {
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
}