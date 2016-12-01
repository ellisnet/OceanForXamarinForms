namespace Ocean.Portable.OceanValidation {
    using System;
    using System.Collections.Generic;
    using Ocean.Portable.InputStringFormatting;

    /// <summary>
    /// Represents StateAbbreviationValidator
    /// </summary>
    public class StateAbbreviationValidator {

        readonly Dictionary<String, String> _states;

        static StateAbbreviationValidator _instance;

        StateAbbreviationValidator() {
            _states = new Dictionary<String, String>();
            _states.Add("AL", "Alabama");
            _states.Add("AK", "Alaska");
            _states.Add("AS", "American Samoa");
            _states.Add("AZ", "Arizona");
            _states.Add("AR", "Arkansas");
            _states.Add("CA", "California");
            _states.Add("CO", "Colorado");
            _states.Add("CT", "Connecticut");
            _states.Add("DE", "Delaware");
            _states.Add("DC", "District of Columbia");
            _states.Add("FM", "Federated States of Micronesia");
            _states.Add("FL", "Florida");
            _states.Add("GA", "Georgia");
            _states.Add("GU", "Guam");
            _states.Add("HI", "Hawaii");
            _states.Add("ID", "Idaho");
            _states.Add("IL", "Illinois");
            _states.Add("IN", "Indiana");
            _states.Add("IA", "Iowa");
            _states.Add("KS", "Kansas");
            _states.Add("KY", "Kentucky");
            _states.Add("LA", "Louisiana");
            _states.Add("ME", "Maine");
            _states.Add("MH", "Marshall Islands");
            _states.Add("MD", "Maryland");
            _states.Add("MA", "Massachusetts");
            _states.Add("MI", "Michigan");
            _states.Add("MN", "Minnesota");
            _states.Add("MS", "Mississippi");
            _states.Add("MO", "Missouri");
            _states.Add("MT", "Montana");
            _states.Add("NE", "Nebraska");
            _states.Add("NV", "Nevada");
            _states.Add("NH", "New Hampshire");
            _states.Add("NJ", "New Jersey");
            _states.Add("NM", "New Mexico");
            _states.Add("NY", "New York");
            _states.Add("NC", "North Carolina");
            _states.Add("ND", "North Dakota");
            _states.Add("MP", "Northern Mariana Islands");
            _states.Add("OH", "Ohio");
            _states.Add("OK", "Oklahoma");
            _states.Add("OR", "Oregon");
            _states.Add("PW", "Palau");
            _states.Add("PA", "Pennsylvania");
            _states.Add("PR", "Puerto Rico");
            _states.Add("RI", "Rhode Island");
            _states.Add("SC", "South Carolina");
            _states.Add("SD", "South Dakota");
            _states.Add("TN", "Tennessee");
            _states.Add("TX", "Texas");
            _states.Add("UT", "Utah");
            _states.Add("VT", "Vermont");
            _states.Add("VI", "Virgin Islands");
            _states.Add("VA", "Virginia");
            _states.Add("WA", "Washington");
            _states.Add("WV", "West Virginia");
            _states.Add("WI", "Wisconsin");
            _states.Add("WY", "Wyoming");

            //
            //leave it up to the government to foul this up!!!
            //Using AE 4 times.  Who's in charge up there!
            //
            //     _objStates.Add("AE", "Armed Forces Africa")
            _states.Add("AA", "Armed Forces Americas");

            //        _objStates.Add("AE", "Armed Forces Canada")
            _states.Add("AE", "Armed Forces Europe");

            //      _objStates.Add("AE", "Armed Forces Middle East")
            _states.Add("AP", "Armed Forces Pacific");
        }

        /// <summary>
        /// Creates the instance or returns the previously created instance.
        /// </summary>
        /// <returns><see cref="StateAbbreviationValidator"/>; if not previously created will return a new instance of <see cref="StateAbbreviationValidator"/>.</returns>
        public static StateAbbreviationValidator CreateInstance() {
            return _instance ?? (_instance = new StateAbbreviationValidator());
        }

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <param name="stateAbbreviation">The state abbreviation.</param>
        /// <returns>String state name that corresponds to the state abbreviation.</returns>
        public String GetStateName(String stateAbbreviation) {
            return IsValid(stateAbbreviation) ? FormatText.ApplyCharacterCasing(_states[stateAbbreviation.ToUpper()], CharacterCasing.ProperName) : $"{stateAbbreviation} is not valid state abbreviation.";
        }

        /// <summary>
        /// Determines whether the specified state abbreviation is valid.
        /// </summary>
        /// <param name="stateAbbreviation">The state abbreviation.</param>
        /// <returns>
        /// 	<c>true</c> if the specified state abbreviation is valid; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsValid(String stateAbbreviation) {
            return _states.ContainsKey(stateAbbreviation.ToUpper());
        }

    }
}
