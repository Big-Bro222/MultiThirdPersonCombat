using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.KCC;
using UnityEngine;

namespace BigBro
{
    public class KCCAnimationController : NetworkBehaviour
    {
        private KCC _kcc;

        public override void Spawned()
        {
            base.Spawned();
            _kcc = GetComponent<KCC>();
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork(); 
            Debug.Log(_kcc.FixedData.RealVelocity.magnitude);
            Debug.Log(_kcc.FixedData.IsGrounded);
        }
    }
}
