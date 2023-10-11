using BigBro.UIframework;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro.UI
{
    public class PlayerSelectionPanel : UIPanelBase
    {
        
        private App _app;
        private UIManager _uiManager;
        [SerializeField] private Button selectBtn;
        [SerializeField] private PlayerInfoList _playerInfoList;
        protected override void OnCreate()
        {
            base.OnCreate();
            _app = App.FindInstance();
            _uiManager = UIManager.Instance;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _playerInfoList.Init(_app.GetActivePlayers());
            _app.OnActivePlayerListUpdatedEvent += _playerInfoList.UpdateInfo;
            selectBtn.onClick.AddListener(SceneReady);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _playerInfoList.Clear();
            _app.OnActivePlayerListUpdatedEvent -= _playerInfoList.UpdateInfo;
            selectBtn.onClick.RemoveListener(SceneReady);
        }

        private void SceneReady()
        {
            _app.loadSceneNetwork(MapIndex.Map);
        }
    }
}
