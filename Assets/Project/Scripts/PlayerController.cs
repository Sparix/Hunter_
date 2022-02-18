using System.Collections;
using System.Threading.Tasks;
using Project.Classes;
using Project.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts {
    public class PlayerController : CanBeKilledController {
        protected void Update() {
            base.Update();
        }

        public override void Die() {
            // StartCoroutine(DeathCoroutine());
            DeathTask();
        }

        private async Task DeathTask() {
            gameObject.SetActive(false);
            await Task.Delay(1000);
            SceneManager.LoadScene(Consts.GAME_SCENE_PATH);
        }
        // private IEnumerator DeathCoroutine() {
        //     gameObject.SetActive(false);
        //     yield return null;
        //     SceneManager.LoadScene("Art/Scenes/Game");
        // }
    }
}