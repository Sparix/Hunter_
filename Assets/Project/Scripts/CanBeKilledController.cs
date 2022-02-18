using System;
using Project.Interfaces;
using UnityEngine;

namespace Project.Scripts {
    public abstract class CanBeKilledController : MonoBehaviour, ICanBeKilled {
        private Field _field;

        protected void Awake() {
            _field = FindObjectOfType<Field>();
        }

        protected void Update() {
            if (IsOutOfField()) {
                Die();
            }
        }

        private bool IsOutOfField() {
            var relativePos = _field.GetPositionInRelativeFormat(transform.position);
            return relativePos.x < 0 || relativePos.x > 1 || relativePos.y < 0 || relativePos.y > 1;
        }

        public abstract void Die();
    }
}