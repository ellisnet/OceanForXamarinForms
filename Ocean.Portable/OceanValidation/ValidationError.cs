namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Represents ValidationError
    /// </summary>
    public class ValidationError {

        /// <summary>
        /// Gets or sets the broken rule.
        /// </summary>
        /// <value>The broken rule.</value>
        public IValidationRuleMethod BrokenRule { get; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName => this.BrokenRule.RuleBase.PropertyName;

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public String RuleName => this.BrokenRule.RuleName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError" /> class.
        /// </summary>
        /// <param name="brokenRule">The broken rule.</param>
        /// <exception cref="ArgumentNullException">brokenRule is null</exception>
        public ValidationError(IValidationRuleMethod brokenRule) {
            if (brokenRule == null) {
                throw new ArgumentNullException(nameof(brokenRule));
            }
            this.BrokenRule = brokenRule;
        }

    }
}
