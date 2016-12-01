namespace Ocean.Portable.InputStringFormatting {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents CharacterFormattingAttribute, when applied to a business entity class property, that property will have its case corrected according to the CharacterCasing assigned.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CharacterFormattingAttribute : Attribute {

        /// <summary>
        /// Get or sets the CharacterCasing that will be applied to this property when the property is updated.
        /// </summary>
        public CharacterCasing CharacterCasing { get; }

        /// <summary>
        /// Gets a value indicating whether to remove multiple spaces.
        /// </summary>
        public RemoveSpace RemoveSpace { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterFormattingAttribute"/> class.
        /// </summary>
        /// <param name="characterCasing">The character casing.</param>
        /// <param name="removeSpace">The remove multiple spaces.</param>
        /// <exception cref="InvalidEnumArgumentException">characterCasing is not a member of CharacterCasing enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">removeSpace is not a member of RemoveSpace enum.</exception>
        public CharacterFormattingAttribute(CharacterCasing characterCasing, RemoveSpace removeSpace = RemoveSpace.None) {
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
