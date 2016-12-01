namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents RangeValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RangeValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the type of the lower range boundary.
        /// </summary>
        /// <value>The type of the lower range boundary.</value>
        public RangeBoundaryType LowerRangeBoundaryType { get; }

        /// <summary>
        /// Gets the lower value.
        /// </summary>
        /// <value>The lower value.</value>
        public IComparable LowerValue { get; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Gets the type of the upper range boundary.
        /// </summary>
        /// <value>The type of the upper range boundary.</value>
        public RangeBoundaryType UpperRangeBoundaryType { get; }

        /// <summary>
        /// Gets the upper value.
        /// </summary>
        /// <value>The upper value.</value>
        public IComparable UpperValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Double lowerValue, RangeBoundaryType upperRangeBoundaryType, Double upperValue, RequiredEntry requiredEntry) {
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
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int32 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int32 upperValue, RequiredEntry requiredEntry) {
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
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int64 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int64 upperValue, RequiredEntry requiredEntry) {
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
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int16 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int16 upperValue, RequiredEntry requiredEntry) {
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
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Single lowerValue, RangeBoundaryType upperRangeBoundaryType, Single upperValue, RequiredEntry requiredEntry) {
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
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute" /> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, String lowerValue, RangeBoundaryType upperRangeBoundaryType, String upperValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            this.LowerValue = lowerValue;
            this.UpperValue = upperValue;
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Added constructor to support date and decimal types since these are not allowed in a attributes constructor
        /// There is some restriction on the data types that can be used as attribute parameters.
        /// Parameters can be any integral data type (Byte, Short, Integer, Long) or floating point data type (Single and Double), as well as Char, String, Boolean, an enumerated type, or System.Type.
        /// Thus, Date, Decimal, Object, and structured types cannot be used as parameters.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="convertToType">Type of the convert to.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">lowerRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">upperRangeBoundaryType is not member of RangeBoundaryType enum.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, String lowerValue, RangeBoundaryType upperRangeBoundaryType, String upperValue, RequiredEntry requiredEntry, ConvertToType convertToType) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            switch (convertToType) {
                case ConvertToType.Date:
                    this.LowerValue = Convert.ToDateTime(lowerValue);
                    this.UpperValue = Convert.ToDateTime(upperValue);

                    break;
                case ConvertToType.Decimal:
                    this.LowerValue = Convert.ToDecimal(lowerValue);
                    this.UpperValue = Convert.ToDecimal(upperValue);

                    break;
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Range <see cref="Validator" /> for the decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(ComparisonValidationRules.InRangeRule, new RangeRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
