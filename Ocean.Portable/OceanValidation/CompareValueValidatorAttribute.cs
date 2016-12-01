namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents CompareValueValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class CompareValueValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets the compare to value.
        /// </summary>
        /// <value>The compare to value.</value>
        public IComparable CompareToValue { get; }

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
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Double compareToValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int32 compareToValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int64 compareToValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int16 compareToValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Single compareToValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute" /> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, String compareToValue, RequiredEntry requiredEntry) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// There is some restriction on the data types that can be used as attribute parameters.
        /// Parameters can be any integral data type (Byte, Short, Integer, Long) or floating point data type (Single and Double), as well as Char, String, Boolean, an enumerated type, or System.Type.
        /// Thus, Date, Decimal, Object, and structured types cannot be used as parameters.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="convertToType">Type of the convert to.</param>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">comparisonType is not a member of ComparisonType enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">convertToType is not a member of ConvertToType enum.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, String compareToValue, RequiredEntry requiredEntry, ConvertToType convertToType) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(ConvertToType), convertToType)) {
                throw new InvalidEnumArgumentException(nameof(convertToType), (Int32)convertToType, typeof(ConvertToType));
            }

            this.ComparisonType = comparisonType;

            switch (convertToType) {
                case ConvertToType.Date:
                    this.CompareToValue = Convert.ToDateTime(compareToValue);

                    break;
                case ConvertToType.Decimal:
                    this.CompareToValue = Convert.ToDecimal(compareToValue);

                    break;
            }

            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Compare value <see cref="Validator" /> for decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(ComparisonValidationRules.CompareValueRule, new CompareValueRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
