using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigBro
{
    public class NetWorkInputProvider : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] private bool cursorLocked = false;
        private bool cursorInputForLook = true;

        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            MoveInput(value);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            if (cursorInputForLook)
            {
                LookInput(value);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            JumpInput(value);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            SprintInput(value);
        }

        private void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        private void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        private void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        private void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }


        // creating a instance of the Input Action created
        //private PlayerActionMap _playerActionMap = new PlayerActionMap();

        public void OnEnable()
        {
            var myNetworkEvent = GetComponent<NetworkEvents>();
            myNetworkEvent.OnInput.AddListener(OnInput);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var myInput = new NetWorkInputData();
            myInput.Look = look;
            myInput.Move = move;
            myInput.Buttons.Set(MyButtons.Jump, jump);
            myInput.Buttons.Set(MyButtons.Sprint, sprint);
            input.Set(myInput);
        }

        public void OnDisable()
        {
            var myNetworkEvent = GetComponent<NetworkEvents>();
            myNetworkEvent.OnInput.RemoveListener(OnInput);
        }
    }
}