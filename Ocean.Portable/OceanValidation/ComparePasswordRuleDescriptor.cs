namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Class ComparePasswordRuleDescriptor.
    /// </summary>
    /// <seealso cref="Ocean.Portable.OceanValidation.RuleDescriptorBase" />
    public class ComparePasswordRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Gets or sets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePasswordRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public ComparePasswordRuleDescriptor(ComparePasswordValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.CompareToPropertyName = e.CompareToPropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePasswordRuleDescriptor"/> class.
        /// </summary>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="ArgumentException">compareToPropertyName cannot be null or whitespace.</exception>
        public ComparePasswordRuleDescriptor(String compareToPropertyName, String propertyName, String propertyFriendlyName, String ruleSet, String customMessage, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            if (String.IsNullOrWhiteSpace(compareToPropertyName)) {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(compareToPropertyName));
            }
            this.CompareToPropertyName = compareToPropertyName;
        }

    }
}
