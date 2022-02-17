using System.Collections.Generic;
using System.Linq;
using MyBox;
using Project.Interfaces;
using UnityEngine;

namespace Project.Scripts.Behaviours {
    public class Seek : DesiredVelocityProvider {
        public List<Vector2> objectsToFollow;
        [SerializeField, Range(0, 10)] private float arriveRadius;

        private void Awake() {
            var rabbits = FindObjectsOfType<RabbitController>().Select(rabbit => rabbit.transform).ToList();
            var does = FindObjectsOfType<DoeController>().Select(doe => doe.transform).ToList();
            var wolfs = FindObjectsOfType<WolfController>().Select(wolf => wolf.transform).ToList();
            objectsToFollow = rabbits.Union(does).Union(wolfs).Select(_transform => _transform.position.ToVector2()).ToList();
            base.Awake();
        }

        public override Vector2 GetDesiredVelocity() {
            // todo remove destroyed items
            // var objectToFollow = objectsToFollow.MinBy(obj => Vector3.Distance(obj.position, transform.position));
            var result = Vector2.zero;
            foreach (var objectToFollow in objectsToFollow) {
                var distance = (objectToFollow - transform.position.ToVector2());
                float k = 1;
                if (distance.magnitude < arriveRadius) {
                    k = distance.magnitude / arriveRadius;
                }

                result += distance.normalized * Animal.VelocityLimit * k;
            }

            return result;
        }
    }
}