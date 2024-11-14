using System;
using o2.Runtime.ScriptGeneration;
using UnityEngine;

namespace o2.EditorTools.o2_Editor {
    [Serializable]
    public class MethodInspection {
        [SerializeField] string name;
        [SerializeField] string returnType;
        [SerializeField] AccessModifier accessModifier;
        [SerializeField] Parameter[] parameters;

        [Serializable]
        class Parameter {
            [SerializeField] public string type;
            [SerializeField] public string name;
        }

        public MethodBuilder GetMethodBuilder() {
            var methodBuilder = new MethodBuilder()
            {
                AccessModifier = accessModifier,
                ReturnTypeAsString = returnType,
                Name = name
            };
            foreach (var parameter in parameters)
                methodBuilder.AddParameter(parameter.type, parameter.name);

            return methodBuilder;
        }
    }
}