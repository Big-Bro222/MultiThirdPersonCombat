using System.Collections;
using System.Collections.Generic;
using Example;
using Fusion;
using Fusion.KCC;
using UnityEngine;


namespace BigBro
{
    public class KCCAnimationAgent : NetworkBehaviour
    {
        // PUBLIC MEMBERS

        [Networked, HideInInspector] public int JumpCount { get; set; }
        [Networked, HideInInspector] public float Speed { get; set; }

        public bool HasJumped { get; private set; }

        public int InterpolatedJumpCount => _jumpCountInterpolator.Value;
        public float InterpolatedSpeed => _speedInterpolator.Value;

        // PRIVATE MEMBERS

        [Networked] private NetworkButtons _lastButtonsInput { get; set; }
        
        private Interpolator<int> _jumpCountInterpolator;
        private Interpolator<float> _speedInterpolator;
        private KCC _kcc;

        // NetworkBehaviour INTERFACE

        public override void Spawned()
        {
            // _jumpCountInterpolator = GetInterpolator<int>(nameof(JumpCount));
            // _speedInterpolator = GetInterpolator<float>(nameof(Speed));
            // _playerInput = GetComponent<KCCPlayerInputHandler>();
            // _kcc = GetComponent<KCC>();
        }

        public override void FixedUpdateNetwork()
        {
            if (IsProxy == true)
                return;

            
        }
    }
}