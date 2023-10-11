using BigBro.UIframework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro
{
    public class PlayerSetupPanel : UIPanelBase
    {
        private PlayerData _data;
        private PlayerDataHolder _initData;

        [SerializeField]
        private Button _confirmBtn;

        [SerializeField] 
        private Button _cancelBtn;

        [SerializeField] private Image _outfit;
        [SerializeField] private TextMeshProUGUI _abiltiy;
        [SerializeField] private TMP_InputField _name;
        [SerializeField] private ShowCaseUI _showCaseUI;

        protected override void OnOpen()
        {
            base.OnOpen();
            _confirmBtn.onClick.AddListener(Confirm);
            _cancelBtn.onClick.AddListener(Cancel);
            _name.onEndEdit.AddListener((text)=>UpdateData());
            _data.SetChangeNetworkAvaliable(false);
            SetupUI();
            _initData = _data.GetPlayerDataHolder() ;
            _showCaseUI.Setup(_data);
        }

        public void SetPlayerData(PlayerData data)
        {
            _data = data;
        }

        private void SetupUI()
        {
            _name.text = _data.Name;
            _outfit.color = _data.PlayerOutfit;
            _abiltiy.text = _data.Ability.ToString();
        }

        private void UpdateData()
        {
            _data.Name = _name.text;
            /*_data.Ability = _abiltiy;*/
            _data.PlayerOutfit = _outfit.color;
            _data.ChangeData();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _confirmBtn.onClick.RemoveListener(Confirm);
            _cancelBtn.onClick.RemoveListener(Cancel);
            _name.onEndEdit.RemoveListener((text)=>UpdateData());
            _data.SetChangeNetworkAvaliable(true);
            _showCaseUI.Dispose();
        }

        private void Confirm()
        {
            //Close Panel
            UIManager.Instance.CloseCurrentPanel();
        }

        private void Cancel()
        {
            _data.SetPlayerData(_initData);
            //Close Panel
            UIManager.Instance.CloseCurrentPanel();
        }
    }
}
