using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace o2.EditorTools.Validator {
    public class ValidatorUtilityGUI : EditorWindow {
        ValidationResult validationResult;
        bool stylesCreated;
        GUIStyle Err;
        GUIStyle Warn;
        GUIStyle Success;

        int scrollHeight = 250;

        public static void DeployUtility() {
            EditorWindow window = GetWindow<ValidatorUtilityGUI>();
            window.titleContent = new GUIContent("Validation Utility");
            window.Show();
        }

        void CreateStyles() {
            Success = new GUIStyle(EditorStyles.label);
            Warn = new GUIStyle(EditorStyles.label);
            Err = new GUIStyle(EditorStyles.label);

            Err.normal.textColor = new Color(1f, 0.29f, 0.21f);
            Err.fontStyle = FontStyle.Bold;
            Warn.normal.textColor = Color.yellow;
            Warn.fontStyle = FontStyle.Bold;
            Success.normal.textColor = Color.green;
            Success.fontStyle = FontStyle.Bold;
            stylesCreated = true;
            
            scrollHeight = o2EditorUtility.GetDataAsset().ValidatorOptions.ScrollHeight;
        }


        void ValidateGUI() {
            GUILayout.Space(25);
            GUILayout.Label("Validate", EditorStyles.boldLabel);
            // CheckMissingScripts = EditorGUILayout.Toggle("Validate For Missing Scripts  ", CheckMissingScripts);

            if (GUILayout.Button("Validate All"))
            {
                validationResult =
                    o2ValidationUtility.ValidateAll(FindObjectsByType<GameObject>(FindObjectsSortMode.None));
            }
        }

        void ValidateAll() {
            validationResult =
                o2ValidationUtility.ValidateAll(FindObjectsByType<GameObject>(FindObjectsSortMode.None));
        }

        void OnGUI() {
            if (!stylesCreated)
                CreateStyles();

            GUILayout.Label("Last Validation data", EditorStyles.boldLabel);
            if (validationResult == null)
                ValidateAll();


            GUILayout.Label($"Total Objects Validated: {validationResult.validationResults.Count}");
            GUILayout.Label($"Missing Scripts: {validationResult.CountOf(ValidationStatus.MissingScript)}");
            GUILayout.Label($"Unassigned Fields: {validationResult.CountOf(ValidationStatus.UnAssignedField)}");
            GUILayout.Label($"Total Problems and Warnings: {validationResult.validationResults.Count}");
            GUILayout.Label($"Last Validation Time: {validationResult.time}");
            GUILayout.Space(10);


            if (validationResult.validationResults.Count == 0)
            {
                GUILayout.Label("Everything looks good. You are good to go!", Success);
                ValidateGUI();
                return;
            }

            GUILayout.Space(15);

            ShowMissingScripts();
            ShowNullFields();
            ValidateGUI();
        }

        Vector2 scrollPosUnassigned = Vector2.zero;
        Vector2 scrollMissingScripts = Vector2.zero;

        void ShowNullFields() {
            if (validationResult.CountOf(ValidationStatus.UnAssignedField) == 0)
            {
                GUILayout.Label("No unassigned fields were found.", Success);
                GUILayout.Space(15);
                return;
            }

            GUILayout.Label("Fields in these scripts are marked as SerializeField or SerializeReference", Warn);
            GUILayout.Label("However, they have not been assigned. This may cause issues at runtime.", Warn);

            scrollPosUnassigned = EditorGUILayout.BeginScrollView(scrollPosUnassigned, GUILayout.Height(scrollHeight));
            GUILayout.Space(15);
            GUILayout.Label("Scripts with unassigned fields", Warn);
            GUILayout.Space(10);

            foreach (var objectValidationData in validationResult.validationResults)
            {
                if (objectValidationData.UnityObject is not MonoBehaviour script)
                    continue;

                GUILayout.Label(
                    $"Unassigned fields found in the script '{script.GetType().Name}'");
                var x = (MonoBehaviour)EditorGUILayout.ObjectField(script, typeof(MonoBehaviour), true);
                var fieldInfo = objectValidationData.FieldInfo;

                Object fieldValue = fieldInfo.GetValue(script) as Object;
                GUILayout.BeginHorizontal();
                GUILayout.Label("Unassigned Field: '" + fieldInfo.Name + "'", Err);
                Object newFieldValue = EditorGUILayout.ObjectField("", fieldValue,
                    fieldInfo.FieldType, true);
                GUILayout.EndHorizontal();
                if (newFieldValue != fieldValue)
                {
                    fieldInfo.SetValue(script, newFieldValue);
                    EditorUtility.SetDirty(script);
                }

                GUILayout.Space(10);
            }

            EditorGUILayout.EndScrollView();
            if (!GUILayout.Button("Try to resolve all unassigned fields"))
                return;
            foreach (var objectValidationData in validationResult.validationResults)
            {
                if (objectValidationData.UnityObject is not MonoBehaviour script)
                    continue;

                if (!typeof(MonoBehaviour).IsAssignableFrom(objectValidationData.FieldInfo.FieldType) &&
                    !typeof(Component).IsAssignableFrom(objectValidationData.FieldInfo.FieldType) &&
                    !objectValidationData.FieldInfo.FieldType.IsInterface)
                    continue;

                var components = script.GetComponentsInChildren(objectValidationData.FieldInfo.FieldType);

                if (components == null || components.Length == 0)
                    components = script.GetComponentsInParent(objectValidationData.FieldInfo.FieldType);

                if (components != null && components.Length > 0)
                    objectValidationData.FieldInfo.SetValue(script, components[0]);

                if (components == null || components.Length == 0)
                    objectValidationData.FieldInfo.SetValue(script,
                        FindFirstObjectByType(objectValidationData.FieldInfo.FieldType));
            }


            ValidateAll();
        }

        void ShowMissingScripts() {
            if (validationResult.CountOf(ValidationStatus.MissingScript) == 0)
            {
                GUILayout.Label("No missing scripts were found.", Success);
                GUILayout.Space(15);
                return;
            }

            scrollMissingScripts = EditorGUILayout.BeginScrollView(scrollMissingScripts, GUILayout.Height(scrollHeight));
            GUILayout.Label("GameObjects with missing scripts", Err);
            foreach (var objectValidationData in validationResult.validationResults)
            {
                if (objectValidationData.UnityObject is GameObject gameObject)
                {
                    var x = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject),
                        true);
                }
            }

            EditorGUILayout.EndScrollView();
            GUILayout.Space(15);
            if (!GUILayout.Button("Remove all missing scripts"))
            {
                GUILayout.Space(15);
                return;
            }

            foreach (var objectValidationData in validationResult.validationResults)
                if (objectValidationData.UnityObject is GameObject gameObject)
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);

            ValidateAll();
        }
    }
}