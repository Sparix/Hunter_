using System.Collections;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class WeaponController : MonoBehaviour {
        private const float RayShowingTime = 0.02f;
        [SerializeField] private LineRenderer lineRenderer;
        private Weapon _weapon;
        private void Awake() {
            _weapon = new Weapon(3, 35, 5, 2, 0.5f, 20f);
        }

        private void Update() {
            HandleInput();
        }

        private void HandleInput() {
            if (Input.GetButton("Fire1")) {
                var position = transform.position;
                var shot =_weapon.TryShoot(position, transform.rotation, out var rays);
                if (shot == Weapon.ShotResult.ShotSuccessful) {
                    foreach (var ray in rays) {
                        StartCoroutine(AnimateShoot(position, ray.GetPoint(10)));
                    }
                }
            }
        }

        private IEnumerator AnimateShoot(Vector3 startPos, Vector3 finishPos) {
            var line = Instantiate(lineRenderer);
            line.SetPosition(0, startPos);
            line.SetPosition(1, finishPos);
            line.enabled = true;
            yield return new WaitForSeconds(RayShowingTime);
            line.enabled = false;
            Destroy(line.gameObject);
        }
    }
}
