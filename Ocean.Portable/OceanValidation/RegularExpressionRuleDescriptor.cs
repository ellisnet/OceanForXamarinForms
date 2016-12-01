namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents RegularExpressionRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class RegularExpressionRuleDescriptor : RuleDescriptorBase {

        String _customRegularExpressionPattern = String.Empty;

        /// <summary>
        /// Gets or sets the custom regular expression pattern.
        /// </summary>
        /// <value>The custom regular expression pattern.</value>
        /// <exception cref="InvalidOperationException">
        /// Either before setting a custom pattern, the Pattern Type must be custom or value is not a valid regular expression.
        /// </exception>
        public String CustomRegularExpressionPattern {
            get { return _customRegularExpressionPattern; }
            set {
                if (String.IsNullOrEmpty(value)) {
                    _customRegularExpressionPattern = String.Empty;
                    return;
                }

                if (this.RegularExpressionPatternType != RegularExpressionPatternType.Custom) {
                    throw new InvalidOperationException("Before setting a custom pattern, the Pattern Type must be custom.");
                }

                if (!(StringValidationRules.IsRegularExpressionPatternValid(value))) {
                    throw new InvalidOperationException("value is not a valid regular expression.");
                }

                _customRegularExpressionPattern = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the regular expression pattern.
        /// </summary>
        /// <value>The type of the regular expression pattern.</value>
        public RegularExpressionPatternType RegularExpressionPatternType { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionRuleDescriptor" /> class.
        /// </summary>
        /// <param name="regularExpressionPatternType">Type of the regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="InvalidOperationException">This constructor does not support assigning the RegularExpressionPatternType to Custom.  Use other constructor.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">regularExpressionPatternType is not member of RegularExpressionPatternType enum.</exception>
        public RegularExpressionRuleDescriptor(RegularExpressionPatternType regularExpressionPatternType, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(customMessage, propertyFriendlyName, propertyName, ruleSet, overrideMessage) {
            if (regularExpressionPatternType == RegularExpressionPatternType.Custom) {
                throw new InvalidOperationException("This constructor does not support assigning the RegularExpressionPatternType to Custom.  Use other constructor.");
            }
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
        /// Initializes a new instance of the <see cref="RegularExpressionRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        /// <exception cref="InvalidOperationException">e.CustomRegularExpressionPattern is not a valid regular expression.</exception>
        public RegularExpressionRuleDescriptor(RegularExpressionValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            this.RegularExpressionPatternType = e.RegularExpressionPatternType;

            if (e.RegularExpressionPatternType == RegularExpressionPatternType.Custom) {
                if (!(StringValidationRules.IsRegularExpressionPatternValid(e.CustomRegularExpressionPattern))) {
                    throw new InvalidOperationException("e.CustomRegularExpressionPattern is not a valid regular expression.");
                }

                _customRegularExpressionPattern = e.CustomRegularExpressionPattern;
            }

            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionRuleDescriptor" /> class.
        /// </summary>
        /// <param name="customRegularExpressionPattern">The custom regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="InvalidOperationException">customRegularExpressionPattern is not a valid regular expression.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        public RegularExpressionRuleDescriptor(String customRegularExpressionPattern, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(customMessage, propertyFriendlyName, propertyName, ruleSet, overrideMessage) {
            this.RegularExpressionPatternType = RegularExpressionPatternType.Custom;
            this.RequiredEntry = requiredEntry;

            if (!(StringValidationRules.IsRegularExpressionPatternValid(customRegularExpressionPattern))) {
                throw new InvalidOperationException("customRegularExpressionPattern is not a valid regular expression.");
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            _customRegularExpressionPattern = customRegularExpressionPattern;
        }

    }
}
