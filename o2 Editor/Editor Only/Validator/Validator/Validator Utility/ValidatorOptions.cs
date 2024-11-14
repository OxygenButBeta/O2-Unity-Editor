using System;
using System.Collections.Generic;

namespace o2.EditorTools.Validator {
    [Serializable]
    public class ValidatorOptions : DataAssetBase {
        public int ScrollHeight = 250;
        public bool ValidatePublicFields = true;

        public List<string> IgnoredNamespaces = new()
        {
            "UnityEngine",
            "TMPro",
            "Unity",
            "UnityEditor",
            "Netcode",
        };
    }
}