using Fusion;
using UnityEngine;

namespace BigBro
{
    //TODO: find a better way to represent button name
    enum MyButtons {
        Jump = 0,
        Sprint = 1,
    }
    public struct NetWorkInputData : INetworkInput
    {
        public Vector2 Move;
        public Vector2 Look;
        public NetworkButtons Buttons;
    }
}
