using System.Net.NetworkInformation;
using BigBro.UIframework;
using UnityEngine;

namespace BigBro
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

        public async void EnterLobby(string lobbyId)
        {
            bool result = await _app.EnterLobby(lobbyId);;
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
