using UnityEngine;

namespace Assets.Code.Scripts.Camera {
    public class ScreenShakeController : MonoBehaviour {

        private float TimeRemaining;
        private float Power;

        public void Update() {
            //if (Input.GetKey(KeyCode.))
        }

        public void StartShake(float duration, float power) {
            this.TimeRemaining = duration;
            this.Power = power;
        }
    }
}