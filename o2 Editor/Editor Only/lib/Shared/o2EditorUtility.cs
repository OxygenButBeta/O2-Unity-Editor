using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace o2.EditorTools {
    public static class o2EditorUtility {
        // ReSharper disable once AssignNullToNotNullAttribute
        static string AssetPath =>
            Path.Combine(Path.GetDirectoryName(GetAssemblyDefinitionPath()), "o2EditorConfig.asset");

        public static string GetAssemblyDefinitionPath() {
            return AssetDatabase.FindAssets("t:asmdef").Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault(path => path.Contains($"o2.EditorTools"));
        }

        [InitializeOnLoadMethod]
        public static void ValidateData() {
            if (string.IsNullOrEmpty(GetAssemblyDefinitionPath()))
                Debug.LogError("Assembly Definition file not found. Please re-import the package.");

            GetOrCreateDataAsset();
        }

        [MenuItem("O2/Settings", priority = 99)]
        static void OpenSettingsAsset() {
            var asset = GetOrCreateDataAsset();
            if (asset == null)
                return;
            EditorGUIUtility.PingObject(asset);
            AssetDatabase.OpenAsset(asset); 
        }

        public static o2EditorDataAsset GetDataAsset() =>
            GetOrCreateDataAsset();

        static o2EditorDataAsset GetOrCreateDataAsset() {
            var path = AssetPath;
            var asset = AssetDatabase.LoadAssetAtPath<o2EditorDataAsset>(path);
            if (asset != null)
                return asset;

            var newData = ScriptableObject.CreateInstance<o2EditorDataAsset>();
            AssetDatabase.CreateAsset(newData, path);
            AssetDatabase.SaveAssets();
            return newData;
        }

        public static string GetCurrentFolderPath() {
            var selected = Selection.activeObject;
            if (selected == null) return "Assets";
            var path = AssetDatabase.GetAssetPath(selected);
            return Directory.Exists(path) ? path : Path.GetDirectoryName(path);
        }
        
        public static void CreateScript(string path,string scriptName, string content) {
            var scriptPath = Path.Combine(path, scriptName + ".cs");
            if (File.Exists(scriptPath)) {
                Debug.LogError("File already exists");
                return;
            }
            File.WriteAllText(scriptPath, content);
            AssetDatabase.Refresh();
        }
    }
}