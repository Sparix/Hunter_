using MyBox;

namespace IntroToGameDev.Steering.Behaviors
{
    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Wander : DesiredVelocityProvider
    {
        [SerializeField, Range(0.5f, 5)]
        private float circleDistance = 1;
        
        [SerializeField, Range(0.5f, 5)]
        private float circleRadius = 2;
        
        [SerializeField, Range(1, 80)]
        private int angleChangeStep = 15;

        private int angle = 0;
        
        public override Vector2 GetDesiredVelocity()
        {
            var rnd = Random.value;
            if (rnd < 0.5)
            {
                angle+= angleChangeStep;
            } else if (rnd < 1)
            {
                angle-= angleChangeStep;
            }
            
            var futurePos = Animal.transform.position.ToVector2() + Animal.Velocity.normalized * circleDistance;
            var vector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * circleRadius;

            return (futurePos + vector - transform.position.ToVector2()).normalized * Animal.VelocityLimit;
        }
    }
}