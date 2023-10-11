using System.Collections.Generic;
using BigBro.UI.UIComponents;
using BigBro.UIframework;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro.UI
{
    public class LobbyUIPanel : UIPanelBase
    {
        [SerializeField] private GridBuilder _sessionGrid;
        [SerializeField] private SessionListItem _sessionListItemPrefab;
        [SerializeField] private Button _creatSessionBtn;
        private App _app;
        private UIManager _uiManager;

        //TODO: set player limit to related to map
        //private int _playerLimit = 6;
        //private event Action<List<SessionInfo>> _onSessionListUpdated;

        protected override void OnCreate()
        {
            base.OnCreate();
            _app = App.FindInstance();
            _uiManager = UIManager.Instance;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _app.OnSessionListUpdatedEvent+=OnSessionListUpdated;
            _creatSessionBtn.onClick.AddListener(CreateSession);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _app.OnSessionListUpdatedEvent-=OnSessionListUpdated;
            _creatSessionBtn.onClick.RemoveListener(CreateSession);
        }

        private  void CreateSession()
        {
            SessionProps props = new SessionProps();
            //props.StartMap = _toggleMap1.isOn ? MapIndex.Map0 : MapIndex.Map1;
            //props.PlayMode = _playMode;
            props.PlayerLimit = _app.CurrentMap.MaxPlayer;
            props.RoomName = "test";
            props.AllowLateJoin = true; 
            _app.CreateSession(props);
        }

        private void OnSessionListUpdated(List<SessionInfo> sessionInfos)
        {
            _sessionGrid.BeginUpdate();
            if (sessionInfos != null)
            {
                foreach (SessionInfo info in sessionInfos)
                {
                    _sessionGrid.AddRow(_sessionListItemPrefab, item => item.Setup(info, selectedSession =>
                    {
                        // Join an existing session - this will unload the current scene and take us to the Staging area
                        _app.JoinSession(selectedSession);
                    }));
                }
            }
            else
            {
                Debug.LogError("Something wrong with the session lists");
            }
            _sessionGrid.EndUpdate();
        }
    }
}
