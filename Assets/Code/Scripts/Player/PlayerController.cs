using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Scripts.Player {
    public class PlayerController : MonoBehaviour {
        private enum PlayerDirection {
            Left, Right
        }

        [SerializeField] private float MovementSpeed;

        [SerializeField] private SpriteRenderer Sprite;
        [SerializeField] private Rigidbody2D Rigidbody;
        [SerializeField] private Vector2 MovementDirection;

        private PlayerActions PlayerActions;
        private Animator Animator;
        private PlayerDirection Direction;

        #region Input
        private void OnEnable() {
            this.PlayerActions = new PlayerActions();
            this.PlayerActions.PlayerControls.Enable();

            this.PlayerActions.PlayerControls.Move.performed += this.MoveInput;
            this.PlayerActions.PlayerControls.Move.canceled += this.MoveInput;

            this.PlayerActions.PlayerControls.Attack.started += this.AttackInput;
        }

        private void OnDisable() {
            this.PlayerActions.PlayerControls.Move.performed -= this.MoveInput;
            this.PlayerActions.PlayerControls.Move.canceled -= this.MoveInput;

            this.PlayerActions.PlayerControls.Attack.started -= this.AttackInput;

            this.PlayerActions.PlayerControls.Disable();
        }

        private void MoveInput(InputAction.CallbackContext context) {
            this.Move(context.ReadValue<Vector2>());
        }

        private void AttackInput(InputAction.CallbackContext _) {
            this.Attack();
        }
        #endregion

        public void Start() {
            this.Animator = this.GetComponent<Animator>();
            this.Rigidbody = this.GetComponentInChildren<Rigidbody2D>();
            this.Sprite = this.GetComponentInChildren<SpriteRenderer>();

            if (this.transform.localScale.x > 0) {
                this.Direction = PlayerDirection.Right;
            } else if (this.transform.localScale.x < 0) {
                this.Direction = PlayerDirection.Left;
            }
        }

        public void FixedUpdate() {
            this.Rigidbody.velocity = this.MovementDirection * this.MovementSpeed;
        }

        private void Move(Vector2 direction) {
            this.MovementDirection = direction;
            if (this.MovementDirection.x < 0 && this.Direction == PlayerDirection.Right) {
                this.Direction = PlayerDirection.Left;
                this.Animator.SetBool("FlipLeft", true);
                this.Animator.SetBool("FlipRight", false);
            } else if (this.MovementDirection.x > 0 && this.Direction == PlayerDirection.Left) {
                this.Direction = PlayerDirection.Right;
                this.Animator.SetBool("FlipRight", true);
                this.Animator.SetBool("FlipLeft", false);
            }
            
            this.Animator.SetBool("IsMoving", direction != Vector2.zero);
        }

        private void Attack() {
            this.Animator.SetTrigger("Attack");
        }

        public void FlipLeft() {
            //this.transform.localScale = new(-1, this.transform.localScale.y, this.transform.localScale.z);
            this.Animator.SetBool("FlipRight", false);
            this.Animator.SetBool("FlipLeft", false);
        }

        public void FlipRight() {
            //this.transform.localScale = new(1, this.transform.localScale.y, this.transform.localScale.z);
            this.Animator.SetBool("FlipRight", false);
            this.Animator.SetBool("FlipLeft", false);
        }
    }
}