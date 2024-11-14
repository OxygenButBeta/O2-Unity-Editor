using System.Collections.Generic;

namespace o2.Runtime.ScriptGeneration {
    /// <summary>
    /// The InterfaceBuilder class helps to build a list of interfaces that a class can implement.
    /// It provides methods to add interfaces by type or name and generates a comma-separated list of interface names.
    /// </summary>
    public class InterfaceBuilder : IBuildable {
        readonly List<string> Interfaces = new();
        public int Count => Interfaces.Count;

        public string Build() {
            return string.Join(", ", Interfaces);
        }

        public void AddInterface<T>() {
            Interfaces.Add(typeof(T).Name);
        }

        public void AddInterface(string interfaceName) {
            Interfaces.Add(interfaceName);
        }
    }
}