using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BigBro
{
    public interface IPlayer
    {
        void Init(PlayerData localPlayerData,bool isLocalPlayer);
        void SpawnCharacter();
        void DespawnCharacter();
        void Clear();

    }
}
