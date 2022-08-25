using System;
using System.Collections;
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
        private Rigidbody2D Rigidbody;
        private Vector2 MovementDirection;

        private float LastAttack = float.MinValue;
        private PlayerActions PlayerActions;
        private Animator Animator;
        private PlayerDirection Direction;

        private Player Player;

        private AttackTarget AttackTarget;
        private CastTarget CastTarget;

        #region Input
        private void OnEnable() {
            this.PlayerActions = new PlayerActions();
            this.PlayerActions.PlayerControls.Enable();

            this.PlayerActions.PlayerControls.Move.performed += this.MoveInput;
            this.PlayerActions.PlayerControls.Aim.performed += this.SetTargetDirectionInput;
            this.PlayerActions.PlayerControls.Move.canceled += this.MoveInput;

            this.PlayerActions.PlayerControls.Attack.started += this.AttackInput;
            this.PlayerActions.PlayerControls.Cast.started += this.CastStartInput;
            this.PlayerActions.PlayerControls.Cast.canceled += this.CastEndInput;
        }

        private void OnDisable() {
            this.PlayerActions.PlayerControls.Move.performed -= this.MoveInput;
            this.PlayerActions.PlayerControls.Aim.performed -= this.SetTargetDirectionInput;
            this.PlayerActions.PlayerControls.Move.canceled -= this.MoveInput;

            this.PlayerActions.PlayerControls.Attack.started -= this.AttackInput;
            this.PlayerActions.PlayerControls.Cast.started -= this.CastStartInput;
            this.PlayerActions.PlayerControls.Cast.canceled -= this.CastEndInput;

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

        private void CastStartInput(InputAction.CallbackContext _) {
            this.CastStart();
        }

        private void CastEndInput(InputAction.CallbackContext _) {
            this.CastEnd();
        }
        #endregion

        public void Start() {
            this.AttackTarget = GameObject.FindGameObjectWithTag("AttackTarget").GetComponent<AttackTarget>();
            this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            if (this.CastTarget != null) {
                this.TargetDirection = this.CastTarget.transform.position - this.transform.position;
            } else {
                this.TargetDirection = direction;
            }
        }

        public void Update() {
            this.AdjustPlayerDirection();
        }

        private void AdjustPlayerDirection() {
            if (this.CastTarget != null) {
                if (this.CastTarget.transform.position.x < this.transform.position.x && this.Direction == PlayerDirection.Right && !this.Animator.GetBool("Flipping")) {
                    this.Direction = PlayerDirection.Left;
                    this.Animator.SetBool("Flipping", true);
                } else if (this.CastTarget.transform.position.x > this.transform.position.x && this.Direction == PlayerDirection.Left && !this.Animator.GetBool("Flipping")) {
                    this.Direction = PlayerDirection.Right;
                    this.Animator.SetBool("Flipping", true);
                }
            } else {
                if (this.TargetDirection.x < 0 && this.Direction == PlayerDirection.Right && !this.Animator.GetBool("Flipping")) {
                    this.Direction = PlayerDirection.Left;
                    this.Animator.SetBool("Flipping", true);
                } else if (this.TargetDirection.x > 0 && this.Direction == PlayerDirection.Left && !this.Animator.GetBool("Flipping")) {
                    this.Direction = PlayerDirection.Right;
                    this.Animator.SetBool("Flipping", true);
                }
            }
        }

        private void Attack() {
            if (this.CastTarget != null) { return; }
            float now = Time.time;
            if (now - this.LastAttack > this.AttackDelay) {
                this.Animator.SetTrigger("Attack");
                this.LastAttack = now;
            }
        }

        private void CastStart() {
            CastTarget prefab = Resources.Load<CastTarget>("Prefabs/CastTarget");
            this.CastTarget = Instantiate(prefab);
            this.CastTarget.transform.position = this.AttackTarget.transform.Find("Target").transform.position;
            this.AttackTarget.gameObject.SetActive(false);
        }

        private void CastEnd() {
            this.AttackTarget.gameObject.SetActive(true);
            this.Player.PerformCast();
            Destroy(this.CastTarget.gameObject);
        }

        public void Flip() {
            this.transform.localScale = new(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            this.Animator.SetBool("Flipping", false);
        }
    }
}