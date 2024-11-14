namespace o2.Runtime.ScriptGeneration {

    /// <summary>
    /// Specifies the type of a method, including options like static, instance, abstract, virtual, or override.
    /// </summary>
    public enum MethodType {
        _static,   // Static method.
        _instance, // Instance method.
        _abstract, // Abstract method.
        _virtual,  // Virtual method.
        _override  // Override method.
    }
}