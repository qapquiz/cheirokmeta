using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Platformer {
    [Serializable]
    public struct PlayerUpdatedPosition : IComponentData {
        public float3 Value;
    }

    public class PlayerUpdatedPositionComponent : ComponentDataWrapper<PlayerUpdatedPosition> {}
}
