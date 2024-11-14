using System;
using System.Collections.Generic;

namespace o2.EditorTools.Validator  {
    public class ValidationResult {
        public readonly List<ObjectValidationData> validationResults = new();
        public readonly int CheckedCount;
        public readonly string time;

        public ValidationResult(int checkedCount) {
            CheckedCount = checkedCount;
            time = DateTime.Now.ToString("HH:mm:ss");
        }

        public int CountOf(ValidationStatus status) {
            return validationResults.FindAll(data => data.validationStat == status).Count;
        }
    }
}