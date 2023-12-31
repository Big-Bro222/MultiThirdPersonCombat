using System;
using BigBro.UI.UIComponents;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

namespace BigBro.UI
{
	public class SessionListItem : GridCell
	{
		[SerializeField] private Text _name;
		[SerializeField] private Text _map;
		[SerializeField] private Text _players;

		private Action<SessionInfo> _onJoin;
		private SessionInfo _info;

		public void Setup(SessionInfo info, Action<SessionInfo> onJoin)
		{
			_info = info;
			_name.text = $"{info.Name} ({info.Region})";
			//_map.text = $"Map {new SessionProps(info.Properties).StartMap}";
			_players.text = $"{info.PlayerCount}/{info.MaxPlayers}";
			_onJoin = onJoin;
		}
		
		public void OnJoin()
		{
			_onJoin(_info);
		}
	}
}