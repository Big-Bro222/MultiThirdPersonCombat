using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace BigBro
{
    public class PlayerJumpState : PlayerStateBehaviour
    {
        [SerializeField]
        private ClipTransition _jumpStartClip;
        
        [SerializeField]
        private ClipTransition _jumpFinishClip;

        protected override void OnEnterStateRender()
        {
            Animancer.Play(_jumpStartClip);
        }
        
        protected override void OnExitStateRender()
        {
            Animancer.Play(_jumpFinishClip);
        }

        protected override void OnRender()
        {
            base.OnRender();
            Debug.Log("Jump");
        }

        protected override void OnFixedUpdate()
        {
        }
    }
}
