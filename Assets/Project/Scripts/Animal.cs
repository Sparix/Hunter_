using Project.Interfaces;
using UnityEngine;

namespace Project.Scripts {
    public abstract class Animal : MonoBehaviour, ICanBeKilled {
        public float VelocityLimit;
        public Vector2 Velocity;

        public void Die() {
            Debug.Log($"{name} killed");
            Destroy(gameObject);
        }
    }
}