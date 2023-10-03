using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace BigBro.SandBox.Fusion
{
    public class NetWorkInputProviderSandBox : MonoBehaviour,INetworkRunnerCallbacks
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        private bool cursorLocked = false;
        private bool cursorInputForLook = true;

        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            MoveInput(value);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            if(cursorInputForLook)
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

        public void OnEnable(){
            var myNetworkRunner = FindObjectOfType<NetworkRunner>();
            myNetworkRunner?.AddCallbacks( this );
        }

        public virtual void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            
        }

        public virtual void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            
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

        public virtual void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            
        }

        public virtual void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            
        }
        public virtual void OnConnectedToServer(NetworkRunner runner)
        {
            
        }

        public virtual void OnDisconnectedFromServer(NetworkRunner runner)
        {
            
        }

        public virtual void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            
        }

        public virtual void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            
        }

        public virtual void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            
        }

        public virtual void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            
        }

        public virtual void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            
        }

        public virtual void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            
        }

        public virtual void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
            
        }

        public virtual void OnSceneLoadDone(NetworkRunner runner)
        {
            
        }

        public virtual void OnSceneLoadStart(NetworkRunner runner)
        {
            
        }

        public void OnDisable(){
            var myNetworkRunner = FindObjectOfType<NetworkRunner>();
            myNetworkRunner?.RemoveCallbacks( this );
        }
    }
	
}
