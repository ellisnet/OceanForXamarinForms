namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents RegularExpressionValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RegularExpressionValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the custom regular expression pattern.
        /// </summary>
        /// <value>The custom regular expression pattern.</value>
        public String CustomRegularExpressionPattern { get; private set; }

        /// <summary>
        /// Gets the type of the regular expression pattern.
        /// </summary>
        /// <value>The type of the regular expression pattern.</value>
        public RegularExpressionPatternType RegularExpressionPatternType { get; private set; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidatorAttribute"/> class.
        /// </summary>
        /// <param name="regularExpressionPatternType">Type of the regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">regularExpressionPatternType is not member of RegularExpressionPatternType enum.</exception>
        public RegularExpressionValidatorAttribute(RegularExpressionPatternType regularExpressionPatternType, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(RegularExpressionPatternType), regularExpressionPatternType)) {
                throw new InvalidEnumArgumentException(nameof(regularExpressionPatternType), (Int32)regularExpressionPatternType, typeof(RegularExpressionPatternType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.RegularExpressionPatternType = regularExpressionPatternType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidatorAttribute" /> class.
        /// </summary>
        /// <param name="customRegularExpressionPattern">The custom regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidOperationException">customRegularExpressionPattern is not a valid regular expression.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        public RegularExpressionValidatorAttribute(String customRegularExpressionPattern, RequiredEntry requiredEntry) {
            if (!(StringValidationRules.IsRegularExpressionPatternValid(customRegularExpressionPattern))) {
                throw new ArgumentOutOfRangeException(nameof(customRegularExpressionPattern), "customRegularExpressionPattern is not a valid regular expression.");
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.RegularExpressionPatternType = RegularExpressionPatternType.Custom;
            this.RequiredEntry = requiredEntry;
            this.CustomRegularExpressionPattern = customRegularExpressionPattern;
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Regular expression <see cref="Validator" /> for the decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(StringValidationRules.RegularExpressionRule, new RegularExpressionRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
