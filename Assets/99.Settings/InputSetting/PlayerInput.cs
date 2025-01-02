using UnityEngine;
using InputSystem;
using UnityEngine.InputSystem;
using System;
namespace InputSystem
{
    [CreateAssetMenu(menuName ="SO/InputSystem/PlayerInput")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnJumpEvent;
        public event Action OnInteractEvent;
        public event Action OnAttackEvent;
        public Vector2 InputDirection { get; private set; }
        private Controls _controls;

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
            OnAttackEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            OnInteractEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            OnJumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
        }
    }

}