namespace Ocean.Portable.InputStringFormatting {
    using System;
    using Ocean.Portable.ComponentModel;

    /// <summary>
    /// Represents FormatText, provides text formatting 
    /// </summary>
    public static class FormatText {

        /// <summary>
        /// Initializes the <see cref="FormatText"/> class.
        /// </summary>
        static FormatText() {
        }

        /// <summary>
        /// Corrects the text character casing and optionally format phone fields similar to Microsoft Outlook.
        /// </summary>
        /// <param name="inputString">String to be case corrected and optionally formatted.</param>
        /// <param name="characterCase">Character case and format.</param>
        /// <returns>String case corrected and optionally formatted.</returns>
        /// <exception cref="InvalidEnumArgumentException">characterCase is not a member of CharacterCase enum.</exception>
        public static String ApplyCharacterCasing(String inputString, CharacterCasing characterCase) {
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCase)) {
                throw new InvalidEnumArgumentException(nameof(characterCase), (Int32)characterCase, typeof(CharacterCasing));
            }

            inputString = inputString.Trim();

            if (inputString.Length == 0) {
                return inputString;
            }

            Int32 intX;

            switch (characterCase) {
                case CharacterCasing.None:
                    return inputString;

                case CharacterCasing.LowerCase:
                    return inputString.ToLower();

                case CharacterCasing.UpperCase:
                    return inputString.ToUpper();

                case CharacterCasing.OutlookPhoneNoProperName:
                    return FormatOutLookPhone(inputString);

                case CharacterCasing.OutlookPhoneUpper:
                    return FormatOutLookPhone(inputString).ToUpper();
            }

            inputString = inputString.ToLower();

            String previous = " ";
            String previousTwo = "  ";
            String previousThree = "   ";
            String charString;

            for (intX = 0; intX < inputString.Length; intX++) {
                charString = inputString.Substring(intX, 1);

                if (Char.IsLetter(Convert.ToChar(charString)) && charString != charString.ToUpper()) {
                    if (previous == " " || previous == "." || previous == "-" || previous == "/" || previousThree == " O'" || previousTwo == "Mc") {
                        inputString = inputString.Remove(intX, 1);
                        inputString = inputString.Insert(intX, charString.ToUpper());
                        previous = charString.ToUpper();
                    } else {
                        previous = charString;
                    }
                } else {
                    previous = charString;
                }

                previousTwo = previousTwo.Substring(1, 1) + previous;
                previousThree = previousThree.Substring(1, 1) + previousThree.Substring(2, 1) + previous;
            }

            intX = inputString.IndexOf("'", StringComparison.Ordinal);

            if (intX == 1) {
                String insertString = inputString.Substring(2, 1).ToUpper();
                inputString = inputString.Remove(2, 1);
                inputString = inputString.Insert(2, insertString);
            }

            try {
                intX = inputString.IndexOf("'", 3, StringComparison.Ordinal);

                if (intX > 3 && inputString.Substring(intX - 2, 1) == " ") {
                    String insertString = inputString.Substring(intX + 1, 1).ToUpper();
                    inputString = inputString.Remove(intX + 1, 1);
                    inputString = inputString.Insert(intX + 1, insertString);
                }

// ReSharper disable EmptyGeneralCatchClause
            } catch {
// ReSharper restore EmptyGeneralCatchClause
            }

            //never remove this code
            inputString += " ";

            foreach (CharacterCasingCheck check in CharacterCasingChecks.GetChecks()) {
                if (inputString.Contains(check.LookFor)) {
                    Int32 foundIndex = inputString.IndexOf(check.LookFor, StringComparison.Ordinal);

                    if (foundIndex > -1) {
                        inputString = inputString.Remove(foundIndex, check.LookFor.Length);
                        inputString = inputString.Insert(foundIndex, check.ReplaceWith);
                    }
                }
            }

            if (characterCase == CharacterCasing.OutlookPhoneProperName) {
                inputString = FormatOutLookPhone(inputString);
            }

            return inputString.Trim();
        }

        static String FormatOutLookPhone(String inputString) {
            if (inputString.Trim().Length == 0) {
                return inputString;
            }

            String tempCasted = inputString + " ";

            try {
                String temp = tempCasted;
                Int32 intX = temp.IndexOf(" ", 8, StringComparison.Ordinal);

                if (intX > 0) {
                    temp = inputString.Substring(0, intX);
                    temp = temp.Replace("(", "");
                    temp = temp.Replace(")", "");
                    temp = temp.Replace(" ", "");
                    temp = temp.Replace("-", "");

                    Int64 lngTemp;

                    if (Int64.TryParse(temp, out lngTemp) && temp.Length == 10) {
                        tempCasted = lngTemp.ToString("(###) ###-####") + "  " + tempCasted.Substring(intX).Trim();
                    }
                }

// ReSharper disable EmptyGeneralCatchClause
            } catch {
// ReSharper restore EmptyGeneralCatchClause
            }

            return tempCasted;
        }

    }
}
