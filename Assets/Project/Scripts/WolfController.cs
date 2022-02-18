using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using Project.Classes;
using Project.Interfaces;
using Project.Scripts.Behaviours;
using UnityEngine;

namespace Project.Scripts {
    public class WolfController : Animal {
        [SerializeField] private float preyDetectRadius = 3f;
        [SerializeField] private float timeForDeathFromStarve = 60f;
        private float _timer;

        private void Awake() {
            _timer = timeForDeathFromStarve;
            base.Awake();
        }

        protected void Update() {
            _timer -= Time.deltaTime;
            if (_timer < 0) {
                Die();
            }
            base.Update();
        }

        protected override void SetCurrentState() {
            var results = new List<Collider2D>();
            Physics2D.OverlapCircle(transform.position, preyDetectRadius, new ContactFilter2D().NoFilter(), results);
            results.RemoveAll(result => result.TryGetComponent<WolfController>(out _));
            var providers = states.Select(state => state.VelocityProvider).ToList();
            if (results.Count > 0) {
                var objectToFollow = results.MinBy(obj => Vector3.Distance(obj.transform.position, transform.position));
                StateVelocityProvider(providers, StateType.Seeking, out var stateVelocityProvider);
                ((Seek) stateVelocityProvider).objectsToFollow =
                    new List<Vector2> {objectToFollow.transform.position.ToVector2()};
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

        private void OnCollisionEnter2D(Collision2D other) {
            if (!other.gameObject.TryGetComponent<CanBeKilledController>(out var controller)) return;
            if (controller is WolfController) {
                return;
            }
            
            Eat(controller);
        }

        private void Eat(ICanBeKilled canBeKilled) {
            canBeKilled.Die();
            Debug.Log("Yum yum");
            _timer = timeForDeathFromStarve;
        }
    }
}