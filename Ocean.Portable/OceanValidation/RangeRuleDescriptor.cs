namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents RangeRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    /// <seealso cref="RuleDescriptorBase" />
    public class RangeRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Gets or sets the type of the lower range boundary.
        /// </summary>
        /// <value>The type of the lower range boundary.</value>
        public RangeBoundaryType LowerRangeBoundaryType { get; set; }

        /// <summary>
        /// Gets or sets the lower value.
        /// </summary>
        /// <value>The lower value.</value>
        public IComparable LowerValue { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        /// <summary>
        /// Gets or sets the type of the upper range boundary.
        /// </summary>
        /// <value>The type of the upper range boundary.</value>
        public RangeBoundaryType UpperRangeBoundaryType { get; set; }

        /// <summary>
        /// Gets or sets the upper value.
        /// </summary>
        /// <value>The upper value.</value>
        public IComparable UpperValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">e cannot be null.</exception>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public RangeRuleDescriptor(RangeValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            this.LowerRangeBoundaryType = e.LowerRangeBoundaryType;
            this.UpperRangeBoundaryType = e.UpperRangeBoundaryType;
            this.LowerValue = e.LowerValue;
            this.UpperValue = e.UpperValue;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRuleDescriptor" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeRuleDescriptor(RangeBoundaryType lowerRangeBoundaryType, RequiredEntry requiredEntry, RangeBoundaryType upperRangeBoundaryType, IComparable lowerValue, IComparable upperValue, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperValue = upperValue;
        }

    }
}
