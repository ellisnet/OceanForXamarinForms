namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents DomainValidatorAttribute
    /// </summary>
    /// <seealso cref="BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DomainValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public String[] Data { get; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidatorAttribute" /> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="data">The data. Is a param array of strings separated by a comma that represents each valid value in the domain.</param>
        /// <exception cref="ArgumentNullException">data is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        /// <example>
        /// If valid values are Slide, Fade and None then the attribute signature would be:  "Slide", "Fade", "None"
        /// <code>
        /// [DomainValidator(RequiredEntry.Yes, "Slide", "Fade", "None")]
        /// </code></example>
        public DomainValidatorAttribute(RequiredEntry requiredEntry, params String[] data) {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.RequiredEntry = requiredEntry;
            this.Data = new String[data.Length];
            data.CopyTo(this.Data, 0);
        }

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Domain <see cref="Validator" /> for the decorated property.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public override Validator Create(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return new Validator(DomainValidationRules.DomainRule, new DomainRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

    }
}
