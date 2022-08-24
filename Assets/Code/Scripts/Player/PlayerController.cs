using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Scripts.Player {
    public class PlayerController : MonoBehaviour {
        private enum PlayerDirection {
            Left, Right
        }

        public float AttackDelay;
        public Vector2 TargetDirection;

        [SerializeField] private float MovementSpeed;
        [SerializeField] private Rigidbody2D Rigidbody;
        [SerializeField] private Vector2 MovementDirection;

        private float LastAttack = float.MinValue;
        private PlayerActions PlayerActions;
        private Animator Animator;
        private PlayerDirection Direction;

        #region Input
        private void OnEnable() {
            this.PlayerActions = new PlayerActions();
            this.PlayerActions.PlayerControls.Enable();

            this.PlayerActions.PlayerControls.Move.performed += this.MoveInput;
            this.PlayerActions.PlayerControls.Aim.performed += this.SetTargetDirectionInput;
            this.PlayerActions.PlayerControls.Move.canceled += this.MoveInput;

            this.PlayerActions.PlayerControls.Attack.started += this.AttackInput;
        }

        private void OnDisable() {
            this.PlayerActions.PlayerControls.Move.performed -= this.MoveInput;
            this.PlayerActions.PlayerControls.Aim.performed += this.SetTargetDirectionInput;
            this.PlayerActions.PlayerControls.Move.canceled -= this.MoveInput;

            this.PlayerActions.PlayerControls.Attack.started -= this.AttackInput;

            this.PlayerActions.PlayerControls.Disable();
        }

        private void MoveInput(InputAction.CallbackContext context) {
            this.Move(context.ReadValue<Vector2>());
        }

        private void SetTargetDirectionInput(InputAction.CallbackContext context) {
            this.SetTargetDirection(context.ReadValue<Vector2>());
        }

        private void AttackInput(InputAction.CallbackContext _) {
            this.Attack();
        }
        #endregion

        public void Start() {
            this.Animator = this.GetComponent<Animator>();
            this.Rigidbody = this.GetComponentInChildren<Rigidbody2D>();
            this.TargetDirection = new(1, 0);

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
            
            this.Animator.SetBool("IsMoving", direction != Vector2.zero);
        }

        private void SetTargetDirection(Vector2 direction) {
            this.TargetDirection = direction;
            if (this.TargetDirection.x < 0 && this.Direction == PlayerDirection.Right && !this.Animator.GetBool("Flipping")) {
                this.Direction = PlayerDirection.Left;
                this.Animator.SetBool("Flipping", true);
            } else if (this.TargetDirection.x > 0 && this.Direction == PlayerDirection.Left && !this.Animator.GetBool("Flipping")) {
                this.Direction = PlayerDirection.Right;
                this.Animator.SetBool("Flipping", true);
            }
        }

        private void Attack() {
            float now = Time.time;
            if (now - this.LastAttack > this.AttackDelay) {
                this.Animator.SetTrigger("Attack");
                this.LastAttack = now;
            }
        }

        public void Flip() {
            this.transform.localScale = new(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            this.Animator.SetBool("Flipping", false);
        }
    }
}