using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public class Flee : DesiredVelocityProvider {
        public List<Vector2> objectsToFlee;

        public override Vector2 GetDesiredVelocity() {
            // todo remove destroyed items
            var result = Vector2.zero;
            foreach (var objectToFlee in objectsToFlee) {
                result += -(objectToFlee - transform.position.ToVector2()).normalized *
                       Animal.VelocityLimit;
            }

            return result.normalized * Animal.VelocityLimit;
        }
    }
}