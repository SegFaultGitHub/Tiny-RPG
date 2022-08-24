using UnityEngine;

namespace Assets.Code.Scripts.Player {
    public class AttackTarget : MonoBehaviour {
        public PlayerController Player;
        public GameObject Target;

        [SerializeField] private float Angle;

        public void Update() {
            this.Angle = Vector2.SignedAngle(new(1, 0), this.Player.TargetDirection);
            this.transform.position = this.Player.transform.position;
            this.transform.eulerAngles = new(0, 0, this.Angle);
            this.Target.transform.localEulerAngles = new(0, 0, -this.Angle);
        }
    }
}