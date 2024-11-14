using System.Collections.Generic;
using UnityEngine;

namespace o2.EditorTools.o2_Editor.QuickFab {
    [System.Serializable]
    public class QuickFabAsset : DataAssetBase {
        public List<ObjectPrefab> Prefabs = new();

        [System.Serializable]
        public class ObjectPrefab {
            public GameObject Prefab;
            public string SubMenu;
            public string Name;
        }
    }
}