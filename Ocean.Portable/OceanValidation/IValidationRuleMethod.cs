namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Contract that represents IValidationRuleMethod. This contract is used by all validator attributes.
    /// </summary>
    public interface IValidationRuleMethod {

        /// <summary>
        /// Gets the rule base.
        /// </summary>
        /// <value>The rule base.</value>
        RuleDescriptorBase RuleBase { get; }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        String RuleName { get; }

        /// <summary>
        /// Invokes the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><c>True</c> if the invoked validation passes; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">brokenRule is null</exception>
        Boolean Invoke(Object target);

    }
}
