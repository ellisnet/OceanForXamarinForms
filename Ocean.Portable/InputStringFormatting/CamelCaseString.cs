namespace Ocean.Portable.InputStringFormatting {
    using System;
    using Ocean.Portable;

    /// <summary>
    /// Represents CamelCaseString
    /// </summary>
    public static class CamelCaseString {

        /// <summary>
        /// Initializes the <see cref="CamelCaseString"/> class.
        /// </summary>
        static CamelCaseString() {
        }

        /// <summary>
        /// Designed to parse property or database column names and return a friendly name without punctuation characters.  Example:  "ap_c_FirstName" or "FirstName" will result in "First Name"
        /// </summary>
        /// <param name="stringToCamelCase">The string to camel case.</param>
        /// <returns>String with words parsed from camel case string and space added between upper case letters.</returns>
        public static String GetWords(String stringToCamelCase) {
            if (String.IsNullOrWhiteSpace(stringToCamelCase)) {
                return String.Empty;
            }
            var sb = new System.Text.StringBuilder(256);
            Boolean foundUpper = false;

            foreach (Char c in stringToCamelCase) {
                if (foundUpper) {
                    if (Char.IsUpper(c)) {
                        sb.Append(GlobalConstants.WhiteSpace);
                        sb.Append(c);
                    } else if (Char.IsLetterOrDigit(c)) {
                        sb.Append(c);
                    }
                } else if (Char.IsUpper(c)) {
                    foundUpper = true;
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

    }
}
