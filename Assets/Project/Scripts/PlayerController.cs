using System.Collections;
using Project.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts {
    public class PlayerController : MonoBehaviour, ICanBeKilled {
        public void Death() {
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine() {
            gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Art/Scenes/Game");
        }
    }
}