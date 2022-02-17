using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public class AvoidEdges : DesiredVelocityProvider {
        [SerializeField] private float edge = 0.05f;
        private Field _field;

        private void Awake() {
            _field = FindObjectOfType<Field>();
        }

        public override Vector2 GetDesiredVelocity() {
            var maxSpeed = Animal.VelocityLimit;
            var point = _field.GetPositionInRelativeFormat(transform.position.ToVector2());
            var result = Vector2.zero;

            if (point.x > 1 - edge) {
                result += new Vector2(-maxSpeed, 0);
            }
            else if (point.x < edge) {
                result += new Vector2(maxSpeed, 0);
            }

            if (point.y > 1 - edge) {
                result += new Vector2(0, -maxSpeed);
            }
            else if (point.y < edge) {
                result += new Vector2(0, maxSpeed);
            }

            return result.magnitude == 0 ? Animal.Velocity : result.normalized * maxSpeed;
        }
    }
}