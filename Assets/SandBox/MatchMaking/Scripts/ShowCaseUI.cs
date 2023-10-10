using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro
{
    public class ShowCaseUI : MonoBehaviour
    {
        [SerializeField] private Image _outfit;
        [SerializeField] private TextMeshProUGUI _ability;
        [SerializeField] private TextMeshProUGUI _name;
        private PlayerData _playerData;
        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
            _playerData.OnDataChange += UpdateUI;
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
            _playerData.OnDataChange -= UpdateUI;
            _playerData = null;
        }
        
        

    }
}
