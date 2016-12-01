namespace Ocean.Portable.InputStringFormatting {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents a CharacterFormat
    /// </summary>
    public class CharacterFormat {

        /// <summary>
        /// Gets the character casing.
        /// </summary>
        public CharacterCasing CharacterCasing { get; }

        /// <summary>
        /// Gets the remove multiple spaces.
        /// </summary>
        public RemoveSpace RemoveSpace { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterFormat"/> class.
        /// </summary>
        /// <param name="characterCasing">The character casing.</param>
        /// <param name="removeSpace">The remove multiple spaces.</param>
        /// <exception cref="InvalidEnumArgumentException">characterCasing is not a member of CharacterCasing enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">removeSpace is not a member of RemoveSpace enum.</exception>
        public CharacterFormat(CharacterCasing characterCasing, RemoveSpace removeSpace) {
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCasing)) {
                throw new InvalidEnumArgumentException(nameof(characterCasing), (Int32)characterCasing, typeof(CharacterCasing));
            }
            if (!Enum.IsDefined(typeof(RemoveSpace), removeSpace)) {
                throw new InvalidEnumArgumentException(nameof(removeSpace), (Int32)removeSpace, typeof(RemoveSpace));
            }
            this.CharacterCasing = characterCasing;
            this.RemoveSpace = removeSpace;
        }

    }
}
