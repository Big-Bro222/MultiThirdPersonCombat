using Fusion;
using UnityEngine;

namespace BigBro
{
    public class Player : NetworkBehaviour , IPlayer
    {
        public void Init()
        {
            // Hook with scriptable object
        }

        public void GenerateCharacter()
        {
            //if in game scene, spawn a character
        }

        public void ClearCharacter()
        {
            //if out of a game scene, despawn a character
        }

        public void Clear()
        {
            // Unregister scriptable object
        }
    }
}
