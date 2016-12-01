namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Linq;
    using System.Reflection;
    using Ocean.Portable.Infrastructure;

    /// <summary>
    /// Represents DomainValidationRules
    /// </summary>
    public class DomainValidationRules {

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidationRules"/> class.
        /// </summary>
        public DomainValidationRules() {
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the domain rule. A domain in this case means an array of valid values.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="DomainRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the domain rule, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">target or ruleDescriptor was null.</exception>
        /// <exception cref="ArgumentException">If the ruleDescriptor is not of type <see cref="DomainRuleDescriptor" />.</exception>
        public static Boolean DomainRule(Object target, RuleDescriptorBase ruleDescriptor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }
            var args = ruleDescriptor as DomainRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to DomainRule: {ruleDescriptor.GetType()}");
            }

            PropertyInfo pi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            String propertyValue = Convert.ToString(pi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.No && String.IsNullOrEmpty(propertyValue)) {
                return true;
            }

            if (args.Data.Any(s => String.Compare(s, propertyValue, StringComparison.OrdinalIgnoreCase) == 0)) {
                return true;
            }

            var sb = new System.Text.StringBuilder(1024);
            sb.AppendFormat("The {0} did not match any of the following acceptable values.", RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor));

            const String format = ", {0}";

            foreach (String s in args.Data) {
                sb.AppendFormat(format, s);
            }

            ruleDescriptor.BrokenRuleDescription = sb.ToString();
            return false;
        }

    }
}
