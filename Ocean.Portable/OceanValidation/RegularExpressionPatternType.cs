namespace Ocean.Portable.OceanValidation {
    /// <summary>
    /// Specifies the RegularExpressionPatternType
    /// </summary>
    public enum RegularExpressionPatternType {

        /// <summary>
        /// Custom user supplied pattern 
        /// </summary>
        Custom,

        /// <summary>
        /// Email pattern
        /// </summary>
        Email,

        /// <summary>
        /// IP address pattern
        /// </summary>
        IPAddress,

        /// <summary>
        /// Social security number pattern
        /// </summary>
        SSN,

        /// <summary>
        /// Url pattern
        /// </summary>
        Url,

        /// <summary>
        /// Zip code pattern
        /// </summary>
        ZipCode

    }
}
