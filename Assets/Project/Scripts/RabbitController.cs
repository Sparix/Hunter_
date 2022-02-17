using System.Collections.Generic;
using System.Linq;
using MyBox;
using Project.Classes;
using Project.Scripts.Behaviours;
using UnityEngine;

namespace Project.Scripts {
    public class RabbitController : Animal {
        [SerializeField] private float animalsDetectRadius;

        protected override void SetCurrentState() {
            var results = new List<Collider2D>();
            Physics2D.OverlapCircle(transform.position, animalsDetectRadius, new ContactFilter2D().NoFilter(), results);
            results.Remove(collider);
            var providers = states.Select(state => state.VelocityProvider).ToList();
            if (results.Count > 0) {
                StateVelocityProvider(providers, StateType.Fleeing, out var stateVelocityProvider);
                ((Flee) stateVelocityProvider).objectsToFlee = results.Select(obj => obj.transform).ToList();
            }
            else {
                StateVelocityProvider(providers, StateType.Wandering, out _);
            }
        }

        private void StateVelocityProvider(List<DesiredVelocityProvider> providers, StateType type,
            out DesiredVelocityProvider stateVelocityProvider) {
            State = states.Find(state => state.StateType == type);
            stateVelocityProvider = State.VelocityProvider;
            stateVelocityProvider.Weight = stateVelocityProvider.DefaultWeight;
            providers.Except(stateVelocityProvider).ForEach(provider => provider.Weight = 0);
        }
    }
}