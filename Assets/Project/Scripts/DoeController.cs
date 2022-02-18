using System.Collections.Generic;
using System.Linq;
using MyBox;
using Project.Classes;
using Project.Scripts.Behaviours;
using UnityEngine;

namespace Project.Scripts {
    public class DoeController : Animal {
        [SerializeField] private float wolfsDetectRadius;
        [SerializeField] private float hunterDetectRadius;
        [SerializeField] private List<DoeController> herd;
        
        protected void Update() {
            base.Update();
        }

        protected override void SetCurrentState() {
            var position = transform.position;
            
            GetWolves(position, out var wolfs);
            GetHunters(position, out var hunters);

            GetProviders(out var fleeProvider, out var seekProvider, out var wanderProvider);
            fleeProvider.objectsToFlee.Clear();
            seekProvider.objectsToFollow.Clear();
            if (wolfs.Count > 0 || hunters.Count > 0) {
                SetFleeingState(fleeProvider, wanderProvider, seekProvider, wolfs, hunters);
            }
            else {
                SetWanderingState(fleeProvider, seekProvider, wanderProvider);
            }
        }

        private void SetFleeingState(Flee fleeProvider, Wander wanderProvider, Seek seekProvider, List<Collider2D> wolfs, List<Collider2D> hunters) {
            State = states.Find(state => state.StateType == StateType.Fleeing);
            fleeProvider.Weight = fleeProvider.DefaultWeight;
            wanderProvider.Weight = 0;
            seekProvider.Weight = 0;
            if (wolfs.Count > 0) {
                fleeProvider.objectsToFlee.AddRange(wolfs.Select(wolf =>
                    wolf.transform.position.ToVector2()));
            }

            if (hunters.Count > 0) {
                fleeProvider.objectsToFlee.AddRange(hunters.Select(hunter =>
                    hunter.transform.position.ToVector2()));
            }
        }

        private void SetWanderingState(Flee fleeProvider, Seek seekProvider, Wander wanderProvider) {
            State = states.Find(state => state.StateType == StateType.Wandering);
            fleeProvider.Weight = fleeProvider.DefaultWeight;
            seekProvider.Weight = seekProvider.DefaultWeight;
            wanderProvider.Weight = wanderProvider.DefaultWeight;
            var centerOfHerd = herd.Select(doe => doe.transform.position.ToVector2()).ToList().Average();
            seekProvider.objectsToFollow.Add(centerOfHerd);
            var averageSpeed = herd.Select(doe => doe.Velocity).ToList().Average();
            seekProvider.objectsToFollow.Add(averageSpeed);
            fleeProvider.objectsToFlee.AddRange(herd.Except(this).Select(doe =>
                doe.transform.position.ToVector2()));
        }

        private void GetProviders(out Flee fleeProvider, out Seek seekProvider, out Wander wanderProvider) {
            var providers = states.Select(state => state.VelocityProvider).ToList();
            fleeProvider = (Flee) providers.Find(provider => provider is Flee);
            seekProvider = (Seek) providers.Find(provider => provider is Seek);
            wanderProvider = (Wander) providers.Find(provider => provider is Wander);
        }

        private void GetWolves(Vector3 position, out List<Collider2D> wolfs) {
            wolfs = new List<Collider2D>();
            var wolvesFilter = new ContactFilter2D().NoFilter();
            // wolvesFilter.SetLayerMask(LayerMask.GetMask("Animal"));
            Physics2D.OverlapCircle(position, wolfsDetectRadius, wolvesFilter, wolfs);
            wolfs = wolfs.Where(animal => animal.TryGetComponent<WolfController>(out _)).ToList();
        }

        private void GetHunters(Vector3 position, out List<Collider2D> hunters) {
            hunters = new List<Collider2D>();
            var huntersFilter = new ContactFilter2D().NoFilter();
            // huntersFilter.SetLayerMask(LayerMask.GetMask("Player"));
            Physics2D.OverlapCircle(position, hunterDetectRadius, huntersFilter, hunters);
            hunters = hunters.Where(hunter => hunter.TryGetComponent<PlayerController>(out _)).ToList();
        }
        
        public override void Die() {
            var otherDoes = herd.Except(this);
            for (var i = 0; i < otherDoes.Count; i++) {
                otherDoes[i].herd.Remove(this);
            }

            base.Die();
        }
    }
}