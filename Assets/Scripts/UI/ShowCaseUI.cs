using BigBro.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro.UI
{
    public class ShowCaseUI : MonoBehaviour
    {
        [SerializeField] private Image _outfit;
        [SerializeField] private TextMeshProUGUI _ability;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private PlayerData _playerData;
        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
            _playerData.OnDataChanged += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _outfit.color = _playerData.PlayerOutfit;
            _ability.text = _playerData.Ability.ToString();
            _name.text = _playerData.Name;
        }

        public void Dispose()
        {
            _playerData.OnDataChanged -= UpdateUI;
            _playerData = null;
        }
        
        

    }
}
