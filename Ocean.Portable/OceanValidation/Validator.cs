namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Reflection;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents Validator
    /// </summary>
    /// <seealso cref="IValidationRuleMethod" />
    public class Validator : IValidationRuleMethod {

        readonly RuleHandler _ruleHandler;

        /// <summary>
        /// Gets the rule base.
        /// </summary>
        /// <value>The rule base.</value>
        public RuleDescriptorBase RuleBase { get; }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public String RuleName { get; }

        /// <summary>
        /// Gets the type of the rule.
        /// </summary>
        /// <value>The type of the rule.</value>
        public RuleType RuleType { get; }

        /// <summary>
        /// Creates a shared or instance validator
        /// </summary>
        /// <param name="ruleHandler">The rule handler.</param>
        /// <param name="ruleDescriptor">The rule descriptor.</param>
        /// <param name="ruleType"><see cref="RuleType" /> of the rule.</param>
        /// <exception cref="ArgumentNullException">
        /// Either ruleHandler or ruleDescriptor is null
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">ruleType is not a member of RuleType enum.</exception>
        public Validator(RuleHandler ruleHandler, RuleDescriptorBase ruleDescriptor, RuleType ruleType) {
            if (ruleHandler == null) {
                throw new ArgumentNullException(nameof(ruleHandler));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }
            if (!Enum.IsDefined(typeof(RuleType), ruleType)) {
                throw new InvalidEnumArgumentException(nameof(ruleType), (Int32)ruleType, typeof(RuleType));
            }
            _ruleHandler = ruleHandler;
            this.RuleBase = ruleDescriptor;
            this.RuleType = ruleType;
            switch (ruleType) {
                case RuleType.Instance:
                    this.RuleName = $"rule://Instance-{ruleHandler.GetMethodInfo().Name}/{ruleDescriptor.PropertyName}-{(Guid.NewGuid())}";
                    break;
                case RuleType.Shared:
                case RuleType.Attribute:
                    this.RuleName = $"rule://{ruleHandler.GetMethodInfo().Name}/{ruleDescriptor.PropertyName}-{Guid.NewGuid()}";
                    break;
            }
        }

        /// <summary>
        /// Invokes the rule handler delegate passing the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><c>True</c> if the invoked validation passes; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">target is null</exception>
        public Boolean Invoke(Object target) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            return _ruleHandler.Invoke(target, this.RuleBase);
        }

    }
}
