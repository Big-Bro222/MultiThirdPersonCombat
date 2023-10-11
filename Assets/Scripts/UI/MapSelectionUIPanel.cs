using BigBro.SO;
using BigBro.UIframework;

namespace BigBro.UI
{
    public class MapSelectionUIPanel : UIPanelBase
    {
        private App _app;
        private UIManager _uiManager;

        protected override void OnCreate()
        {
            base.OnCreate();
            _app = App.FindInstance();
            _uiManager = UIManager.Instance;
        }

        public async void EnterLobby(MapConfig map)
        {
            bool result = await _app.EnterLobby(map);;
            if (result)
            {
                _uiManager.OpenPanel("LobbyUIPanel");
            }
            else
            {
                //TODO: handle error
            }
        }
    }
}
