using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Platformer {
    public class PlayerInputSystem : ComponentSystem
    {
        struct PlayerData {
            public readonly int Length;
            public ComponentDataArray<PlayerInput> Inputs;
        }

        [Inject] private PlayerData _player;

        protected override void OnUpdate() {
            float3 move = new float3 {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical"),
                z = 0.0f
            };

            for (int index = 0; index < _player.Length; index++) {
                UpdatePlayerInput(index, move);
            }
        }

        private void UpdatePlayerInput(int index, float3 move) {
            PlayerInput playerInput;

            playerInput.Move.x = move.x;
            playerInput.Move.y = move.y;
            playerInput.Move.z = move.z;

            _player.Inputs[index] = playerInput;
        }
    }
}