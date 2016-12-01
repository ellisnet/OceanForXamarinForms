namespace Ocean.Portable.InputStringFormatting {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents SharedCharacterCasingRules
    /// </summary>
    public class SharedCharacterFormattingRules {

        static readonly Dictionary<Type, CharacterFormattingRulesManager> _characterCasingRulesManagers = new Dictionary<Type, CharacterFormattingRulesManager>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedCharacterFormattingRules"/> class.
        /// </summary>
        SharedCharacterFormattingRules() {
        }

        /// <summary>
        /// Gets the <see cref="CharacterFormattingRulesManager" /> for the specified Object type, optionally creating a new instance of the Object if necessary.
        /// </summary>
        /// <param name="type">Type of business Object for which the rules apply.</param>
        /// <returns><see cref="CharacterFormattingRulesManager" /> for the specified type.</returns>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static CharacterFormattingRulesManager GetManager(Type type) {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }
            lock(_characterCasingRulesManagers) {
                CharacterFormattingRulesManager manager;
                if (!_characterCasingRulesManagers.TryGetValue(type, out manager)) {
                    manager = new CharacterFormattingRulesManager();
                    _characterCasingRulesManagers.Add(type, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Returns a Boolean value indicating whether a set of rules have been created for the specified <see cref="Type" />.
        /// </summary>
        /// <param name="type">Type of business Object for which the rules apply.</param>
        /// <returns><c>True</c> if rules exist for the type; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Boolean RulesExistFor(Type type) {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }
            lock(_characterCasingRulesManagers) {
                return _characterCasingRulesManagers.ContainsKey(type);
            }
        }

    }
}
