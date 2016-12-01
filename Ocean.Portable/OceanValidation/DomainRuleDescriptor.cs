namespace Ocean.Portable.OceanValidation {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents DomainRuleDescriptor
    /// </summary>
    /// <seealso cref="RuleDescriptorBase" />
    public class DomainRuleDescriptor : RuleDescriptorBase {

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public String[] Data { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRuleDescriptor" /> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">e cannot be null.</exception>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public DomainRuleDescriptor(DomainValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            this.Data = e.Data;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRuleDescriptor" /> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="ArgumentNullException">data is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">requiredEntry is not member of RequiredEntry enum.</exception>
        public DomainRuleDescriptor(RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage, params String[] data)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.Data = data;
            this.RequiredEntry = requiredEntry;
        }

    }
}
