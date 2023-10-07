using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BigBro
{
    public interface IPlayer
    {
        void Init();
        void GenerateCharacter();
        void Clear();

    }
}
