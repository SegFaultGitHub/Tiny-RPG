using System.Collections;
using UnityEngine;

namespace Assets.Code.Scripts.Player {
    public class CastTarget : MonoBehaviour {
        private Player Player;

        public void Start() {
            this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void FixedUpdate() {
            if (Vector2.Distance(this.Player.transform.position, this.transform.position) > 5) {
                this.transform.position = Vector3.ClampMagnitude(this.transform.position - this.Player.transform.position, 1) * 5 + this.Player.transform.position;
            }
        }
    }
}