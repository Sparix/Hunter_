using UnityEngine;

namespace Project.Scripts {
    public class DoeController : Animal {
        [SerializeField] private float animalsDetectRadius;
        
        protected override void SetCurrentState() {
            State = states[0];
        }
    }
}