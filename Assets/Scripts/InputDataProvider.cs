using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBro
{
    /// <summary>
    /// Set up local current InputData
    /// </summary>
    public class InputDataProvider : MonoBehaviour
    {
        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] private bool cursorLocked = false;
        private bool cursorInputForLook = true;

        [SerializeField] private CharacterInputProvider _characterInputProvider;

        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            _characterInputProvider.move = value;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            _characterInputProvider.look = value;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            _characterInputProvider.jump = value;
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            _characterInputProvider.sprint = value;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            _characterInputProvider.fire = value;
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            _characterInputProvider.aim = value;
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            _characterInputProvider.dash = value;
        }
    }
}