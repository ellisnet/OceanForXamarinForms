namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents BankRoutingNumberValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BankRoutingNumberValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankRoutingNumberValidatorAttribute" /> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of enum RequiredEntry.</exception>
        public BankRoutingNumberValidatorAttribute(RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Creates the a validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Bank routing number <see cref="Validator" /> for decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(StringValidationRules.BankRoutingNumberRule, new BankRoutingNumberRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
