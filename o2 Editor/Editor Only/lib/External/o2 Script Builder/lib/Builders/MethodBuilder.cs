using System;
using System.Collections.Generic;
using System.Text;

namespace o2.Runtime.ScriptGeneration {
    /// <summary>
    /// Builds a C# method with specified properties, including name, return type, access modifier, 
    /// attributes, parameters, and body content.
    /// </summary>
    public class MethodBuilder : IBuildable {
        public string Name = "UnnamedMethod";
        public string Body = string.Empty;
        public bool ExpressionBody = false;
        public string ReturnTypeAsString = String.Empty;
        public Type ReturnType = typeof(void);
        public AccessModifier AccessModifier = AccessModifier._private;
        public MethodType MethodType = MethodType._instance;
        readonly List<(string type, string parameterName)> Parameters = new();
        readonly List<(string type, string parameterName, string value)> OptionalParameters = new();
        readonly List<(string Attribute, string param)> Attributes = new();
        readonly List<string> RawAttributes = new();

        /// <summary>
        /// Constructs the method's C# code string based on the builder's settings.
        /// </summary>
        public string Build() {
            StringBuilder builder = new();

            foreach (var attribute in Attributes)
            {
                if (!string.IsNullOrEmpty(attribute.param))
                    builder.Append("[" + attribute.Attribute + "(" + attribute.param + ")]\n");
                else
                    builder.Append("[" + attribute.Attribute + "]\n");
            }

            foreach (var rawAttribute in RawAttributes)
                builder.AppendLine(rawAttribute);

            builder.Append("\n")
                .Append(AccessModifier.ToString().Replace("_", " "))
                .Append(" ");

            if (MethodType != MethodType._instance)
            {
                builder.Append(MethodType.ToString().Replace("_", ""));
                builder.Append(" ");
            }

            builder.Append(ReturnType.Name.ToLower())
                .Append(" ")
                .Append(Name)
                .Append("(");

            for (var index = 0; index < Parameters.Count; index++)
            {
                var (parameter, parameterName) = Parameters[index];
                builder.Append(parameter)
                    .Append(" ")
                    .Append(parameterName);
                if (index < Parameters.Count - 1)
                    builder.Append(", ");
            }

            if (Parameters.Count > 0 && OptionalParameters.Count > 0)
                builder.Append(", ");

            for (var index = 0; index < OptionalParameters.Count; index++)
            {
                (string type, string NameAndvalue, string value) optionalParameter = OptionalParameters[index];
                builder.Append(optionalParameter.type + " " + optionalParameter.NameAndvalue + " = " +
                               optionalParameter.value);
                if (index < OptionalParameters.Count - 1)
                    builder.Append(", ");
            }

            if (Body == string.Empty)
                Body = "throw new NotImplementedException();";

            builder.Append(")")
                .Append(ExpressionBody ? " => " : " {\n")
                .Append(Body)
                .Append(ExpressionBody ? "\n" : "\n}\n");

            return builder.ToString();
        }

        /// <summary>
        /// Adds a raw attribute line to the method.
        /// </summary>
        public MethodBuilder AddAttributeLine(string attribute) {
            RawAttributes.Add(attribute);
            return this;
        }

        /// <summary>
        /// Adds a parameter of a specific type to the method.
        /// </summary>
        public MethodBuilder AddParameter<T>(string parameterName) {
            Parameters.Add((typeof(T).Name, parameterName));
            return this;
        }

        /// <summary>
        /// Adds a parameter with a specified type as a string to the method.
        /// </summary>
        public MethodBuilder AddParameter(string type, string parameterName) {
            Parameters.Add((type, parameterName));
            return this;
        }

        /// <summary>
        /// Adds an optional parameter with a default value to the method.
        /// </summary>
        public MethodBuilder AddOptionalParameter(string type, string parameterName, string defaultValue) {
            OptionalParameters.Add((type, parameterName, defaultValue));
            return this;
        }

        /// <summary>
        /// Adds an attribute with an optional parameter to the method.
        /// </summary>
        public MethodBuilder AddAttribute(string attributeName, string attributeParam = "") {
            Attributes.Add((attributeName, attributeParam));
            return this;
        }

        /// <summary>
        /// Adds a line to the body of the method.
        /// </summary>
        public MethodBuilder AddBodyLine(string line) {
            Body += line + "\n";
            return this;
        }

        /// <summary>
        /// Returns the built method as a formatted string.
        /// </summary>
        public override string ToString() {
            return Build();
        }
    }
}