using BigBro.UIframework;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro
{
    public class PlayerSelectionPanel : UIPanelBase
    {
        
        private App _app;
        private UIManager _uiManager;
        [SerializeField] private Button selectBtn;
        protected override void OnCreate()
        {
            base.OnCreate();
            _app = App.FindInstance();
            _uiManager = UIManager.Instance;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            selectBtn.onClick.AddListener(SceneReadyDel);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            selectBtn.onClick.RemoveListener(SceneReadyDel);
        }

        private void SceneReadyDel()
        {
            _app.loadSceneNetwork((int)MapIndex.GameOver);
        }
    }
}
