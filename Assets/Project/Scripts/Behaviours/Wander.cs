using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    using Random = UnityEngine.Random;

    public class Wander : DesiredVelocityProvider {
        [SerializeField, Range(0.5f, 5)] private float circleDistance = 1;
        [SerializeField, Range(0.5f, 5)] private float circleRadius = 2;
        [SerializeField, Range(1, 80)] private int angleChangeStep = 15;

        private int _angle = 0;

        public override Vector2 GetDesiredVelocity() {
            var rnd = Random.value;
            if (rnd < 0.5) {
                _angle += angleChangeStep;
            }
            else if (rnd < 1) {
                _angle -= angleChangeStep;
            }

            var velocityNormalized = Animal.Velocity.normalized;
            var velocity = velocityNormalized.magnitude == 0 ? Animal.VelocityLimit * velocityNormalized : velocityNormalized;
            var futurePos = Animal.transform.position.ToVector2() + velocity * circleDistance;
            var vector = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad)) * circleRadius;

            return (futurePos + vector - transform.position.ToVector2()).normalized * Animal.VelocityLimit;
        }
    }
}