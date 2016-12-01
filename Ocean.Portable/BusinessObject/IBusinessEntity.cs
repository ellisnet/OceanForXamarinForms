namespace Ocean.Portable.BusinessObject {
    using System;
    using System.Collections.Generic;
    using Ocean.Portable;
    using Ocean.Portable.Audit;

    /// <summary>
    /// Provides contact for IBusinessEntity classes
    /// </summary>
    public interface IBusinessEntity {
        /// <summary>
        /// Gets or sets the active rule set.
        /// </summary>
        /// <value>The active rule set.</value>
        String ActiveRuleSet { get; set; }

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this Object.  Every error in the Broken Rules collection is returned.
        /// </summary>
        /// <exception cref="InvalidOperationException"> if BusinessEntityBase.IsLoading is <c>true</c>.</exception>
        /// <returns>String that represents all broken rule descriptions for this instance.</returns>
        String Error { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        Boolean IsValid { get; }

        /// <summary>
        /// Checks all rules.
        /// </summary>
        void CheckAllRules();

        /// <summary>
        /// Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>.
        /// <param name="dictionary">Pass an IDictionary object that needs to be populated. This could be the Data property of an exception object that you want to populate, etc.</param>
        /// <returns>IDictionary populated with properties and values.</returns>
        IDictionary<String, String> ToAuditIDictionary(String defaultValue, IDictionary<String, String> dictionary);

        /// <summary>
        /// Builds up a string containing each property and value in the class decorated with the AuditAttribute. The string displays the property name, property friendly name and property value.
        /// </summary>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="delimiter">What delimiter do you want between each property? Defaults to comma; can pass others like Environment.NewLine, etc.</param>
        /// <param name="includeAllProperties">if set to <c>true</c> [include all properties].</param>
        /// <returns>A string containing each property name, friendly name and value, separated by the delimiter and sorted by AuditAttribute.AuditSequence and then property name.</returns>
        String ToAuditString(String defaultValue, String delimiter = GlobalConstants.DefaultDelimiter, Boolean includeAllProperties = false);

        /// <summary>
        /// Populates the passed in IDictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="dictionary">Pass an IDictionary object that needs to be populated. This could be the Data property of an exception object that you want to populate, etc.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>The dictionary passed in populated with properties and values.</returns>
        IDictionary<String, String> ToClassIDictionary(String defaultValue, IDictionary<String, String> dictionary, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes);

        /// <summary>
        /// Builds up a string containing each property and value in the class. The string displays the property name, property friendly name and property value.
        /// </summary>
        /// <param name="delimiter">What delimiter do you want between each property? Defaults to comma; can pass others like Environment.NewLine, etc.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>A string containing each property name, friendly name and value, separated by the delimiter and optionally sorted by property name.</returns>
        String ToClassString(String delimiter = GlobalConstants.DefaultDelimiter, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes);
    }
}