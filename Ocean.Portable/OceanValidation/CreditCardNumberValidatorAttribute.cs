namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents CreditCardNumberValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CreditCardNumberValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardNumberValidatorAttribute" /> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        public CreditCardNumberValidatorAttribute(RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Credit card number <see cref="Validator" /> for decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(StringValidationRules.CreditCardNumberRule, new CreditCardNumberRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
