using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Scripts.Player {
    public class CastTargetController : MonoBehaviour {
        private PlayerActions PlayerActions;
        private Vector2 Direction;
        private Rigidbody2D Rigidbody;

        #region Input
        private void OnEnable() {
            this.PlayerActions = new PlayerActions();
            this.PlayerActions.PlayerControls.Enable();

            this.PlayerActions.PlayerControls.Aim.performed += this.AimInput;
            this.PlayerActions.PlayerControls.Aim.canceled += this.AimInput;
        }

        private void OnDisable() {
            this.PlayerActions.PlayerControls.Aim.performed -= this.AimInput;
            this.PlayerActions.PlayerControls.Aim.canceled -= this.AimInput;

            this.PlayerActions.PlayerControls.Disable();
        }

        private void AimInput(InputAction.CallbackContext context) {
            this.Aim(context.ReadValue<Vector2>());
        }
        #endregion

        public void Start() {
            this.Rigidbody = this.GetComponentInChildren<Rigidbody2D>();
        }

        private void Aim(Vector2 direction) {
            this.Direction = direction;
        }

        public void FixedUpdate() {
            this.Rigidbody.velocity = this.Direction * 10;
        }
    }
}