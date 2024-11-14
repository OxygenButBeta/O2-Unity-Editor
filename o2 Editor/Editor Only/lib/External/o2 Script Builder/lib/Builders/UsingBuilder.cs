
namespace o2.Runtime.ScriptGeneration {
    /// <summary>
    /// The UsingBuilder class is responsible for generating a 'using' directive for a specified namespace.
    /// It holds the namespace name and can build the corresponding 'using' statement for inclusion in a script.
    /// </summary>
    public class UsingBuilder :  IBuildable {
      public readonly string NamespaceName;

        public UsingBuilder(string namespaceName) {
            NamespaceName = namespaceName;
        }

        public string Build() {
            return $"using {NamespaceName};";
        }

    }
}