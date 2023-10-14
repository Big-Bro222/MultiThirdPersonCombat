using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace BigBro
{
    public class NetworkInputSetter : InputSetter,INetworkRunnerCallbacks
    {
        private NetWorkInputData _myInput = new NetWorkInputData();
        
        protected override void MoveInput(Vector2 newMoveDirection)
        {
            _myInput.Move = newMoveDirection;
        } 

        protected override void LookInput(Vector2 newLookDirection)
        {
            _myInput.Look = newLookDirection;
        }

        protected override void JumpInput(bool newJumpState)
        {
            _myInput.Buttons.Set(MyButtons.Jump, newJumpState);
        }

        protected override void SprintInput(bool newSprintState)
        {
            _myInput.Buttons.Set(MyButtons.Sprint, newSprintState);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(_myInput);
        }

        #region NetworkCallBacks

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }



        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        { }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        #endregion
        

    }
}
