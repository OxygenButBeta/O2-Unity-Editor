using System.Linq;
using UnityEditor;
using UnityEngine;

namespace o2.EditorTools {
    [InitializeOnLoad]
    public sealed class HierarchyHighlighter {
        static HierarchyHighlighterAsset _highlighterAsset;

        static HierarchyHighlighter() {
            _highlighterAsset = o2EditorUtility.GetDataAsset().HierarchyHighlighter;
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
            if (!_highlighterAsset.Enable)
                return;

            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (obj == null) return;

            // ReSharper disable once Unity.ExplicitTagComparison
            foreach (var pair in _highlighterAsset._tagColorPairs.Where(pair => obj.tag == pair.Tag.ToString()))
            {
                float indentLevel = EditorGUI.indentLevel * 14;
                Rect bgRect = new(selectionRect.x + indentLevel, selectionRect.y, selectionRect.width - indentLevel,
                    selectionRect.height);
                Rect labelRect = new(selectionRect.x + indentLevel, selectionRect.y,
                    selectionRect.width,
                    selectionRect.height);

                EditorGUI.DrawRect(bgRect, pair.Color);
                EditorGUI.LabelField(labelRect, obj.name, new GUIStyle()
                {
                    fontSize = pair.fontSize,
                    fontStyle = pair.FontStyle,
                    alignment = pair.alignment,
                    normal = new GUIStyleState() { textColor = Color.black },
                });
                break;
            }
        }
    }
}