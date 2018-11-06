using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class PlatformerManager : MonoBehaviour {
        public static PlatformerSettings Settings { get; private set;}

        [SerializeField] private PlatformerSettings settings;

        void Awake() {
            Settings = settings;
        }
    }
}