namespace o2.Runtime.ScriptGeneration {

    /// <summary>
    /// Defines the type of a class, such as static, abstract, sealed, partial, or normal.
    /// </summary>
    public enum ClassType  {
        _static,   // Static class.
        _abstract, // Abstract class.
        _sealed,   // Sealed class.
        _partial,  // Partial class.
        _normal    // Regular class.
    }
}