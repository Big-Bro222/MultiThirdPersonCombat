using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Utility;

namespace BigBro
{
    public class PlayerInfoListItem : PooledObject
    {
        [SerializeField] private Image _playerOutfit;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _ability;
        [SerializeField] private PlayerData _playerData;
        private bool isLocalPlayer = false;

        public void Setup(PlayerData data)
        {
            _playerData = data;
            _playerData.OnDataChange+=UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _playerOutfit.color = _playerData.PlayerOutfit;
            _name.text = _playerData.Name;
            _ability.text = _playerData.Ability.ToString();
        }

        public override void OnRecycled()
        {
            base.OnRecycled();
            _playerData = null;
            _playerData.OnDataChange-=UpdateUI;
        }
    }
}
