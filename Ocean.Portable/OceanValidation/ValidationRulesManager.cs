namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Collections.Generic;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents ValidationRulesManager, maintains rule methods for a business Object or business Object type.
    /// </summary>
    public class ValidationRulesManager {

        Dictionary<String, ValidationRulesList> _validationRulesList;

        /// <summary>
        /// Returns RulesDictionary that contains all defined rules for this Object.
        /// </summary>
        public Dictionary<String, ValidationRulesList> RulesDictionary => _validationRulesList ?? (_validationRulesList = new Dictionary<String, ValidationRulesList>());

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesManager"/> class.
        /// </summary>
        public ValidationRulesManager() {
        }

        /// <summary>
        /// Adds a rule to the list of rules to be enforced.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">rule is null</exception>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public void AddRule(IValidationRuleMethod rule, String propertyName) {
            if (rule == null) {
                throw new ArgumentNullException(nameof(rule));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            List<IValidationRuleMethod> list = GetRulesForProperty(propertyName).List;
            list.Add(rule);
        }

        /// <summary>
        /// Adds a rule to the list of rules to be enforced.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="e">The e.</param>
        /// <param name="ruleType">Type of the rule.</param>
        /// <exception cref="ArgumentNullException">
        /// Either handler or e is null
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">ruleType is not a member of RuleType enum.</exception>
        public void AddRule(RuleHandler handler, RuleDescriptorBase e, RuleType ruleType) {
            if (handler == null) {
                throw new ArgumentNullException(nameof(handler));
            }
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (!Enum.IsDefined(typeof(RuleType), ruleType)) {
                throw new InvalidEnumArgumentException(nameof(ruleType), (Int32)ruleType, typeof(RuleType));
            }
            List<IValidationRuleMethod> list = GetRulesForProperty(e.PropertyName).List;
            list.Add(new Validator(handler, e, ruleType));
        }

        /// <summary>
        /// Returns the list containing rules for a property. If no list exists one is created and returned.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see cref="ValidationRulesList" /> for the specified property name.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public ValidationRulesList GetRulesForProperty(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            if (this.RulesDictionary.ContainsKey(propertyName)) {
                return this.RulesDictionary[propertyName];
            }

            var validationRulesList = new ValidationRulesList();
            this.RulesDictionary.Add(propertyName, validationRulesList);
            return validationRulesList;
        }

    }
}
