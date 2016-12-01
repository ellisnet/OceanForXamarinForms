namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Class ComparePasswordValidatorAttribute.
    /// </summary>
    /// <seealso cref="Ocean.Portable.OceanValidation.BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ComparePasswordValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePasswordValidatorAttribute"/> class.
        /// </summary>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <exception cref="ArgumentException">compareToPropertyName cannot be null or white space.</exception>
        public ComparePasswordValidatorAttribute(String compareToPropertyName) {
            if (String.IsNullOrWhiteSpace(compareToPropertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(compareToPropertyName));
            }
            this.CompareToPropertyName = compareToPropertyName;
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Compare property <see cref="Validator" /> for decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(ComparisonValidationRules.ComparePasswordRule, new ComparePasswordRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
