using System.Text;
using System;

namespace o2.Runtime.ScriptGeneration {

    /// <summary>
    /// Represents a builder for dynamically generating field declarations in C# code.
    /// Allows setting access modifiers, field modifiers (static, readonly, const), and type, 
    /// name, and optional default value for the field. Implements IBuildable to support code generation.
    /// </summary>
    public class FieldBuilder : IBuildable {

        /// <summary>
        /// Specifies the access level of the field (e.g., private, public).
        /// Defaults to private.
        /// </summary>
        public AccessModifier accessModifier = AccessModifier._private;

        /// <summary>
        /// Indicates if the field should be static.
        /// </summary>
        public bool IsStatic = false;

        /// <summary>
        /// Indicates if the field should be readonly.
        /// </summary>
        public bool IsReadonly = false;

        /// <summary>
        /// Indicates if the field should be constant.
        /// </summary>
        public bool IsConst = false;

        /// <summary>
        /// The name of the field.
        /// </summary>
        public string Name;

        /// <summary>
        /// The type of the field as a string representation.
        /// </summary>
        public string Type;

        /// <summary>
        /// The default value of the field, if any.
        /// </summary>
        public string Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBuilder"/> class 
        /// with the specified field name and type.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The type of the field as a string.</param>
        public FieldBuilder(string name, string type) {
            this.Type = type;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBuilder"/> class 
        /// with the specified field name and a <see cref="Type"/> object.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The <see cref="Type"/> object representing the field's type.</param>
        public FieldBuilder(string name, Type type) {
            this.Type = type.Name;
            this.Name = name;
        }

        /// <summary>
        /// Builds the field declaration as a formatted C# code string based on the specified properties.
        /// </summary>
        /// <returns>A string representing the complete field declaration in C#.</returns>
        public string Build() {
            StringBuilder builder = new();

            builder.Append(accessModifier.ToString().ToLower() + " ");

            if (!IsConst)
            {
                if (IsStatic)
                    builder.Append("static ");

                if (IsReadonly)
                    builder.Append("readonly ");
            }
            else
                builder.Append("const ");

            builder.Append(Type)
                .Append(" ")
                .Append(Name);

            if (!string.IsNullOrEmpty(Value))
            {
                builder.Append(" = ")
                    .Append(Value);
            }

            builder.Append(";");
            return builder.ToString();
        }
    }
}
