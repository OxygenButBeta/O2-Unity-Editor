using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace o2.EditorTools.Validator {
    public static class o2ValidationUtility {
        public static ValidationResult ValidateAll(GameObject[] behaviours) {
            var result = new ValidationResult(behaviours.Length);

            var ObjectsWithMissingScripts =
                FindMissingScripts(behaviours.Select(behaviour => behaviour.gameObject).ToArray());

            foreach (var vScript in ObjectsWithMissingScripts)
                result.validationResults.Add(new(vScript, "Missing Script", ValidationStatus.MissingScript));

            var failedValidations = ValidateFields(behaviours);

            foreach (var (behaviour, fieldInfo) in failedValidations)
            {
                result.validationResults.Add(new(behaviour, fieldInfo.Name, ValidationStatus.UnAssignedField)
                {
                    FieldInfo = fieldInfo
                });
            }

            return result;
        }

        static bool IsIgnoredNamespace(string ns, List<string> ignoredNamespaces) {
            return ignoredNamespaces.Any(ns.Contains);
        }

        static IEnumerable<(MonoBehaviour, FieldInfo)> ValidateFields(GameObject[] gameObjects) {
            List<(MonoBehaviour, FieldInfo)> failedValidations = new();
            ValidatorOptions options = o2EditorUtility.GetDataAsset().ValidatorOptions;
            var nameSpacesToIgnore = options.IgnoredNamespaces;
            foreach (var obj in gameObjects)
            {
                foreach (var behaviour in obj.GetComponentsInChildren<MonoBehaviour>())
                {
                    if (behaviour == null)
                        continue;


                    var ns = behaviour.GetType().Namespace;
                    if (ns != null)
                        if (IsIgnoredNamespace(ns, nameSpacesToIgnore))
                            continue;


                    var fields = behaviour.GetType()
                        .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


                    foreach (var field in fields)
                    {
                        if (validAttribute(field, options.ValidatePublicFields))
                        {
                            var fieldValue = field.GetValue(behaviour);
                            if (fieldValue == null || fieldValue.Equals(null))
                            {
                                failedValidations.Add((behaviour, field));
                            }
                        }
                    }

                    bool validAttribute(FieldInfo fieldInfo, bool validatePublicFields) {
                        if (fieldInfo.GetCustomAttribute<IgnoreValidationAttribute>() != null)
                            return false;


                        if (fieldInfo.GetCustomAttribute<SerializeReference>() != null ||
                            fieldInfo.GetCustomAttribute<SerializeField>() != null)
                            return true;

                        return fieldInfo.IsPublic && validatePublicFields;
                    }
                }
            }


            return failedValidations;
        }


        public static IEnumerable<GameObject> FindMissingScripts(GameObject[] objects) {
            return (from gameObject in objects
                from component in gameObject.GetComponentsInChildren<Component>()
                where component == null
                select gameObject).ToList();
        }
    }
}