namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents StateAbbreviationRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class StateAbbreviationRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        /// <summary>
        /// Gets or sets the validate for united states only.
        /// </summary>
        /// <value>The validate for united states only.</value>
        public ValidateUnitedStatesOnly ValidateForUnitedStatesOnly { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateAbbreviationRuleDescriptor" /> class.
        /// </summary>
        /// <param name="validateForUnitedStatesOnly">The validate for united states only.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">validateUnitedStatesOnly is not member of ValidateUnitedStatesOnly enum.</exception>
        public StateAbbreviationRuleDescriptor(ValidateUnitedStatesOnly validateForUnitedStatesOnly, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            if (!Enum.IsDefined(typeof(ValidateUnitedStatesOnly), validateForUnitedStatesOnly)) {
                throw new InvalidEnumArgumentException(nameof(validateForUnitedStatesOnly), (Int32)validateForUnitedStatesOnly, typeof(ValidateUnitedStatesOnly));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.RequiredEntry = requiredEntry;
            this.ValidateForUnitedStatesOnly = validateForUnitedStatesOnly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateAbbreviationRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">e cannot be null.</exception>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public StateAbbreviationRuleDescriptor(StateAbbreviationValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            this.RequiredEntry = e.RequiredEntry;
            this.ValidateForUnitedStatesOnly = e.ValidateForUnitedStatesOnly;
        }

    }
}
