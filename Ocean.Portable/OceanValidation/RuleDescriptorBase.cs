namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Represents RuleDescriptorBase
    /// </summary>
    public abstract class RuleDescriptorBase {

        String _brokenRuleDescription = String.Empty;

        /// <summary>
        /// Gets or sets the broken rule description.
        /// </summary>
        /// <value>The broken rule description.</value>
        public String BrokenRuleDescription {
            get { return String.IsNullOrEmpty(_brokenRuleDescription) ? $"Missing Broken Rule Description For {this.PropertyName}" : _brokenRuleDescription; }
            set { _brokenRuleDescription = value; }
        }

        /// <summary>
        /// Gets or sets the custom message.
        /// </summary>
        /// <value>The custom message.</value>
        public String CustomMessage { get; set; }

        /// <summary>
        /// Gets or sets the override message.
        /// </summary>
        /// <value>The override message.</value>
        public String OverrideMessage { get; set; }

        /// <summary>
        /// Friendly name of property for error reporting purposes.  If not provided, this will be generated from the property name by parsing the property name.
        /// </summary>
        /// <value>The name of the property friendly.</value>
        public String PropertyFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the rule set.
        /// </summary>
        /// <value>The rule set.</value>
        public String RuleSet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleDescriptorBase" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        protected RuleDescriptorBase(String propertyName, String propertyFriendlyName, String ruleSet, String customMessage, String overrideMessage) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            this.PropertyName = propertyName;
            this.PropertyFriendlyName = propertyFriendlyName;
            this.RuleSet = ruleSet;
            this.CustomMessage = customMessage;
            this.OverrideMessage = overrideMessage;
        }

        /// <summary>
        /// Gets the friendly name of the property.
        /// </summary>
        /// <param name="ruleDescriptor">The rule descriptor.</param>
        /// <returns>String with either the property friendly name or property name formated as a camel case string with spaces before each upper case letter.</returns>
        /// <exception cref="ArgumentNullException">ruleDescriptor is null.</exception>
        public static String GetPropertyFriendlyName(RuleDescriptorBase ruleDescriptor) {
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }
            return String.IsNullOrEmpty(ruleDescriptor.PropertyFriendlyName) ? InputStringFormatting.CamelCaseString.GetWords(ruleDescriptor.PropertyName) : ruleDescriptor.PropertyFriendlyName;
        }

    }
}
