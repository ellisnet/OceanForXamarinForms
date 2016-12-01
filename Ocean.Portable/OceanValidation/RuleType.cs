namespace Ocean.Portable.OceanValidation {
    /// <summary>
    /// Specifies the RuleType
    /// </summary>
    public enum RuleType {

        /// <summary>
        /// Rule applied as an attribute
        /// </summary>
        Attribute,

        /// <summary>
        /// Rules applied to a single instance
        /// </summary>
        Instance,

        /// <summary>
        /// Rule is shared between all instances
        /// </summary>
        Shared

    }
}
