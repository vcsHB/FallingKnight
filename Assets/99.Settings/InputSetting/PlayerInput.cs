using UnityEngine;
using UnityEngine.InputSystem;
using System;
namespace InputSystem
{
    [CreateAssetMenu(menuName = "SO/InputSystem/PlayerInput")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnJumpEvent;
        public event Action OnHoldWallEvent;
        public event Action OnInteractEvent;
        public event Action OnAttackEvent;
        public event Action OnDropAttackEvent;
        public Vector2 InputDirection { get; private set; }
        private Controls _controls;
        public bool canControl = true;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(!canControl) return;
            if (context.performed)
                OnAttackEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(!canControl) return;
            OnInteractEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(!canControl) return;
            if (context.performed)
                OnHoldWallEvent?.Invoke();
            else if (context.canceled)
                OnJumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if(!canControl) return;
            InputDirection = context.ReadValue<Vector2>();
        }

        public void OnDropAttack(InputAction.CallbackContext context)
        {
            if(!canControl) return;
            if (context.performed)
                OnDropAttackEvent?.Invoke();
        }

        public void ClearEventsAll()
        {
            OnJumpEvent = null;
            OnAttackEvent = null;
            OnDropAttackEvent = null;
            OnHoldWallEvent = null;
        }
    }

}