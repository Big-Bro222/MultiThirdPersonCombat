using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace BigBro
{
    public class PlayerLocomotionState : PlayerStateBehaviour
    {
        [SerializeField]
        private LinearMixerTransition _moveMixer;
        
        //[SerializeReference] private ITransition _Move;

        protected override void OnEnterStateRender()
        {
            Animancer.Play(_moveMixer);
            // Update the animation time based on the state time
            _moveMixer.State.Time = Machine.StateTime;
        }

        protected override void OnRender()
        {
            _moveMixer.State.Parameter = Controller.InterpolatedSpeed;
        }
    }
}
