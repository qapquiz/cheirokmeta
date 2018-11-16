using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Platformer {
    [Serializable]
    public struct PlayerInput : IComponentData {
        public float3 Move;
    }
    
    public class PlayerInputComponent : ComponentDataWrapper<PlayerInput> {}
}