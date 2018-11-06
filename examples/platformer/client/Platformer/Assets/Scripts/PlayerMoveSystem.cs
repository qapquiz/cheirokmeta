using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace Platformer {
    public class PlayerMoveSystem : ComponentSystem {
        
        struct Data {
            public readonly int Length;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<PlayerInput> Inputs;
        }

        [Inject] private Data _data; 
        
        protected override void OnUpdate() {
            var settings = PlatformerManager.Settings;

            float dt = Time.deltaTime;

            for (int index = 0; index < _data.Length; index++) {
                var position = _data.Positions[index].Value;

                var playerInput = _data.Inputs[index];

                position += dt * playerInput.Move * settings.PlayerMoveSpeed;

                _data.Positions[index] = new Position { Value = position };
            }
        }
    }
}