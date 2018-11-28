using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Platformer.Components;

namespace Platformer {
    public class PlayerMoveSystem : ComponentSystem {
        
        struct Data {
            public readonly int Length;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<PlayerInput> Inputs;
            public ComponentDataArray<MoveSpeed> MoveSpeeds;
        }

        [Inject] private Data _data; 
        
        protected override void OnUpdate() {
            float dt = Time.deltaTime;
            float screenLeft = -9.5f;
            float screenRight = 9.5f;
            float screenBottom = -4.5f;
            float screenTop = 4.5f;

            for (int index = 0; index < _data.Length; index++) {
                var position = _data.Positions[index].Value;

                var playerInput = _data.Inputs[index];
                var moveSpeed = _data.MoveSpeeds[index];

                position += dt * playerInput.Move * moveSpeed.Value;

                if (position.x < screenLeft) {
                    position.x = screenLeft;
                }

                if (position.x > screenRight)
                {
                    position.x = screenRight;
                }

                if (position.y < screenBottom)
                {
                    position.y = screenBottom;
                }

                if (position.y > screenTop)
                {
                    position.y = screenTop;
                }

                _data.Positions[index] = new Position { Value = position };
            }
        }
    }
}