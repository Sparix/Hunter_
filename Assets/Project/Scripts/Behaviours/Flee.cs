using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public class Flee : DesiredVelocityProvider {
        [SerializeField] private Transform objectToFlee;

        public override Vector2 GetDesiredVelocity() {
            return -(objectToFlee.position.ToVector2() - transform.position.ToVector2()).normalized *
                   Animal.VelocityLimit;
        }
    }
}