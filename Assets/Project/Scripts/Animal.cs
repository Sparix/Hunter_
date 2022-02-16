using Project.Interfaces;
using UnityEngine;

namespace Project.Scripts {
    public class Animal : MonoBehaviour, ICanBeKilled {
        public void Death() {
            Debug.Log($"{name} killed");
            Destroy(gameObject);
        }
    }
}