using UnityEngine;

namespace Project.Scripts {
    public class AimController : MonoBehaviour {
        [SerializeField] private float deadZoneLenght = 0.1f; 
        private Camera _camera;
        private float _pixToUnits;
        private float DeadZonePixelsLenght => deadZoneLenght * _pixToUnits;

        private void Awake() {
            _camera = Camera.main;
            _pixToUnits = FindPixelsToUnitsCoef();
        }

        private float FindPixelsToUnitsCoef() {
            return Display.main.renderingHeight / (_camera.orthographicSize * 2);
        }

        private void FixedUpdate() {
            Rotate();
        }

        private void Rotate() {
            var pos = _camera.WorldToScreenPoint(transform.position);
            var dir = Input.mousePosition - pos;
            if (dir.magnitude < DeadZonePixelsLenght) {
                return;
            }

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
