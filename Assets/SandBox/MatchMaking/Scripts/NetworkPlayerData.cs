using Fusion;
using UnityEngine;

namespace BigBro
{
    public struct NetworkPlayerData : INetworkStruct
    {
        public NetworkString<_16> Name;
        public Color PlayerOutfit;
        public Ability Ability;
    }
}
