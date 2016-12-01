namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents ComparePropertyValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ComparePropertyValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; }

        /// <summary>
        /// Gets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePropertyValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="ArgumentException">compareToPropertyName cannot be null or whitespace.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        public ComparePropertyValidatorAttribute(ComparisonType comparisonType, String compareToPropertyName, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (String.IsNullOrWhiteSpace(compareToPropertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(compareToPropertyName));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToPropertyName = compareToPropertyName;
            this.RequiredEntry = requiredEntry;
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
            return new Validator(ComparisonValidationRules.ComparePropertyRule, new ComparePropertyRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
