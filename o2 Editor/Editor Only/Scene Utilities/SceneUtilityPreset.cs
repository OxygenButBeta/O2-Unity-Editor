using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace o2.EditorTools {
    [Serializable]
    public class SceneUtilityPreset : DataAssetBase {
        public bool StartWithSetupScene;
        public string StartScenePath;
    }
}