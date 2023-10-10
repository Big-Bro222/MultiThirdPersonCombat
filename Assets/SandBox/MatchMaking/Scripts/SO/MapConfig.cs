using UnityEditor;
using UnityEngine;

namespace BigBro
{
    [CanEditMultipleObjects]
    [CreateAssetMenu(fileName = "MapName_SO", menuName = "BigBro/ScriptableObjects/MapConfig", order = 1)]
    public class MapConfig : ScriptableObject
    {
        public string MapName;
        public SceneReference CombatScene;
        public int MaxPlayer = 4;
    }
}

