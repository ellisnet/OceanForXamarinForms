namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents ComparePropertyRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class ComparePropertyRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Gets or sets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; }

        /// <summary>
        /// Gets or sets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePropertyRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public ComparePropertyRuleDescriptor(ComparePropertyValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            this.ComparisonType = e.ComparisonType;
            this.CompareToPropertyName = e.CompareToPropertyName;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePropertyRuleDescriptor" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not member of ComparisonType enum.</exception>
        /// <exception cref="ArgumentException">compareToPropertyName cannot be null or whitespace.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        public ComparePropertyRuleDescriptor(ComparisonType comparisonType, String compareToPropertyName, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
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

    }
}
