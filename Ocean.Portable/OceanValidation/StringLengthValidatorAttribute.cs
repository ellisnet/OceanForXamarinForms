namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents StringLengthValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class StringLengthValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the allow null String.
        /// </summary>
        /// <value>The allow null String.</value>
        public AllowNullString AllowNullString { get; }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public Int32 MaximumLength { get; }

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>The minimum length.</value>
        public Int32 MinimumLength { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        public StringLengthValidatorAttribute(Int32 maximumLength)
            : this(GlobalConstants.MinusOne, maximumLength, AllowNullString.No) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Either minimumLength - must be greater than or equal to negative one or maximumLength - must be greater than or equal to the MinimumLength
        /// </exception>
        public StringLengthValidatorAttribute(Int32 minimumLength, Int32 maximumLength)
            : this(minimumLength, maximumLength, AllowNullString.No) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute" /> class.
        /// </summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="allowNullString">The allow null String.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Either minimumLength - must be greater than or equal to negative one or maximumLength - must be greater than or equal to the MinimumLength
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">allowNullString is not a member of AllowNullString enum.</exception>
        public StringLengthValidatorAttribute(Int32 minimumLength, Int32 maximumLength, AllowNullString allowNullString) {
            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), "must be greater than or equal to the MinimumLength");
            }
            if (!Enum.IsDefined(typeof(AllowNullString), allowNullString)) {
                throw new InvalidEnumArgumentException(nameof(allowNullString), (Int32)allowNullString, typeof(AllowNullString));
            }

            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
            this.AllowNullString = allowNullString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="allowNullString">The allow null String.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// maximumLength - must be greater than or equal to the MinimumLength
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">allowNullString is not a member of AllowNullString enum.</exception>
        public StringLengthValidatorAttribute(Int32 maximumLength, AllowNullString allowNullString)
            : this(GlobalConstants.MinusOne, maximumLength, allowNullString) {
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>String length <see cref="Validator" /> for decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(StringValidationRules.StringLengthRule, new StringLengthRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
