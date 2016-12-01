namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Reflection;
    using Ocean.Portable.Infrastructure;

    /// <summary>
    /// Represents ComparisonValidationRules
    /// </summary>
    public class ComparisonValidationRules {

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonValidationRules"/> class.
        /// </summary>
        public ComparisonValidationRules() {
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor is equal to the other specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="ComparePasswordRuleDescriptor" />.</param>
        /// <returns><c>True</c> if the target property specified in the ruleDescriptor is equal to the other specified property, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">target or ruleDescriptor is null.</exception>
        /// <exception cref="ArgumentException">Wrong rule passed to ComparePropertyRule</exception>
        public static Boolean ComparePasswordRule(Object target, RuleDescriptorBase ruleDescriptor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }
            var args = ruleDescriptor as ComparePasswordRuleDescriptor;
            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to ComparePasswordRule {ruleDescriptor.GetType()}");
            }

            PropertyInfo pi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            var source = pi.GetValue(target, null);
            if (source == null) {
                return true;
            }

            pi = PclReflection.GetPropertyInfo(target, args.CompareToPropertyName);
            var testAgainst = pi.GetValue(target, null);

            if (testAgainst == null) {
                return true;
            }

            var iSource = (IComparable)source;
            var iTestAgainst = (IComparable)testAgainst;
            var result = iSource.CompareTo(iTestAgainst);

            if (result == 0) {
                return true;
            }
            ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be equal to {InputStringFormatting.CamelCaseString.GetWords(args.CompareToPropertyName)}.";
            return false;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the comparison criteria against another specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="ComparePropertyRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the comparison criteria against another specified property, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">target or ruleDescriptor is null.</exception>
        /// <exception cref="ArgumentException">Wrong rule passed to ComparePropertyRule</exception>
        /// <exception cref="OverflowException">comparison type not programmed</exception>
        public static Boolean ComparePropertyRule(Object target, RuleDescriptorBase ruleDescriptor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }
            var args = ruleDescriptor as ComparePropertyRuleDescriptor;
            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to ComparePropertyRule {ruleDescriptor.GetType()}");
            }

            PropertyInfo pi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            Object source = pi.GetValue(target, null);
            var stringSource = source as String;

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (source == null || (stringSource != null && String.IsNullOrWhiteSpace(stringSource))) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} was null or empty but is a required field.";
                    return false;
                }
            } else {
                if (source == null) {
                    return true;
                }
            }

            pi = PclReflection.GetPropertyInfo(target, args.CompareToPropertyName);
            Object testAgainst = pi.GetValue(target, null);

            if (testAgainst == null) {
                return true;
            }

            var iSource = (IComparable)source;
            var iTestAgainst = (IComparable)testAgainst;
            Int32 result = iSource.CompareTo(iTestAgainst);

            switch (args.ComparisonType) {
                case ComparisonType.Equal:

                    if (result == 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be equal to {testAgainst}.";
                    return false;

                case ComparisonType.GreaterThan:

                    if (result > 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be greater than {testAgainst}.";
                    return false;

                case ComparisonType.GreaterThanEqual:

                    if (result >= 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be greater than or equal to {testAgainst}.";
                    return false;

                case ComparisonType.LessThan:

                    if (result < 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be less than {testAgainst}.";
                    return false;

                case ComparisonType.LessThanEqual:

                    if (result <= 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be less than or equal to {testAgainst}.";
                    return false;

                case ComparisonType.NotEqual:

                    if (result != 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must not equal {testAgainst}.";
                    return false;

                default:
                    throw new OverflowException("comparison type not programmed");
            }
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the comparison criteria for the property's value.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="RangeRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the comparison criteria, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">target or ruleDescriptor is null.</exception>
        /// <exception cref="ArgumentException">If the ruleDescriptor is not of type <see cref="RangeRuleDescriptor" />.</exception>
        /// <exception cref="OverflowException">comparison type not programmed</exception>
        public static Boolean CompareValueRule(Object target, RuleDescriptorBase ruleDescriptor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }
            var args = ruleDescriptor as CompareValueRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to CompareValueRule {ruleDescriptor.GetType()}");
            }

            PropertyInfo pi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            Object source = pi.GetValue(target, null);
            var stringSource = source as String;

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (source == null || (stringSource != null && String.IsNullOrWhiteSpace(stringSource))) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} was null or empty but is a required field.";
                    return false;
                }
            } else {
                if (source == null) {
                    return true;
                }
            }

            Object testAgainst = args.CompareToValue;
            var iSource = (IComparable)source;
            Int32 result = iSource.CompareTo(args.CompareToValue);

            switch (args.ComparisonType) {
                case ComparisonType.Equal:

                    if (result == 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be equal to {testAgainst}.";
                    return false;

                case ComparisonType.GreaterThan:

                    if (result > 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be greater than {testAgainst}.";
                    return false;

                case ComparisonType.GreaterThanEqual:

                    if (result >= 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be greater than or equal to {testAgainst}.";
                    return false;

                case ComparisonType.LessThan:

                    if (result < 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be less than {testAgainst}.";
                    return false;

                case ComparisonType.LessThanEqual:

                    if (result <= 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be less than or equal to {testAgainst}.";
                    return false;

                case ComparisonType.NotEqual:

                    if (result != 0) {
                        return true;
                    }
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must not equal {testAgainst}.";
                    return false;

                default:
                    throw new OverflowException("comparison type not programmed");
            }
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor is within the specified range.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="RangeRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor is within the specified range, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">target or ruleDescriptor is null.</exception>
        /// <exception cref="ArgumentException">Wrong rule passed to InRangeRule</exception>
        public static Boolean InRangeRule(Object target, RuleDescriptorBase ruleDescriptor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }

            var args = ruleDescriptor as RangeRuleDescriptor;
            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to InRangeRule {ruleDescriptor.GetType()}");
            }

            PropertyInfo pi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            Object source = pi.GetValue(target, null);
            var stringSource = source as String;

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (source == null || (stringSource != null && String.IsNullOrWhiteSpace(stringSource))) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} was null or empty but is a required field.";
                    return false;
                }
            } else {
                if (source == null) {
                    return true;
                }
            }

            var iSource = (IComparable)source;
            Object lower = args.LowerValue;
            Object upper = args.UpperValue;
            Int32 lowerResult = iSource.CompareTo(args.LowerValue);

            if (args.LowerRangeBoundaryType == RangeBoundaryType.Inclusive) {
                if (lowerResult < 0) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be greater than or equal to {lower}";
                    return false;
                }
            } else {
                if (lowerResult <= 0) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be greater than {lower}";
                    return false;
                }
            }

            Int32 upperResult = iSource.CompareTo(args.UpperValue);

            if (args.UpperRangeBoundaryType == RangeBoundaryType.Inclusive) {
                if (upperResult > 0) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be less than {upper}";
                    return false;
                }
            } else {
                if (upperResult >= 0) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} must be less than or equal to {upper}";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor is not null.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="NotNullRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor is not null, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Either target or ruleDescriptor is null.</exception>
        /// <exception cref="ArgumentException">If the ruleDescriptor is not of type <see cref="NotNullRuleDescriptor" />.</exception>
        public static Boolean NotNullRule(Object target, RuleDescriptorBase ruleDescriptor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (ruleDescriptor == null) {
                throw new ArgumentNullException(nameof(ruleDescriptor));
            }

            //Boxing and Unboxing
            //When a nullable type is boxed, the common language runtime automatically boxes the underlying value of the 
            //Nullable Object, not the Nullable Object itself. That is, if the HasValue property is true, the contents 
            //of the Value property is boxed. If the HasValue property is false, a null reference (Nothing in Visual Basic) is boxed. 
            //When the underlying value of a nullable type is un-boxed, the common language runtime creates a new 
            //Nullable structure initialized to the underlying value. 
            var args = ruleDescriptor as NotNullRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to NotNullRule {ruleDescriptor.GetType()}");
            }

            PropertyInfo pi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            Object source = pi.GetValue(target, null);

            //this handles both Nullable and standard uninitialized values
            if (source == null) {
                ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} is null.";
                return false;
            }

            return true;
        }

    }
}
