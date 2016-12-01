namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Represents a CustomRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class CustomRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRuleDescriptor"/> class.
        /// </summary>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public CustomRuleDescriptor(String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
        }

    }
}
