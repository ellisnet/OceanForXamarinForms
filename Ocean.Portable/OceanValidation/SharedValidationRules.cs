namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents SharedValidationRules, maintains a list of all the per-type <see cref="ValidationRulesManager"/> objects loaded in memory.
    /// </summary>
    public static class SharedValidationRules {

        static readonly Dictionary<Type, ValidationRulesManager> _validationRuleManagers = new Dictionary<Type, ValidationRulesManager>();

        /// <summary>
        /// Gets the <see cref="ValidationRulesManager" /> for the specified object type, optionally creating a new instance of the ValidationRulesManager for the type if necessary.
        /// </summary>
        /// <param name="type">Type of business Object for which the rules apply.</param>
        /// <returns><see cref="ValidationRulesManager" /> for the specified type.</returns>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static ValidationRulesManager GetManager(Type type) {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }
            lock(_validationRuleManagers) {
                ValidationRulesManager manager;
                if (!(_validationRuleManagers.TryGetValue(type, out manager))) {
                    manager = new ValidationRulesManager();
                    _validationRuleManagers.Add(type, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a set of rules have been created for a given <see cref="Type" />.
        /// </summary>
        /// <param name="type">Type of business Object for which the rules apply.</param>
        /// <returns><see langword="true" /> if rules exist for the type.</returns>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Boolean RulesExistFor(Type type) {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }
            Boolean returnValue;
            lock(_validationRuleManagers) {
                returnValue = _validationRuleManagers.ContainsKey(type);
            }
            return returnValue;
        }

    }
}
