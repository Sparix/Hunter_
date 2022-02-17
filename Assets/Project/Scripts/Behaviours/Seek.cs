using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public class Seek : DesiredVelocityProvider {
        [SerializeField] private Transform objectToFollow;
        [SerializeField, Range(0, 10)] private float arriveRadius;

        public override Vector2 GetDesiredVelocity() {
            var distance = (objectToFollow.position.ToVector2() - transform.position.ToVector2());
            float k = 1;
            if (distance.magnitude < arriveRadius) {
                k = distance.magnitude / arriveRadius;
            }

            return distance.normalized * Animal.VelocityLimit * k;
        }
    }
}