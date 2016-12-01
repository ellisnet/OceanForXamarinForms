namespace Ocean.Portable.InputStringFormatting {
    using System;
    using System.Collections.Generic;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents CharacterCasingRulesManager, maintains character casing rules for a business Object.
    /// </summary>
    public class CharacterFormattingRulesManager {

        Dictionary<String, CharacterFormat> _characterCasingRulesList;

        /// <summary>
        /// Gets RulesDictionary that contains all defined rules for this Object.
        /// </summary>
        public Dictionary<String, CharacterFormat> RulesDictionary => _characterCasingRulesList ?? (_characterCasingRulesList = new Dictionary<String, CharacterFormat>());

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterFormattingRulesManager"/> class.
        /// </summary>
        public CharacterFormattingRulesManager() {
        }

        /// <summary>
        /// Adds a CharacterCasing Formatting rule to the list of rules to be executed when the property is changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="characterCasing">The desired character casing formatting.</param>
        /// <param name="removeSpace">The remove multiple space.</param>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        /// <exception cref="InvalidEnumArgumentException">characterCasing is not a member of CharacterCasing enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">characterCasing is not a member of CharacterCasing enum.</exception>
        public void AddRule(String propertyName, CharacterCasing characterCasing, RemoveSpace removeSpace) {
            if (string.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCasing)) {
                throw new InvalidEnumArgumentException(nameof(characterCasing), (Int32)characterCasing, typeof(CharacterCasing));
            }
            if (!Enum.IsDefined(typeof(RemoveSpace), removeSpace)) {
                throw new InvalidEnumArgumentException(nameof(removeSpace), (Int32)removeSpace, typeof(RemoveSpace));
            }
            this.RulesDictionary.Add(propertyName, new CharacterFormat(characterCasing, removeSpace));
        }

        /// <summary>
        /// Returns the CharacterCasing rule for a the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see cref="CharacterCasing"/> from the rules dictionary for the property name.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public CharacterFormat GetRuleForProperty(String propertyName) {
            if (string.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return this.RulesDictionary.ContainsKey(propertyName) ? this.RulesDictionary[propertyName] : null;
        }

    }
}
