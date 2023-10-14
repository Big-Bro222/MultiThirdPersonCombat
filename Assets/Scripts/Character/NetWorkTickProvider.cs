using System;
using Fusion;
using UnityEngine;

namespace BigBro
{
    public class NetWorkTickProvider : NetworkBehaviour, ITickProvider
    {
        private NetWorkInputData _inputData;
        private float deltaTime = 0;
        public event Action OnTick;
        public float GetDeltaTime()
        {
            return deltaTime;
        }

        [SerializeField] private CharacterInputProvider _characterInputProvider;
        
        public override void FixedUpdateNetwork()
        {
            OnTick?.Invoke();
            deltaTime = Runner.DeltaTime;
            if(GetInput(out _inputData))
            {
                Debug.Log("GetInput!!!");
                _characterInputProvider.look = _inputData.Look;
                _characterInputProvider.move = _inputData.Move;
                _characterInputProvider.jump = _inputData.Buttons.IsSet(MyButtons.Jump);
                _characterInputProvider.sprint = _inputData.Buttons.IsSet(MyButtons.Sprint);
            }
        }
        
        
    }
}
