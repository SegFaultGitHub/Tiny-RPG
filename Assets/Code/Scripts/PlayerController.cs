using UnityEngine;

namespace Assets.Code.Scripts {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float MovementSpeed;

        [SerializeField] private SpriteRenderer Sprite;
        [SerializeField] private Rigidbody2D Rigidbody;
        [SerializeField] private Vector2 MovementDirection;

        public void Start() {
            this.Rigidbody = this.GetComponentInChildren<Rigidbody2D>();
            this.Sprite = this.GetComponentInChildren<SpriteRenderer>();
        }

        public void Update() {
            
            this.MovementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (this.MovementDirection.x < 0) {
                this.Sprite.flipX = true;
            } else if (this.MovementDirection.x > 0) {
                this.Sprite.flipX = false;
            }
        }

        public void FixedUpdate() {
            this.Rigidbody.velocity = this.MovementDirection * this.MovementSpeed;
        }
    }
}