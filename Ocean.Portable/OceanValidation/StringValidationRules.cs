namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Ocean.Portable.Infrastructure;

    /// <summary>
    /// Represents StringValidationRules
    /// </summary>
    public class StringValidationRules {

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValidationRules"/> class.
        /// </summary>
        public StringValidationRules() {
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the bank routing number rule for the specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="BankRoutingNumberRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the bank routing number rule for the specified property; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
        /// <exception cref="NotSupportedException">Bank routing number validation rule can only be applied to a String</exception>
        public static Boolean BankRoutingNumberRule(Object target, RuleDescriptorBase ruleDescriptor) {
            var args = ruleDescriptor as BankRoutingNumberRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule {ruleDescriptor.GetType()}, passed to BankRoutingNumberRule");
            }

            var objPi = PclReflection.GetPropertyInfo(target, args.PropertyName);

            if (objPi.PropertyType != typeof(String)) {
                throw new NotSupportedException("Bank routing number validation rule can only be applied to a String");
            }

            String bankRoutingNumber = Convert.ToString(objPi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (String.IsNullOrEmpty(bankRoutingNumber)) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} null or empty but is a required field";
                    return false;
                }
            } else {
                if (String.IsNullOrEmpty(bankRoutingNumber)) {
                    return true;
                }
            }

            var lengthBankRoutingNumber = bankRoutingNumber.Length;
            var value = 0;

            if (lengthBankRoutingNumber != 9) {
                args.BrokenRuleDescription = $"The entered value {bankRoutingNumber} is not a valid bank routing number.  All bank routing numbers are 9 digit in length";
                return false;
            }

            if (Int32.Parse(bankRoutingNumber.Substring(0, 1)) > 1) {
                args.BrokenRuleDescription = $"The entered value {bankRoutingNumber} is not a valid bank routing number. The first digit must be a 0 or a 1";
                return false;
            }

            if (bankRoutingNumber.Cast<Char>().Any(c => !Char.IsDigit(c))) {
                args.BrokenRuleDescription = $"The entered value {bankRoutingNumber} is not a valid bank routing number. Only numeric input is allowed";
                return false;
            }

            for (var intX = 0; intX <= 8; intX += 3) {
                value += Int32.Parse(bankRoutingNumber.Substring(intX, 1)) * 3;
                value += Int32.Parse(bankRoutingNumber.Substring(intX + 1, 1)) * 7;
                value += Int32.Parse(bankRoutingNumber.Substring(intX + 2, 1));
            }

            if (value % 10 != 0) {
                args.BrokenRuleDescription = $"The entered value {bankRoutingNumber} is not a valid bank routing number";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the credit card number rule for the specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="CreditCardNumberRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the credit card number rule for the specified property; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
        /// <exception cref="NotSupportedException">Credit card number validation rule can only be applied to String properties</exception>
        public static Boolean CreditCardNumberRule(Object target, RuleDescriptorBase ruleDescriptor) {
            var args = ruleDescriptor as CreditCardNumberRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to CreditCardNumberRule {ruleDescriptor.GetType()}");
            }

            var objPi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            if (objPi.PropertyType != typeof(String)) {
                throw new NotSupportedException("Credit card number validation rule can only be applied to String properties");
            }

            var cardNumber = Convert.ToString(objPi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (String.IsNullOrEmpty(cardNumber)) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} was null or empty but is a required field";
                    return false;
                }
            } else {
                if (String.IsNullOrEmpty(cardNumber)) {
                    return true;
                }
            }

            var lengthCreditCardNumber = cardNumber.Length;
            var cardArray = new Int32[lengthCreditCardNumber + 1];
            Int32 value;

            if (cardNumber.Cast<Char>().Any(c => !Char.IsDigit(c))) {
                args.BrokenRuleDescription = $"The entered value {cardNumber} is not a valid credit card number. Only numeric input is allowed";
                return false;
            }

            for (var count = lengthCreditCardNumber - 1; count >= 1; count -= 2) {
                value = Convert.ToInt32(cardNumber.Substring(count - 1, 1)) * 2;
                cardArray[count] = value;
            }

            value = 0;

            var start = lengthCreditCardNumber % 2 == 0 ? 2 : 1;

            for (var count = start; count <= lengthCreditCardNumber; count += 2) {
                value = value + Convert.ToInt32(cardNumber.Substring(count - 1, 1));
                var arrValue = cardArray[count - 1];

                if (arrValue < 10) {
                    value = value + arrValue;
                } else {
                    value = value + Convert.ToInt32(Convert.ToString(arrValue).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(arrValue).Substring(Convert.ToString(arrValue).Length - 1));
                }
            }

            if (value % 10 != 0) {
                args.BrokenRuleDescription = $"The entered value {cardNumber} is not a valid credit card number";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the regular expression pattern is valid.
        /// </summary>
        /// <param name="regularExpressionPattern">The regular expression pattern.</param>
        /// <returns>
        /// 	<c>true</c> if the regular expression pattern is valid; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsRegularExpressionPatternValid(String regularExpressionPattern) {
            var returnValue = false;

            try {
                // ReSharper disable once ObjectCreationAsStatement
                new Regex(regularExpressionPattern);
                returnValue = true;

                // ReSharper disable EmptyGeneralCatchClause
            } catch {
                // ReSharper restore EmptyGeneralCatchClause
            }

            return returnValue;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the postal code rule for the specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="StateAbbreviationRuleDescriptor" />.</param>
        /// <returns><c>True</c> does the target property specified in the ruleDescriptor meet the postal code rule for the specified property; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
        /// <exception cref="NotSupportedException">Postal code validation rule can only be applied to String properties</exception>
        public static Boolean PostalCodeRule(Object target, RuleDescriptorBase ruleDescriptor) {
            var args = ruleDescriptor as PostCodeRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to PostalCodeRule: {ruleDescriptor.GetType()}");
            }

            if (args.ValidateForUnitedStatesOnly == ValidateUnitedStatesOnly.Yes) {
                var pi = PclReflection.GetPropertyInfo(target, "Country");
                if (pi == null) {
                    return true;
                }
                if (Convert.ToString(pi.GetValue(target, null)) != "United States") {
                    return true;
                }
            }

            var objPi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            if (objPi.PropertyType != typeof(String)) {
                throw new NotSupportedException("Postal code validation rule can only be applied to String properties");
            }

            var postalCode = Convert.ToString(objPi.GetValue(target, null)).Trim();

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (String.IsNullOrEmpty(postalCode)) {
                    ruleDescriptor.BrokenRuleDescription = $"Postal code was null or empty but is a required field: {RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)}";
                    return false;
                }
            } else {
                if (String.IsNullOrEmpty(postalCode)) {
                    return true;
                }
            }

            const String pattern = "^\\d{5}(-\\d{4})?$";
            var brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required Zip Code pattern";

            if (Regex.IsMatch(postalCode, pattern, RegexOptions.IgnoreCase)) {
                return true;
            }
            ruleDescriptor.BrokenRuleDescription = brokenRuleDescription;
            return false;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the regular expression rule for the specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="RegularExpressionRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the state abbreviation rule for the specified property; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
        /// <exception cref="InvalidOperationException">
        /// CustomRegularExpressionPattern not supplied
        /// or
        /// CustomRegularExpressionPattern is not a valid RegEx pattern.
        /// </exception>
        /// <exception cref="OverflowException">RegularExpressionPatternType type not programmed</exception>
        public static Boolean RegularExpressionRule(Object target, RuleDescriptorBase ruleDescriptor) {
            //great site for patterns
            //http://regexlib.com/Search.aspx
            //
            var args = ruleDescriptor as RegularExpressionRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to RegularExpressionRule {ruleDescriptor.GetType()}");
            }

            var objPi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            var testMe = Convert.ToString(objPi.GetValue(target, null));

            if (args.RegularExpressionPatternType == RegularExpressionPatternType.Custom && String.IsNullOrEmpty(args.CustomRegularExpressionPattern)) {
                throw new InvalidOperationException("CustomRegularExpressionPattern not supplied");
            }

            if (!IsRegularExpressionPatternValid(args.CustomRegularExpressionPattern)) {
                throw new InvalidOperationException("CustomRegularExpressionPattern is not a valid RegEx pattern.");
            }

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (String.IsNullOrWhiteSpace(testMe)) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} was null or empty but is a required field";
                    return false;
                }
            } else {
                if (testMe.Trim().Length == 0) {
                    return true;
                }
            }

            String pattern;
            String brokenRuleDescription;

            switch (args.RegularExpressionPatternType) {
                case RegularExpressionPatternType.Custom:
                    pattern = args.CustomRegularExpressionPattern;
                    brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required {pattern} pattern";

                    break;
                case RegularExpressionPatternType.Email:
                    pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
                    brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required email pattern";

                    break;
                case RegularExpressionPatternType.IPAddress:
                    pattern = "^((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])$";
                    brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required IP Address pattern";

                    break;
                case RegularExpressionPatternType.SSN:
                    pattern = "^\\d{3}-\\d{2}-\\d{4}$";
                    brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required SSN pattern";

                    break;
                case RegularExpressionPatternType.Url:
                    pattern = "(?#WebOrIP)((?#protocol)((news|nntp|telnet|http|ftp|https|ftps|sftp):\\/\\/)?(?#subDomain)(([a-zA-Z0-9]+\\.*(?#domain)[a-zA-Z0-9\\-]+(?#TLD)(\\.[a-zA-Z]+){1,2})|(?#IPAddress)((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])))+(?#Port)(:[1-9][0-9]*)?)+(?#Path)((\\/((?#dirOrFileName)[a-zA-Z0-9 \\-\\%\\~\\+]+)?)*)?(?#extension)(\\.([a-zA-Z0-9 ]+))?(?#parameters)(\\?([a-zA-Z0-9 \\-]+\\=[a-z-A-Z0-9 \\-\\%\\~\\+]+)?(?#additionalParameters)(\\&([a-zA-Z0-9 \\-]+\\=[a-z-A-Z0-9 \\-\\%\\~\\+]+)?)*)?";
                    brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required URL pattern";

                    break;
                case RegularExpressionPatternType.ZipCode:
                    pattern = "^\\d{5}(-\\d{4})?$";
                    brokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} did not match the required Zip Code pattern";

                    break;
                default:
                    throw new OverflowException("RegularExpressionPatternType type not programmed");
            }

            if (Regex.IsMatch(testMe, pattern, RegexOptions.IgnoreCase)) {
                return true;
            }
            ruleDescriptor.BrokenRuleDescription = brokenRuleDescription;
            return false;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the state abbreviation rule for the specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="StateAbbreviationRuleDescriptor" />.</param>
        /// <returns><c>True</c> does the target property specified in the ruleDescriptor meets the state abbreviation rule for the specified property; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
        /// <exception cref="NotSupportedException">State abbreviation validation rule can only be applied to String properties</exception>
        public static Boolean StateAbbreviationRule(Object target, RuleDescriptorBase ruleDescriptor) {
            var args = ruleDescriptor as StateAbbreviationRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to StateAbbreviationRule {ruleDescriptor.GetType()}");
            }

            if (args.ValidateForUnitedStatesOnly == ValidateUnitedStatesOnly.Yes) {
                var pi = PclReflection.GetPropertyInfo(target, "Country");
                if (pi == null) {
                    return true;
                }
                if (Convert.ToString(pi.GetValue(target, null)) != "United States") {
                    return true;
                }
            }
            var objPi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            if (objPi.PropertyType != typeof(String)) {
                throw new NotSupportedException("State abbreviation validation rule can only be applied to String properties");
            }

            var state = Convert.ToString(objPi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.Yes) {
                if (String.IsNullOrEmpty(state)) {
                    ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} was null or empty but is a required field";
                    return false;
                }
            } else {
                if (String.IsNullOrEmpty(state)) {
                    return true;
                }
            }

            if (StateAbbreviationValidator.CreateInstance().IsValid(state)) {
                return true;
            }
            args.BrokenRuleDescription = $"The entered value {state} is not a valid state abbreviation";
            return false;
        }

        /// <summary>
        /// Validation rule that checks if the target property specified in the ruleDescriptor meets the String length criteria for the specified property.
        /// </summary>
        /// <param name="target">The target instance.</param>
        /// <param name="ruleDescriptor">The ruleDescriptor of type <see cref="StringLengthRuleDescriptor" />.</param>
        /// <returns><c>True</c> is the target property specified in the ruleDescriptor meets the String length criteria for the specified property; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException">wrong rule passed to ruleDescriptor</exception>
        public static Boolean StringLengthRule(Object target, RuleDescriptorBase ruleDescriptor) {
            var args = ruleDescriptor as StringLengthRuleDescriptor;

            if (args == null) {
                throw new ArgumentException($"Wrong rule passed to StringLengthRule {ruleDescriptor.GetType()}");
            }

            var objPi = PclReflection.GetPropertyInfo(target, args.PropertyName);
            var testMe = Convert.ToString(objPi.GetValue(target, null));

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (args.AllowNullString == AllowNullString.No && (testMe == null)) {
                // ReSharper restore ConditionIsAlwaysTrueOrFalse
                ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} can not be null";
                return false;
            }

            if (args.AllowNullString == AllowNullString.Yes && String.IsNullOrWhiteSpace(testMe)) {
                return true;
            }

            if (String.IsNullOrEmpty(testMe)) {
                testMe = String.Empty;
            }

            if (args.MinimumLength > 0 && testMe.Length < args.MinimumLength) {
                ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} can not be less than {args.MinimumLength} character's long";
                return false;
            }

            if (args.MaximumLength > 0 && testMe.Length > args.MaximumLength) {
                ruleDescriptor.BrokenRuleDescription = $"{RuleDescriptorBase.GetPropertyFriendlyName(ruleDescriptor)} can not be greater than {args.MaximumLength} character's long";
                return false;
            }

            return true;
        }

    }
}
