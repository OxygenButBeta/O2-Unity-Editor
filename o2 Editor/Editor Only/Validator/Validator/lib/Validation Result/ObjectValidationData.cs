using System.Reflection;
using Object = UnityEngine.Object;

namespace o2.EditorTools.Validator {
    public class ObjectValidationData {
        public  readonly Object UnityObject;
        public readonly string fieldName;
        public FieldInfo FieldInfo;
        public readonly ValidationStatus validationStat;

        public ObjectValidationData(Object UnityObject, string fieldName, ValidationStatus validationStat) {
            this.UnityObject = UnityObject;
            this.fieldName = fieldName;
            this.validationStat = validationStat;
        }
    }
}