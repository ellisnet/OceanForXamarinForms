namespace Ocean.Portable.InputStringFormatting {
    using System;
    using System.Text;
    using Ocean.Portable.ComponentModel;
    using Ocean.Portable.Infrastructure;

    /// <summary>
    /// Provides a container for a single character casing check.
    /// </summary>
    public class CharacterCasingCheck : ObservableObject, IDataErrorInfo, IComparable<CharacterCasingCheck> {

        String _lookFor = String.Empty;
        String _replaceWith = String.Empty;
        const String LOOKFOR = "LookFor";
        const String REPLACEWITH = "ReplaceWith";

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public String Error {
            get {
                if (_lookFor == null) {
                    _lookFor = String.Empty;
                }

                if (_replaceWith == null) {
                    _replaceWith = String.Empty;
                }

                var error = new StringBuilder();

                if (String.IsNullOrEmpty(_lookFor)) {
                    error.Append("Look For is a required field");
                }

                if (String.IsNullOrEmpty(_replaceWith)) {
                    if (error.Length > 0) {
                        error.Append(Environment.NewLine);
                    }

                    error.Append("Replace With is a required field");
                }

                if (_lookFor.Length != _replaceWith.Length) {
                    if (error.Length > 0) {
                        error.Append(Environment.NewLine);
                    }

                    error.Append("Look For and Replace With must be the same length");
                }

                return error.ToString();
            }
        }

        /// <summary>
        /// Gets the any errors associated with the specified column name.
        /// </summary>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public String this[String columnName] {
            get {
                if (columnName == LOOKFOR && String.IsNullOrEmpty(_lookFor)) {
                    return "Look For is a required field";
                }

                if (columnName == REPLACEWITH && String.IsNullOrEmpty(_replaceWith)) {
                    return "Replace With is a required field";
                }

                return _lookFor.Length != _replaceWith.Length ? "Look For and Replace With must be the same length" : String.Empty;
            }
        }

        /// <summary>
        /// Gets and sets the String value to look for when the character casing check is being performed.
        /// </summary>
        /// <value>The look for.</value>
        public String LookFor {
            get { return _lookFor; }
            set {
                _lookFor = value;
                RaisePropertyChanged(LOOKFOR);
                RaisePropertyChanged(REPLACEWITH);
            }
        }

        /// <summary>
        /// Gets and sets the String value that will replace the LookFor value when the character casing check is being performed.
        /// </summary>
        /// <value>The replace with.</value>
        public String ReplaceWith {
            get { return _replaceWith; }
            set {
                _replaceWith = value;
                RaisePropertyChanged(REPLACEWITH);
                RaisePropertyChanged(LOOKFOR);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCasingCheck"/> class.
        /// </summary>
        public CharacterCasingCheck() {
        }

        /// <summary>
        /// Provides a container for a custom character casing correction.  The LookFor and ReplaceWith strings must be the same length.
        /// </summary>
        /// <param name="lookFor">String value to replace.</param>
        /// <param name="replaceWith">String value that will replace the LookFor value.</param>
        /// <exception cref="ArgumentException"> when the look for, and replace with strings are not the same length.</exception>
        public CharacterCasingCheck(String lookFor, String replaceWith) {
            if (lookFor.Length != replaceWith.Length) {
                throw new ArgumentException("LookFor and ReplaceWith strings must be the same length");
            }

            _lookFor = lookFor;
            _replaceWith = replaceWith;
        }

        /// <summary>
        /// Compares this instance to another <see cref="CharacterCasingCheck"/>.
        /// </summary>
        /// <param name="other">The other <see cref="CharacterCasingCheck"/>.</param>
        /// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.</returns>
        public Int32 CompareTo(CharacterCasingCheck other) {
            return String.Compare(_lookFor, other.LookFor, StringComparison.Ordinal);
        }

    }
}
