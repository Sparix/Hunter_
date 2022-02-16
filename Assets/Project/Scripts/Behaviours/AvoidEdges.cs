namespace IntroToGameDev.Steering.Behaviors
{
    using UnityEngine;

    public class AvoidEdges : DesiredVelocityProvider {
        private Field _field; 
        private float edge = 0.05f;

        private void Awake() {
            _field = FindObjectOfType<Field>();
        }

        public override Vector2 GetDesiredVelocity()
        {
            var cam = Camera.current;
            var maxSpeed = Animal.VelocityLimit;
            var v = Animal.Velocity;
            if (cam == null)
            {
                return v;
            }
            
            var point = cam.WorldToViewportPoint(transform.position);

            if (point.x > 1 - edge)
            {
                return new Vector2(-maxSpeed, 0);
                
            }
            if (point.x < edge)
            {
                return new Vector2(maxSpeed, 0);
            }
            if (point.y > 1 - edge)
            {
                return new Vector2(0, -maxSpeed);
            }
            if (point.y < edge)
            {
                return new Vector2(0, maxSpeed);
            }

            return v;
        }
    }
}