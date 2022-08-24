using UnityEngine;

namespace Assets.Code.Scripts.Camera {
    public class CameraController : MonoBehaviour {
        public static CameraController Instance;

        public Transform Target;
        private float StartY;

        private void Awake() {
            Instance = this;
        }

        public void Start() {
            this.StartY = this.transform.position.y;
        }

        public void Update() {
            if (this.Target != null) {
                this.transform.position = new(this.Target.position.x, this.Target.position.y, this.transform.position.z);
            }
        }
    }
}