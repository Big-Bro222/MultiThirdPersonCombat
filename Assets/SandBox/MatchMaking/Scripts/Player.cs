using System;
using Fusion;
using UnityEngine;

namespace BigBro
{
    public class Player : NetworkBehaviour , IPlayer
    {
        [SerializeField]
        private PlayerData _localPlayerData;
        public PlayerData PlayerData => _localPlayerData;
        private bool _isLocalPlayer = false;

        public void Init(PlayerData localPlayerData,bool isLocalPlayer)
        {
                _isLocalPlayer = isLocalPlayer;
                _localPlayerData = localPlayerData;
                //do something with the local player
                _localPlayerData.OnDataChange += PlayerDataUpdated;
        }

        
        
        private void PlayerDataUpdated()
        {
            
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
            if (_localPlayerData != null)
            {
                _localPlayerData.OnDataChange -= PlayerDataUpdated;
            }
        }
    }
}
