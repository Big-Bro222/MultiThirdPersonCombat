namespace Example
{
    using UnityEngine;
    using Fusion;

    public enum EGameplayInputAction
    {
        LMB = 0,
        RMB = 1,
        MMB = 2,
        Jump = 3,
        Dash = 4,
        Sprint = 5,
        Aim = 6,
        Fire = 7
    }

    /// <summary>
    /// Input structure polled by Fusion. This is sent over network and processed by server, keep it optimized and remove unused data.
    /// </summary>
    public struct GameplayInput : INetworkInput
    {
        // PUBLIC MEMBERS

        public Vector2 MoveDirection;
        public Vector2 LookRotationDelta;
        public Vector3 HeadPosition;
        public Quaternion HeadRotation;
        public Vector3 LeftHandPosition;
        public Quaternion LeftHandRotation;
        public Vector3 RightHandPosition;
        public Quaternion RightHandRotation;
        public NetworkButtons Actions;

        public bool LMB
        {
            get { return Actions.IsSet(EGameplayInputAction.LMB); }
            set { Actions.Set(EGameplayInputAction.LMB, value); }
        }

        public bool RMB
        {
            get { return Actions.IsSet(EGameplayInputAction.RMB); }
            set { Actions.Set(EGameplayInputAction.RMB, value); }
        }

        public bool MMB
        {
            get { return Actions.IsSet(EGameplayInputAction.MMB); }
            set { Actions.Set(EGameplayInputAction.MMB, value); }
        }
        
        public bool Aim
        {
            get { return Actions.IsSet(EGameplayInputAction.Aim); }
            set { Actions.Set(EGameplayInputAction.Aim, value); }
        }
        
        public bool Fire
        {
            get { return Actions.IsSet(EGameplayInputAction.Fire); }
            set { Actions.Set(EGameplayInputAction.Fire, value); }
        }

        public bool Jump
        {
            get { return Actions.IsSet(EGameplayInputAction.Jump); }
            set { Actions.Set(EGameplayInputAction.Jump, value); }
        }

        public bool Dash
        {
            get { return Actions.IsSet(EGameplayInputAction.Dash); }
            set { Actions.Set(EGameplayInputAction.Dash, value); }
        }

        public bool Sprint
        {
            get { return Actions.IsSet(EGameplayInputAction.Sprint); }
            set { Actions.Set(EGameplayInputAction.Sprint, value); }
        }
    }

    public static class GameplayInputActionExtensions
    {
        // PUBLIC METHODS


        //Detect if a certain key is pressed done
        public static bool IsActive(this EGameplayInputAction action, GameplayInput input)
        {
            return input.Actions.IsSet(action) == true;
        }

        //Detect the last and current pressed key
        public static bool WasActivated(this EGameplayInputAction action, GameplayInput currentInput,
            GameplayInput previousInput)
        {
            return currentInput.Actions.IsSet(action) == true && previousInput.Actions.IsSet(action) == false;
        }

        public static bool WasDeactivated(this EGameplayInputAction action, GameplayInput currentInput,
            GameplayInput previousInput)
        {
            return currentInput.Actions.IsSet(action) == false && previousInput.Actions.IsSet(action) == true;
        }
    }
}