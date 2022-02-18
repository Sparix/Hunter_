using System;
using System.Collections.Generic;
using MyBox;
using Project.Interfaces;
using Project.Scripts.Behaviours;
using UnityEngine;

namespace Project.Scripts {
    public abstract class Animal : CanBeKilledController {
        [SerializeField] protected Rigidbody2D rigidbody;
        [SerializeField] protected Collider2D collider;
        [SerializeField] protected List<State> states;
        [SerializeField, Range(0.1f, 10f)] private float steeringForceLimit = 5;
        [SerializeField, ReadOnly] protected State State;
        public Vector2 Velocity => rigidbody.velocity;

        public float VelocityLimit => State?.VelocityLimit ?? 0;

        protected void Awake() {
            SetCurrentState();
            base.Awake();
        }

        protected void Update() {
            base.Update();
        }

        protected abstract void SetCurrentState();

        public override void Die() {
            Debug.Log($"{name} killed");
            Destroy(gameObject);
        }

        protected void FixedUpdate() {
            SetCurrentState();
            ApplySteeringForce();
        }

        private void ApplySteeringForce() {
            var providers = GetComponents<DesiredVelocityProvider>();
            var steering = Vector2.zero;
            foreach (var provider in providers) {
                var desiredVelocity = provider.GetDesiredVelocity() * provider.Weight;
                steering += desiredVelocity - Velocity;
            }

            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity + Vector2.ClampMagnitude(steering, steeringForceLimit), VelocityLimit);
        }
    }
}