using o2.EditorTools;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(o2EditorDataAsset))]
public class o2EditorDataAssetCustomEditor : Editor {
    public override void OnInspectorGUI() {
        var editorDataAsset = (o2EditorDataAsset)target;
        base.OnInspectorGUI();
        GUILayout.Space(10);
        if (!GUILayout.Button("Re-Generate Scripts"))
            return;
        MenuContextScriptGenerator.GenerateScript(editorDataAsset);

        AssetDatabase.Refresh();
    }
}