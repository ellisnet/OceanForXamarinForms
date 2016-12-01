namespace Ocean.Portable.InputStringFormatting {
    /// <summary>
    /// Specifies the input String character casing
    /// </summary>
    public enum CharacterCasing {

        /// <summary>
        /// No character casing applied
        /// </summary>
        None,

        /// <summary>
        /// All characters converted to lower case
        /// </summary>
        LowerCase,

        /// <summary>
        /// Formats phone number entries using Outlook style phone formatting, no case changes applied
        /// </summary>
        OutlookPhoneNoProperName,

        /// <summary>
        /// Formats phone number entries using Outlook style phone formatting, proper name casing applied
        /// </summary>
        OutlookPhoneProperName,

        /// <summary>
        /// Formats phone number entries using Outlook style phone formatting, upper casing applied
        /// </summary>
        OutlookPhoneUpper,

        /// <summary>
        /// Proper name casing applied
        /// </summary>
        ProperName,

        /// <summary>
        /// All characters converted to upper case
        /// </summary>
        UpperCase

    }
}
