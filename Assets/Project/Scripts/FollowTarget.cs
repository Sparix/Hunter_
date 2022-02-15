using MyBox;
using UnityEngine;

namespace Project.Scripts {
    public class FollowTarget : MonoBehaviour {
        [SerializeField] private Transform target;

        private void Update() {
            var position = target.position;
            transform.SetXY(position.x, position.y);
        }
    }
}