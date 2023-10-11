using Fusion;
using UnityEngine;

namespace BigBro
{
	public enum MapIndex {
		Staging,
		GameOver,
		Map
	};

	/// <summary>
	/// MapLoader handles the transition from one game scene to the next by showing a load screen,
	/// loading the required scene and then collecting all network objects in that scene and passing
	/// them to Fusion.
	/// </summary>
 
	public class MapLoader : NetworkSceneManagerDefault
	{
		[SerializeField] private GameObject _loadScreen;

		[Header("Scenes")]
		[SerializeField] private SceneReference _staging;
		[SerializeField] private SceneReference _gameOver;
		[SerializeField] private SceneReference _combatMap;

		private void Awake()
		{
			_loadScreen.SetActive(false);
		}

		public void SetCombatMap(SceneReference combatMap)
		{
			_combatMap = combatMap;
		}
	
		public void loadSceneNetwork(MapIndex _mapIndex)
		{
			int sceneIndex;
			// Debug.Log("Login");
			switch (_mapIndex)
			{
				case MapIndex.Staging: sceneIndex = _staging.SceneBuildIndex; break;
				case MapIndex.GameOver: sceneIndex = _gameOver.SceneBuildIndex; break;
				case MapIndex.Map: sceneIndex = _combatMap.SceneBuildIndex; break;
				default: sceneIndex = _gameOver.SceneBuildIndex; break;
			}	
		
			// Debug.Log(_staging.ScenePath);
			// SceneUtility.GetBuildIndexByScenePath(_staging);
		
			Runner.SetActiveScene(sceneIndex);
		}
	}
}