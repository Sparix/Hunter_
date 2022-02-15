using System;
using System.Collections.Generic;
using System.Linq;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class MovementController : MonoBehaviour {
        private const string HorAxisName = "Horizontal";
        private const string VerAxisName = "Vertical";
        [SerializeField] private float maxSpeed;
        [SerializeField] private Rigidbody2D rigidbody;
        private List<float> _horPotentials = new List<float>();
        private List<float> _verPotentials = new List<float>();

        private void Update() {
            HandleInput();
        }

        private void HandleInput() {
            _horPotentials.Add(Input.GetAxis(HorAxisName));
            _verPotentials.Add(Input.GetAxis(VerAxisName));
        }

        private void FixedUpdate() {
            Move();
            ClearPotentialsLists();
        }

        private void ClearPotentialsLists() {
            _horPotentials.Clear();
            _verPotentials.Clear();
        }

        private void Move() {
            var horSpeed = GetHorizontalSpeed();
            var verSpeed = GetVerticalSpeed();
            var deltaPos = new Vector2(horSpeed, verSpeed).Limit(maxSpeed) * Time.fixedDeltaTime;
            rigidbody.MovePosition(rigidbody.position + deltaPos);
        }

        private float GetHorizontalSpeed() => _horPotentials.Average() * maxSpeed;
        private float GetVerticalSpeed() => _verPotentials.Average() * maxSpeed;
    }
}
