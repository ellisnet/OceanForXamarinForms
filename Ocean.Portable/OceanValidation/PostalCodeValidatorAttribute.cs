namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents PostalCodeValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PostalCodeValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Gets or sets the validate for united states only.
        /// </summary>
        /// <value>The validate for united states only.</value>
        public ValidateUnitedStatesOnly ValidateForUnitedStatesOnly { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateAbbreviationValidatorAttribute" /> class.
        /// </summary>
        /// <param name="validateForUnitedStatesOnly">The validate for united states only.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">validateUnitedStatesOnly is not a member of ValidateUnitedStatesOnly enum.</exception>
        public PostalCodeValidatorAttribute(ValidateUnitedStatesOnly validateForUnitedStatesOnly, RequiredEntry requiredEntry) {
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
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>State abbreviation <see cref="Validator" /> for the decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(StringValidationRules.PostalCodeRule, new PostCodeRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
