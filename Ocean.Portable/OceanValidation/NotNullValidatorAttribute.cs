namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Represents NotNullValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotNullValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullValidatorAttribute"/> class.
        /// </summary>
        public NotNullValidatorAttribute() {
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Not null <see cref="Validator" /> for the decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(ComparisonValidationRules.NotNullRule, new NotNullRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
