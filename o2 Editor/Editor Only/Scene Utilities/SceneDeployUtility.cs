using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace o2.EditorTools {
    public static class SceneDeployUtility {
        [RuntimeInitializeOnLoadMethod]
        public static void DeployScene() {
            var sceneUtilityPreset = o2EditorUtility.GetDataAsset().SceneUtility;
            if (!sceneUtilityPreset.StartWithSetupScene)
                return;

            if (SceneManager.GetActiveScene().path != sceneUtilityPreset.StartScenePath)
                SceneManager.LoadScene(sceneUtilityPreset.StartScenePath, LoadSceneMode.Single);
        }
        public static void LoadScene(string path, OpenSceneMode _openSceneMode) {
            if (!EditorApplication.isPlaying)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(path, _openSceneMode);
            }
            else
            {
                var mode = _openSceneMode == OpenSceneMode.Additive
                    ? LoadSceneMode.Additive
                    : LoadSceneMode.Single;

                SceneManager.LoadScene(path, mode);
            }
        }
    }
}