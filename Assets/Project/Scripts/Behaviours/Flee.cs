using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public class Flee : DesiredVelocityProvider {
        public List<Transform> objectsToFlee;

        public override Vector2 GetDesiredVelocity() {
            // todo remove destroyed items
            var result = Vector2.zero;
            foreach (var objectToFlee in objectsToFlee) {
                result += -(objectToFlee.position.ToVector2() - transform.position.ToVector2()).normalized *
                       Animal.VelocityLimit;
            }

            return result.normalized * Animal.VelocityLimit;
        }
    }
}