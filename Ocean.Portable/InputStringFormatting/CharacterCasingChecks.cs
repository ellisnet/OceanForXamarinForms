namespace Ocean.Portable.InputStringFormatting {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CharacterCasingChecks, provides container for all application character casing correction rules.  This class is consumed by the BusinessEntityBase class when it applies CharacterCasingFormatting rules to a property when it's changed.
    /// </summary>
    public class CharacterCasingChecks : List<CharacterCasingCheck> {

        static Func<CharacterCasingChecks> _getChecksSource;

        static CharacterCasingChecks _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCasingChecks"/> class.
        /// </summary>
        CharacterCasingChecks() {
        }

        /// <summary>
        ///     <para>
        ///         Utilizing lazy loading, inInitializes and returns <see cref="CharacterCasingChecks"/> containing a default set of <see cref="CharacterCasingCheck"/> objects; these objects are utilized when a CharacterCasingFormatting rule is applied to a property.
        ///     </para>
        ///     <para>
        ///         If the <see cref="CharacterCasingChecks.SetGetChecksSource"/> method has been called, this method will invoke the source argument each time this method is called.
        ///     </para>
        /// </summary>
        /// <returns>Returns <see cref="CharacterCasingChecks"/> containing a default set of <see cref="CharacterCasingCheck"/> objects.</returns>
        /// <remarks>
        ///     <para>
        ///         Developers you can use the returned value from this methods and add or remove checks. For example, you could load checks from a database, config file, web service, etc.
        ///     </para>
        ///     <para>
        ///         Developers you can also use the <see cref="CharacterCasingChecks.SetGetChecksSource"/> method to create an empty <see cref="CharacterCasingChecks"/> object and then populate it within your application. Note: this technique does not load up any default checks./>
        ///     </para>
        /// </remarks>
        public static CharacterCasingChecks GetChecks() {
            if (_getChecksSource != null) {
                return _getChecksSource.Invoke();
            }

            if (_instance == null) {
// ReSharper disable UseObjectOrCollectionInitializer
                _instance = new CharacterCasingChecks();

                // ReSharper restore UseObjectOrCollectionInitializer
                //TODO: Optionally load this from a data base, config file, web service, etc.
                //
                //You can also add, remove or edit these by modifying the CharacterCasingChecks collection from your application.
                //
                //These are values that are specific to your company or line of business
                //remove the ones that don't apply and add your own.
                //ensure that the lengths of the LookFor and the ReplaceWith strings are the same
                _instance.Add(new CharacterCasingCheck("Po Box", "PO Box"));
                _instance.Add(new CharacterCasingCheck("C/o ", "c/o "));
                _instance.Add(new CharacterCasingCheck("C/O ", "c/o "));
                _instance.Add(new CharacterCasingCheck("Vpn ", "VPN "));
                _instance.Add(new CharacterCasingCheck("Xp ", "XP "));
                _instance.Add(new CharacterCasingCheck(" Or ", " or "));
                _instance.Add(new CharacterCasingCheck(" And ", " and "));
                _instance.Add(new CharacterCasingCheck(" Nw ", " NW "));
                _instance.Add(new CharacterCasingCheck(" Ne ", " NE "));
                _instance.Add(new CharacterCasingCheck(" Sw ", " SW "));
                _instance.Add(new CharacterCasingCheck(" Se ", " SE "));
                _instance.Add(new CharacterCasingCheck(" Llc. ", " LLC. "));
                _instance.Add(new CharacterCasingCheck(" Llc ", " LLC "));
                _instance.Add(new CharacterCasingCheck(" Lc ", " LC "));
                _instance.Add(new CharacterCasingCheck(" Lc. ", " LC. "));
                _instance.Add(new CharacterCasingCheck("Wpf", "WPF"));
            }

            return _instance;
        }

        /// <summary>
        /// Provides an injection point for an alternate source of CharacterCasingChecks at runtime
        /// </summary>
        /// <param name="source">The source is a function that returns a user-populated <see cref="CharacterCasingChecks"/>.</param>
        /// <returns>Returns a new <see cref="CharacterCasingChecks"/> each time this method is called.</returns>
        public static CharacterCasingChecks SetGetChecksSource(Func<CharacterCasingChecks> source) {
            _getChecksSource = source;
            return new CharacterCasingChecks();
        }

    }
}
