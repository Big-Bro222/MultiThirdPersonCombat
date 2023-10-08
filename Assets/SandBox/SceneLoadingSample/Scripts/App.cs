using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BigBro;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ConnectionStatus
{
	Disconnected,
	Connecting,
	Connected,
	Failed,
	EnteringLobby,
	InLobby,
	Starting,
	Started
}

/// <summary>
/// This is the main entry point for the application. App is a singleton created when the game is launched.
/// Access it anywhere using `App.Instance`
/// </summary>

[RequireComponent(typeof(NetworkSceneManagerBase))]
public class App : MonoBehaviour, INetworkRunnerCallbacks
{
	[SerializeField] private SceneReference _introScene;
	[SerializeField] private Player _playerPrefab;
	//[SerializeField] private Session _sessionPrefab;
	//[SerializeField] private ErrorBox _errorBox;
	//[SerializeField] private PlayerSetupPanel _playerSetup;
	private MapConfig _currentMap;
	public MapConfig CurrentMap => _currentMap;

	[Space(10)]
	//[SerializeField] private bool _autoConnect;
	[SerializeField] private bool _skipStaging;
	//[SerializeField] private SessionProps _autoSession = new SessionProps();

	private NetworkRunner _runner;
	private MapLoader _loader;
	public event Action<List<SessionInfo>> OnSessionListUpdatedEvent;
	//private InputData _data;
	//private Session _session;
	private string _lobbyId;
	private bool _allowInput;

	public static App FindInstance()
	{
		return FindObjectOfType<App>();
	}

	
	 public ConnectionStatus ConnectionStatus { get; private set; }
	 public bool IsSessionOwner => _runner != null && (_runner.IsServer);
	// //public SessionProps AutoSession => _autoSession;
	public bool SkipStaging => _skipStaging;
	//
	// public bool AllowInput
	// {
	// 	get => _allowInput && Session != null && Session.PostLoadCountDown.Expired(Session.Runner);
	// 	set => _allowInput = value;
	// } 
	//
	private void Awake()  
	{
		App[] apps = FindObjectsOfType<App>();
		
		if (apps != null && apps.Length > 1)
		{
			// There should never be more than a single App container in the context of this sample.
			Destroy(gameObject);
			return;
		}
		
		if(_loader==null)
		{
			_loader = GetComponent<MapLoader>();
			DontDestroyOnLoad(gameObject);
			SceneManager.LoadSceneAsync( _introScene );
		}
	}
	//
	private void Connect()
	{
		if (_runner == null)
		{
			SetConnectionStatus(ConnectionStatus.Connecting);
			// GameObject go = new GameObject("Session");
			// go.transform.SetParent(transform);
			_runner = gameObject.AddComponent<NetworkRunner>();
			_runner.AddCallbacks(this);
		}
	}
	
	private void Disconnect()
	{
		if (_runner != null)
		{
			SetConnectionStatus(ConnectionStatus.Disconnected);
			_runner.Shutdown();
		}
	}

	private void SetupCombatMap(SceneReference map)
	{
		_loader.SetCombatMap(map);
	}
	
	public void  loadSceneNetwork(MapIndex mapIndex)
	{
		_loader.loadSceneNetwork(mapIndex);
	}
	
	public void JoinSession(SessionInfo info)
	{
		
		SessionProps props = new SessionProps(info.Properties);
		props.PlayerLimit = info.MaxPlayers;
		props.RoomName = info.Name;
		StartSession(GameMode.Client, props);
		
		// Debug.Log("Joining Session");
		// SessionProps props = new SessionProps();
		// //props.StartMap = _toggleMap1.isOn ? MapIndex.Map0 : MapIndex.Map1;
		// //props.PlayMode = _playMode;
		// props.PlayerLimit = 6;
		// props.RoomName = "test";
		// props.AllowLateJoin = true; 
		// StartSession(GameMode.Client, props);
	}
	
	public async void CreateSession(SessionProps props)
	{
		await StartSession( GameMode.Host, props, true);
	}

	private async Task StartSession(GameMode mode, SessionProps props, bool disableClientSessionCreation=true)
	{
		Connect();
	
		SetConnectionStatus(ConnectionStatus.Starting);
		Debug.Log($"Starting game with session {props.RoomName}, player limit {props.PlayerLimit}");
		_runner.ProvideInput = mode != GameMode.Server;
		StartGameResult result = await _runner.StartGame(new StartGameArgs
		{
			GameMode = mode,
			CustomLobbyName = _lobbyId,
			SceneManager = _loader,
			SessionName = "test",
			PlayerCount = props.PlayerLimit,
			SessionProperties = props.Properties,
			DisableClientSessionCreation = disableClientSessionCreation
		});
		if(!result.Ok)
			SetConnectionStatus(ConnectionStatus.Failed, result.ShutdownReason.ToString());
		else
		{
			//load to new scene
			loadSceneNetwork((int)MapIndex.Staging);
		}
	}

	public async Task<bool> EnterLobby(MapConfig map)
	{
		Connect();
		//
		_lobbyId = map.MapName;
		_currentMap = map;
		// _onSessionListUpdated = onSessionListUpdated;
		//
		SetConnectionStatus(ConnectionStatus.EnteringLobby);
		 var result = await _runner.JoinSessionLobby(SessionLobby.Custom, _lobbyId);
		//
		if (!result.Ok) {
			//_onSessionListUpdated = null;
			SetConnectionStatus(ConnectionStatus.Failed);
			//onSessionListUpdated(null);
		}
		else
		{
			SetConnectionStatus(ConnectionStatus.InLobby);
			SetupCombatMap(map.CombatScene);
		}
		return result.Ok;
	}

