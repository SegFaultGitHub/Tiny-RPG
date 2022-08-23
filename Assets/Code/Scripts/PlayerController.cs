using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Scripts {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float MovementSpeed;

        [SerializeField] private SpriteRenderer Sprite;
        [SerializeField] private Rigidbody2D Rigidbody;
        [SerializeField] private Vector2 MovementDirection;

        private PlayerActions PlayerActions;


        #region Input
        private void OnEnable() {
            this.PlayerActions = new PlayerActions();
            this.PlayerActions.PlayerControls.Enable();

            this.PlayerActions.PlayerControls.Move.performed += this.MoveInput;
            this.PlayerActions.PlayerControls.Move.canceled += this.MoveInput;
        }

        private void OnDisable() {
            this.PlayerActions.PlayerControls.Move.performed -= this.MoveInput;
            this.PlayerActions.PlayerControls.Move.canceled -= this.MoveInput;

            this.PlayerActions.PlayerControls.Disable();
        }

        private void MoveInput(InputAction.CallbackContext context) {
            this.Move(context.ReadValue<Vector2>());
        }
        #endregion

        public void Start() {
            this.Rigidbody = this.GetComponentInChildren<Rigidbody2D>();
            this.Sprite = this.GetComponentInChildren<SpriteRenderer>();
        }

        public void FixedUpdate() {
            this.Rigidbody.velocity = this.MovementDirection * this.MovementSpeed;
        }

        private void Move(Vector2 direction) {
            this.MovementDirection = direction;
            if (this.MovementDirection.x < 0) {
                this.transform.localScale = new(-1, this.transform.localScale.y, this.transform.localScale.z);
            } else if (this.MovementDirection.x > 0) {
                this.transform.localScale = new(1, this.transform.localScale.y, this.transform.localScale.z);
            }
        }
    }
}