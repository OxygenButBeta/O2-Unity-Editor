using o2.Runtime.ScriptGeneration;

namespace o2.EditorTools.o2_Editor {
    [System.Serializable]
    public class ScriptDraftPreset {
        public string Name;
        public string BaseClass;
        public string Namespace;
        public ClassType ClassType;
        public AccessModifier AccessModifier;
        public string[] Usings;
        public string[] Interfaces;
        public MethodInspection[] MethodInspections;

        public string Build() {
            var builder = new ScriptBuilder(Name)
            {
                AccessModifier = AccessModifier,
                Namespace = Namespace,
                ClassType = ClassType,
                BaseClass = BaseClass
            };

            foreach (var @interface in Interfaces)
                builder.AddInterface(@interface);

            foreach (var methodInspection in MethodInspections)
                builder.AddMethod(methodInspection.GetMethodBuilder());

            foreach (var variableUsing in Usings)
                builder.AddUsing(variableUsing);
            
            return builder.Build();
        }
    }
}