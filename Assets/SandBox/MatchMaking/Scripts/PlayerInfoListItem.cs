using UnityEngine;
using System;
using BigBro.UIframework;
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
        [SerializeField] private Button _setUpBtn;
        

        public void Setup(PlayerData data, bool isLocalPlayer)
        {
            _playerData = data;
            _playerData.OnDataChanged+=UpdateUI;
            UpdateUI();
            if (isLocalPlayer)
            {
                _setUpBtn.gameObject.SetActive(true);
                _setUpBtn.onClick.AddListener(OpenPlayerSetupPanel);
            }
        }

        private void OpenPlayerSetupPanel()
        {
            UIManager.Instance.OpenPanel("PlayerSetupPanel", (uiPanelBase) =>
            {
                PlayerSetupPanel panel = (PlayerSetupPanel)uiPanelBase;
                panel.SetPlayerData(_playerData);
            });
        }

        private void UpdateUI()
        {
            if (_playerData.ShouldNetworkUpdate)
            {
                _playerOutfit.color = _playerData.PlayerOutfit;
                _name.text = _playerData.Name;
                _ability.text = _playerData.Ability.ToString();
            }
        }

        public override void OnRecycled()
        {
            base.OnRecycled();
            _playerData.OnDataChanged-=UpdateUI;
            _playerData = null;
            _setUpBtn.gameObject.SetActive(false);
            _setUpBtn.onClick.RemoveAllListeners();
        }
    }
}
