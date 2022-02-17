using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public abstract class DesiredVelocityProvider : MonoBehaviour {
        [SerializeField, Range(0, 3)] private float defaultWeight = 1f;
        [SerializeField, ReadOnly] private float weight = 1f;

        public float Weight {
            get => weight;
            set => weight = value;
        }
        public float DefaultWeight => defaultWeight;

        protected Animal Animal;

        protected void Awake() {
            Animal = GetComponent<Animal>();
        }

        public abstract Vector2 GetDesiredVelocity();
    }
}