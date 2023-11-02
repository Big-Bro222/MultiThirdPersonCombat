using System.Collections;
using System.Collections.Generic;
using Animancer;
using Example;
using Fusion;
using Fusion.FSM;
using Fusion.KCC;
using UnityEngine;

namespace BigBro
{
    [OrderAfter(typeof(CharacterController))]
    [OrderBefore(typeof(StateMachineController))]
    public class KCCAnimancerFSM : NetworkBehaviour, IStateMachineOwner
    {
        // PRIVATE MEMBERS

        [Networked, HideInInspector] public int JumpCount { get; set; }
        [Networked, HideInInspector] public float Speed { get; set; }
        
        public int InterpolatedJumpCount => _jumpCountInterpolator.Value;
        public float InterpolatedSpeed => _speedInterpolator.Value;
        
        private PlayerBehaviourMachine _fullBodyMachine;
        private KCCPlayerInputHandler _playerInput;
        private Interpolator<int> _jumpCountInterpolator;
        private Interpolator<float> _speedInterpolator;
        private KCC _kcc;

        private bool _isInAir = false;

        // NetworkBehaviour INTERFACE

        public override void Spawned()
        {
            _jumpCountInterpolator = GetInterpolator<int>(nameof(JumpCount));
            _speedInterpolator = GetInterpolator<float>(nameof(Speed));
            _playerInput = GetComponent<KCCPlayerInputHandler>();
            _kcc = GetComponent<KCC>();
        }

        public override void FixedUpdateNetwork()
        {
            if (IsProxy == true)
                return;
            
            if (!_kcc.FixedData.IsGrounded && !_isInAir)
            {
                if (_fullBodyMachine.TryActivateState<PlayerJumpState>())
                {
                    _isInAir = true;
                };
            }

            if (_kcc.FixedData.IsGrounded && _isInAir)
            {
                _isInAir = false;
                _fullBodyMachine.TryDeactivateState<PlayerJumpState>();
            }

            Speed = _kcc.FixedData.RealSpeed;
            Debug.Log(Speed);

        }

        // IStateMachineOwner INTERFACE

        void IStateMachineOwner.CollectStateMachines(List<IStateMachine> stateMachines)
        {
            var states = GetComponentsInChildren<PlayerStateBehaviour>();
            var animancer = GetComponentInChildren<AnimancerComponent>();

            _fullBodyMachine = new PlayerBehaviourMachine("Full Body", this, animancer, states);
            stateMachines.Add(_fullBodyMachine);
        }
    }
}