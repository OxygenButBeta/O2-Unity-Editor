using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace o2.EditorTools {
    public class SceneUtilityGUI : EditorWindow {
        readonly List<string> _activeScenes = new();
        bool _IsStartSceneSet, _isStartSceneSetPrevious;
        int selectedIndex, previousIndex;
        OpenSceneMode _openSceneMode;
        SceneUtilityPreset _sceneUtilityPreset;

        public static void Init() {
            EditorWindow window = GetWindow<SceneUtilityGUI>();
            window.titleContent = new GUIContent("Scene Utility");
            o2EditorUtility.GetDataAsset();
            window.Show();
        }

        void OnEnable() {
            var scenes = EditorBuildSettings.scenes;
            _activeScenes.AddRange(scenes.Where(scene => scene.enabled)
                .Select(scene => scene.path));
            _activeScenes.AddRange(GetNonBuildScenes());

            _sceneUtilityPreset = o2EditorUtility.GetDataAsset().SceneUtility;
            _IsStartSceneSet = _sceneUtilityPreset.StartWithSetupScene;
            if (_sceneUtilityPreset.StartScenePath != string.Empty)
                selectedIndex = Array.IndexOf(_activeScenes.ToArray(), _sceneUtilityPreset.StartScenePath);
        }

        IReadOnlyList<string> GetNonBuildScenes() {
            return new List<string>()
            {
                /*
                "Assets/Scenes/Scene1.unity",
                "Assets/Scenes/Scene2.unity",
                "Assets/Scenes/Scene3.unity"
                 */
            };
        }

        void OnGUI() {
            if ((previousIndex != selectedIndex || _isStartSceneSetPrevious != _IsStartSceneSet) &&
                _activeScenes.Count != 0)
            {
                _sceneUtilityPreset.StartWithSetupScene = _IsStartSceneSet;
                try
                {
                    _sceneUtilityPreset.StartScenePath = _activeScenes[selectedIndex];
                }
                catch (Exception e)
                {
                    _sceneUtilityPreset.StartScenePath = _activeScenes[0];
                }

                _isStartSceneSetPrevious = _IsStartSceneSet;
                previousIndex = selectedIndex;
            }

            GUILayout.Space(10);
            GUILayout.Label("Loadable Scenes", EditorStyles.boldLabel);
            var btnPerRow = (int)(position.width / 200);
            var cIndex = 0;
            foreach (var t in _activeScenes)
            {
                if (cIndex == 0)
                    GUILayout.BeginHorizontal();

                if (GUILayout.Button(Path.GetFileNameWithoutExtension(t)))
                    LoadScene(t);

                cIndex++;

                if (cIndex != btnPerRow)
                    continue;
                GUILayout.EndHorizontal();
                cIndex = 0;
            }

            if (cIndex != 0)
                GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("Actions", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reload Current Scene"))
                LoadScene(SceneManager.GetActiveScene().path);

            if (GUILayout.Button("Highlight in Project Explorer"))
                HighlightScene(SceneManager.GetActiveScene().path);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("Preferences", EditorStyles.boldLabel);
            _openSceneMode = (OpenSceneMode)EditorGUILayout.EnumPopup("Scene Load Mode", _openSceneMode);

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Start With Setup Scene", EditorStyles.boldLabel);
            _IsStartSceneSet = EditorGUILayout.Toggle(_IsStartSceneSet);
            GUILayout.EndHorizontal();

            if (!_IsStartSceneSet || _activeScenes.Count <= 0)
                return;


            GUILayout.Space(5);
            GUILayout.Label(
                "Select the scene to start with. If active, this scene will automatically start when entering Play Mode.");
            GUILayout.Space(10);

            selectedIndex = EditorGUILayout.Popup("Setup Scene", selectedIndex,
                _activeScenes.Select(Path.GetFileNameWithoutExtension).ToArray());
            selectedIndex = Mathf.Clamp(selectedIndex, 0, _activeScenes.Count - 1);


            GUILayout.Space(10);
            GUILayout.Label("'" + Path.GetFileNameWithoutExtension(_activeScenes[selectedIndex]) +
                            "' Scene will be when editor enters play mode.");
        }

        void LoadScene(string path) {
            SceneDeployUtility.LoadScene(path, _openSceneMode);
        }

        void HighlightScene(string path) {
            var relativePath = path.Substring(path.IndexOf("Assets", StringComparison.Ordinal));
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(relativePath);
            if (sceneAsset != null)
            {
                Selection.activeObject = sceneAsset;
                EditorGUIUtility.PingObject(sceneAsset);
            }
            else
                Debug.LogError("Scene not found in the project folder.");
        }
    }
}