using UnityEngine;

namespace Project.Scripts.Behaviours {
    public abstract class DesiredVelocityProvider : MonoBehaviour {
        [SerializeField, Range(0, 3)] private float weight = 1f;

        public float Weight => weight;

        protected Animal Animal;

        private void Awake() {
            Animal = GetComponent<Animal>();
        }

        public abstract Vector2 GetDesiredVelocity();
    }
}