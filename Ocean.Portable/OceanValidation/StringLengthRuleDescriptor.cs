namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents StringLengthRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class StringLengthRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Gets or sets the allow null String.
        /// </summary>
        /// <value>The allow null String.</value>
        public AllowNullString AllowNullString { get; set; }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public Int32 MaximumLength { get; set; }

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>The minimum length.</value>
        public Int32 MinimumLength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthRuleDescriptor" /> class.
        /// </summary>
        /// <param name="allowNullString">The allow null String.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Either minimumLength must be equal to or greater than negative one or maximumLength must be greater than 0 or maximumLength must be greater than or equal to the MinimumLength
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">allowNullString is not a member of AllowNullString enum.</exception>
        public StringLengthRuleDescriptor(AllowNullString allowNullString, Int32 minimumLength, Int32 maximumLength, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            if (minimumLength >= GlobalConstants.MinusOne) {
                throw new ArgumentOutOfRangeException(nameof(minimumLength), "must be greater than or equal to negative one.");
            }
            if (maximumLength < GlobalConstants.One) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), "must be greater than zero");
            }
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
        /// Initializes a new instance of the <see cref="StringLengthRuleDescriptor" /> class.
        /// </summary>
        /// <param name="allowNullString">The allow null String.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="ArgumentOutOfRangeException">maximumLength - must be greater than zero</exception>
        /// <exception cref="InvalidEnumArgumentException">allowNullString is not a member of AllowNullString enum.</exception>
        public StringLengthRuleDescriptor(AllowNullString allowNullString, Int32 maximumLength, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            if (maximumLength < GlobalConstants.One) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), "must be greater than zero");
            }
            if (!Enum.IsDefined(typeof(AllowNullString), allowNullString)) {
                throw new InvalidEnumArgumentException(nameof(allowNullString), (Int32)allowNullString, typeof(AllowNullString));
            }

            this.MinimumLength = GlobalConstants.MinusOne;
            this.MaximumLength = maximumLength;
            this.AllowNullString = allowNullString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentException">
        /// Either e.maximumLength must be greater than zero or e.maximumLength must be greater than or equal to the e.MinimumLength
        /// </exception>
        public StringLengthRuleDescriptor(StringLengthValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e.MaximumLength < GlobalConstants.One) {
                throw new ArgumentException("e.maximumLength must be greater than zero");
            }

            if (e.MaximumLength < e.MinimumLength) {
                throw new ArgumentException("e.maximumLength must be greater than or equal to the e.MinimumLength");
            }
            this.AllowNullString = e.AllowNullString;
            this.MaximumLength = e.MaximumLength;
            this.MinimumLength = e.MinimumLength;
        }

    }
}
