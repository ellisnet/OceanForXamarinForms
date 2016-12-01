namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Represents NotNullRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class NotNullRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">e cannot be null.</exception>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public NotNullRuleDescriptor(NotNullValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullRuleDescriptor"/> class.
        /// </summary>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public NotNullRuleDescriptor(String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
        }

    }
}