	// public Session Session
	// {
	// 	get => _session;
	// 	set { _session = value; _session.transform.SetParent(_runner.transform); }
	// }

	// public Player GetPlayer()
	// {
	// 	return _runner?.GetPlayerObject(_runner.LocalPlayer)?.GetComponent<Player>();
	// }
	//
	// public void ForEachPlayer(Action<Player> action)
	// {
	// 	if (_runner)
	// 	{
	// 		foreach (PlayerRef plyRef in _runner.ActivePlayers)
	// 		{
	// 			NetworkObject plyObj = _runner.GetPlayerObject(plyRef);
	// 			if (plyObj)
	// 			{
	// 				Player ply = plyObj.GetComponent<Player>();
	// 				action(ply);
	// 			}
	// 		}
	// 	}
	// }
	//
	private void SetConnectionStatus(ConnectionStatus status, string reason="")
	{
		if (ConnectionStatus == status) return;
		ConnectionStatus = status;
	
		// if (!string.IsNullOrWhiteSpace(reason) && reason != "Ok")
		// {
		// 	_errorBox.Show(status,reason);
		// }
		
		Debug.Log($"ConnectionStatus={status} {reason}");
	}
	//
	// /// <summary>
	// /// Fusion Event Handlers
	// /// </summary>
	//
	public void OnConnectedToServer(NetworkRunner runner)
	{
		Debug.Log("Connected to server");
		SetConnectionStatus(ConnectionStatus.Connected);
	}
	
	public void OnDisconnectedFromServer(NetworkRunner runner)
	{
		Debug.Log("Disconnected from server");
		Disconnect();
	}
	
	public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
	{
		 Debug.Log($"Connect failed {reason}");
		 Disconnect();
		 SetConnectionStatus(ConnectionStatus.Failed, reason.ToString());
	}
	
	public void OnPlayerJoined(NetworkRunner runner, PlayerRef playerRef)
	{
		Debug.Log($"Player {playerRef} Joined!");
		// if ( _session==null && IsSessionOwner)
		// {
		// 	Debug.Log("Spawning world");
		// 	_session = runner.Spawn(_sessionPrefab, Vector3.zero, Quaternion.identity);
		// }
		//
		if (runner.IsServer)
		{
			Debug.Log("Spawning player");
			Player player= runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, playerRef, (runner, obj) => runner.SetPlayerObject(playerRef, obj) );
			player.Init();
			player.transform.SetParent(transform);
		}
		//
		// SetConnectionStatus(ConnectionStatus.Started);
	}
	
	public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
	{
		// Debug.Log($"{player.PlayerId} disconnected.");
		//
		// if (runner.IsServer)
		// {
		// 	NetworkObject playerObj = runner.GetPlayerObject(player);
		// 	if (playerObj)
		// 	{
		// 		if (playerObj != null && playerObj.HasStateAuthority)
		// 		{
		// 			Debug.Log("De-spawning Player");
		// 			playerObj.GetComponent<Player>().Despawn();
		// 		}
		// 	}
		// }
	}
	
	public void OnShutdown(NetworkRunner runner, ShutdownReason reason)
	{
		// Debug.Log($"OnShutdown {reason}");
		// SetConnectionStatus(ConnectionStatus.Disconnected, reason.ToString());
		//
		// if(_runner!=null && _runner.gameObject)
		// 	Destroy(_runner.gameObject);
		//
		// _runner = null;
		// _session = null;
		//
		// if(Application.isPlaying)
		// 	SceneManager.LoadSceneAsync(_introScene);
	}
	
	public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
	{
		//request.Accept();
	}
	//
	// private void Update() 
	// {
	// 	// Check events like KeyDown or KeyUp in Unity's update. They might be missed otherwise because they're only true for 1 frame
	// 	_data.ButtonFlags |= Input.GetKeyDown( KeyCode.R ) ? ButtonFlag.RESPAWN : 0;
	// }
	//
	// public void ShowPlayerSetup()
	// {
	// 	if(_playerSetup)
	// 		_playerSetup.Show(true);
	// }
	//
	public void OnInput(NetworkRunner runner, NetworkInput input)
	{
		// if (!AllowInput)
		// 	return;
		//
		// // Persistent button flags like GetKey should be read when needed so they always have the actual state for this tick
		// _data.ButtonFlags |= Input.GetKey( KeyCode.W ) ? ButtonFlag.FORWARD : 0;
		// _data.ButtonFlags |= Input.GetKey( KeyCode.A ) ? ButtonFlag.LEFT : 0;
		// _data.ButtonFlags |= Input.GetKey( KeyCode.S ) ? ButtonFlag.BACKWARD : 0;
		// _data.ButtonFlags |= Input.GetKey( KeyCode.D ) ? ButtonFlag.RIGHT : 0;
		//
		// input.Set( _data );
		//
		// // Clear the flags so they don't spill over into the next tick unless they're still valid input.
		// _data.ButtonFlags = 0;
	}
	
	public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
	{
		OnSessionListUpdatedEvent?.Invoke(sessionList);
	}

	public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
	public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
	public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
	public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
	public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
	public void OnSceneLoadDone(NetworkRunner runner) { }
	public void OnSceneLoadStart(NetworkRunner runner) { }
}