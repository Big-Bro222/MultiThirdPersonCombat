using System;
using BigBro.Character;
using BigBro.SO;
using Fusion;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace BigBro
{
    public class Player : NetworkBehaviour, IPlayer
    {
        [FormerlySerializedAs("_CharacterPrefab")] [SerializeField] private CharacterBase  _characterPrefab;
        private CharacterBase _character;
        [SerializeField] private PlayerData _localPlayerData;
        public PlayerData PlayerData => _localPlayerData;
        [Networked] private NetworkPlayerData _networkPlayerData { get; set; }

        [Networked] public ref NetworkPlayerData _networkPlayerDataRef => ref MakeRef<NetworkPlayerData>();

        public bool IsLocalPlayer => _isLocalPlayer;
        private bool _isLocalPlayer = false;

        public void Init(PlayerData localPlayerData, bool isLocalPlayer)
        {
            _isLocalPlayer = isLocalPlayer;
            _localPlayerData = localPlayerData;
            //do something with the local player
            _localPlayerData.OnDataChanged += PlayerDataUpdated;
        }


        private void PlayerDataUpdated()
        {
            if (_localPlayerData.ShouldNetworkUpdate)
            {
                //transfer the player data to networkplayerdata
                _networkPlayerDataRef.Name = _localPlayerData.Name;
                _networkPlayerDataRef.PlayerOutfit = _localPlayerData.PlayerOutfit;
                _networkPlayerDataRef.Ability = _localPlayerData.Ability;
                //RPC calls
                RPC_UpdatePlayerData(_networkPlayerDataRef);
            }
        }

        
        

        //TODO: Fix the wierd bug when the local client data get update
        [Rpc(RpcSources.InputAuthority, RpcTargets.All, InvokeLocal = false)]
        public void RPC_UpdatePlayerData(NetworkPlayerData networkPlayerData)
        {
            //This work only do on remote player
            //transfer networkPlayerData to _localPlayerData
            _localPlayerData.Name = networkPlayerData.Name.ToString();
            _localPlayerData.PlayerOutfit = networkPlayerData.PlayerOutfit;
            _localPlayerData.Ability = networkPlayerData.Ability;
            _localPlayerData.ChangeData();
        }


        public void SpawnCharacter()
        {
            //get spawn points
            _character=Runner.Spawn(_characterPrefab,Vector3.zero, quaternion.identity, Object.InputAuthority);
        }

        public void DespawnCharacter()
        {
            //if out of a game scene, despawn a character
            Runner.Despawn(_character.Object);
        }

        public void Clear()
        {
            if (_localPlayerData != null)
            {
                _localPlayerData.OnDataChanged -= PlayerDataUpdated;
            }
        }
    }
}