using UnityEditor;
using UnityEngine;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;
using static System.IO.Directory;
using static  System.IO.Path;

namespace BigBro.Editor
{
    public static class SandBoxProjectSetupTool 
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        //[CreateAssetMenu()]
        public static void CreateDefaultFolders()
        {
            
            Dir(GetAssetPath(Selection.activeObject).Replace("Assets/",""), "Scripts", "Prefabs", "Presets","Arts");
            Refresh();
        }

        public static void Dir(string root, params string[] dir)
        {
            var fullpath = Combine(dataPath, root);
            foreach (var newDirectroy in dir)
            {
                CreateDirectory(Combine(fullpath, newDirectroy));
            }
        }

    }
}
