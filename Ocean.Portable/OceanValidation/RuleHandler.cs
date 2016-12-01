namespace Ocean.Portable.OceanValidation {
    using System;

    /// <summary>
    /// Represents a delegate RuleHandler
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="ruleDescriptor">The rule descriptor.</param>
    /// <returns><c>True</c> when the rule handler is invoked and the rule passes; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
    public delegate Boolean RuleHandler(Object target, RuleDescriptorBase ruleDescriptor);

    /// <summary>
    /// Represents a delegate RuleHandler
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    /// <typeparam name="TR">Rule descriptor type.</typeparam>
    /// <param name="target">The target object.</param>
    /// <param name="ruleDescriptor">The rule descriptor</param>
    /// <returns><c>True</c> when the rule handler is invoked and the rule passes; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
    public delegate Boolean RuleHandler<in T, in TR>(T target, TR ruleDescriptor) where TR : RuleDescriptorBase;
}
