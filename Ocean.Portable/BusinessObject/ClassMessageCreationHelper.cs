namespace Ocean.Portable.BusinessObject {
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Ocean.Portable;
    using Ocean.Portable.Audit;
    using Ocean.Portable.Infrastructure;
    using Ocean.Portable.InputStringFormatting;

    /// <summary>
    /// Represents ClassMessageCreationHelper, which provides helper methods to create audit log strings, complete class property values listings in either String or IDictionary formats.
    /// </summary>
    public class ClassMessageCreationHelper {

        /// <summary>
        /// Represents a SortablePropertyBasket
        /// </summary>
        internal class SortablePropertyBasket : IComparable<SortablePropertyBasket> {

            /// <summary>
            /// Gets the audit sequence.
            /// </summary>
            /// <value>The audit sequence.</value>
            public Int32 AuditSequence { get; }

            /// <summary>
            /// Gets the name of the friendly.
            /// </summary>
            /// <value>The name of the friendly.</value>
            public String FriendlyName { get; }

            /// <summary>
            /// Gets the name of the property.
            /// </summary>
            /// <value>The name of the property.</value>
            public String PropertyName { get; }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public String Value { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="SortablePropertyBasket"/> class.
            /// </summary>
            /// <param name="auditSequence">The audit sequence.</param>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="friendlyName">The friendly property name.</param>
            /// <param name="value">The property value.</param>
            public SortablePropertyBasket(Int32 auditSequence, String propertyName, String friendlyName, String value) {
                if (value == null) {
                    value = String.Empty;
                }

                this.AuditSequence = auditSequence;
                this.PropertyName = propertyName;
                this.FriendlyName = friendlyName;
                this.Value = value;
            }

            /// <summary>
            /// Compares the current object with another object of the same type.
            /// </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
            /// Value
            /// Meaning
            /// Less than zero
            /// This object is less than the <paramref name="other"/> parameter.
            /// Zero
            /// This object is equal to <paramref name="other"/>.
            /// Greater than zero
            /// This object is greater than <paramref name="other"/>.</returns>
            public Int32 CompareTo(SortablePropertyBasket other) {
                return String.CompareOrdinal(String.Concat(String.Concat(FourZeros, this.AuditSequence.ToString()).Substring(String.Concat(FourZeros, this.AuditSequence.ToString()).Length - 4), this.PropertyName), String.Concat(String.Concat(FourZeros, other.AuditSequence.ToString()).Substring(String.Concat(FourZeros, other.AuditSequence.ToString()).Length - 4), other.PropertyName));
            }

            /// <summary>
            /// Returns a <see cref="String"/> that represents this instance.
            /// </summary>
            /// <returns>A <see cref="String"/> that represents this instance.</returns>
            public override String ToString() {
                return this.FriendlyName.Length == 0 ? $"{this.PropertyName} = {this.Value}" : $"{this.FriendlyName} ( {this.PropertyName} ) = {this.Value}";
            }

        }

        const String DefaultValue = "DefaultValue";
        const String FourZeros = "0000";
        const String Null = "Null";
        const Int32 One = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassMessageCreationHelper"/> class.
        /// </summary>
        ClassMessageCreationHelper() {
        }

        /// <summary>
        /// Generates an IDictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>.
        /// <returns>IDictionary populated with properties and values.</returns>
        public static IDictionary<String, String> AuditToIDictionary<T>(T instance, String defaultValue) {
            var dictionary = new Dictionary<String, String>();
            return AuditToIDictionary(instance, defaultValue, dictionary);
        }

        /// <summary>
        /// Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>.
        /// <param name="dictionary">Pass an IDictionary object that needs to be populated. This could be the Data property of an exception object that you want to populate, etc.</param>
        /// <returns>IDictionary populated with properties and values.</returns>
        public static IDictionary<String, String> AuditToIDictionary<T>(T instance, String defaultValue, IDictionary<String, String> dictionary) {
            var list = new List<SortablePropertyBasket>();
            
            foreach (var propInfo in PclReflection.GetPropertiesInfo(instance)) {
                var auditAttributes = propInfo.GetCustomAttributes<AuditAttribute>(false);
                var auditAttributeList = new List<AuditAttribute>(auditAttributes);

                AuditAttribute auditAttribute = null;

                if (auditAttributeList.Count == 1) {
                    auditAttribute = auditAttributeList[0];
                }
                if (auditAttribute != null) {
                    list.Add(new SortablePropertyBasket(auditAttribute.AuditSequence, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), propInfo.GetValue(instance, null).ToString()));
                }
            }

            if (list.Count > 0) {
                list.Sort();

                foreach (var propertyBasket in list) {
                    dictionary.Add(propertyBasket.PropertyName, propertyBasket.Value);
                }
            } else {
                dictionary.Add(DefaultValue, defaultValue);
            }

            return dictionary;
        }

        /// <summary>
        /// Builds up a string containing each property and value in the class decorated with the AuditAttribute. The string displays the property name, property friendly name and property value.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="instance">Instance of the class</param>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="delimiter">What delimiter do you want between each property? Defaults to comma; can pass others like Environment.NewLine, etc.</param>
        /// <param name="includeAllProperties">if set to <c>true</c> [include all attributes].</param>
        /// <returns>A string containing each property name, friendly name and value, separated by the delimiter and sorted by AuditAttribute.AuditSequence and then property name.</returns>
        public static String AuditToString<T>(T instance, String defaultValue, String delimiter = GlobalConstants.DefaultDelimiter, Boolean includeAllProperties = false) {
            var sb = new StringBuilder(2048);
            var list = new List<SortablePropertyBasket>();
            var nonAttributedPropertyIndex = 0;

            foreach (var propInfo in PclReflection.GetPropertiesInfo(instance)) {
                var auditAttributes = propInfo.GetCustomAttributes<AuditAttribute>(false);
                var auditAttributeList = new List<AuditAttribute>(auditAttributes);
                AuditAttribute auditAttribute = null;

                if (auditAttributeList.Count == 1) {
                    auditAttribute = auditAttributeList[0];
                }

                if (auditAttribute != null) {
                    list.Add(propInfo.GetValue(instance, null) != null ? new SortablePropertyBasket(auditAttribute.AuditSequence, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), propInfo.GetValue(instance, null).ToString()) : new SortablePropertyBasket(auditAttribute.AuditSequence, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), Null));
                } else if (includeAllProperties && propInfo.Name != "Item") {
                    nonAttributedPropertyIndex += 1;
                    list.Add(propInfo.GetValue(instance, null) != null ? new SortablePropertyBasket(nonAttributedPropertyIndex, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), propInfo.GetValue(instance, null).ToString()) : new SortablePropertyBasket(nonAttributedPropertyIndex, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), Null));
                }
            }

            if (list.Count > 0) {
                list.Sort();

                foreach (SortablePropertyBasket propertyBasket in list) {
                    sb.Append(propertyBasket);
                    sb.Append(delimiter);
                }

                if (sb.Length > delimiter.Length) {
                    sb.Length -= delimiter.Length;
                }
            } else {
                sb.Append(defaultValue);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates an IDictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>IDictionary populated with properties and values.</returns>
        public static IDictionary<String, String> ClassToIDictionary<T>(T instance, String defaultValue, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {
            var dictionary = new Dictionary<String, String>();
            return ClassToIDictionary(instance, defaultValue, dictionary, sortByPropertyName);
        }

        /// <summary>
        /// Populates the passed in IDictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="dictionary">Pass an IDictionary object that needs to be populated. This could be the Data property of an exception object that you want to populate, etc.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>The dictionary passed in populated with properties and values.</returns>
        public static IDictionary<String, String> ClassToIDictionary<T>(T instance, String defaultValue, IDictionary<String, String> dictionary, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {
            var list = new List<SortablePropertyBasket>();

            foreach (var propInfo in PclReflection.GetPropertiesInfo(instance)) {
                var auditAttributes = propInfo.GetCustomAttributes<AuditAttribute>(false);
                var auditAttributeList = new List<AuditAttribute>(auditAttributes);

                AuditAttribute auditAttribute = null;

                if (auditAttributeList.Count == 1) {
                    auditAttribute = auditAttributeList[0];
                }

                if (auditAttribute != null) {
                    list.Add(new SortablePropertyBasket(One, propInfo.Name, String.Empty, propInfo.GetValue(instance, null).ToString()));
                }
            }

            if (list.Count > 0) {
                if (sortByPropertyName == SortByPropertyName.Yes) {
                    list.Sort();
                }

                foreach (SortablePropertyBasket propertyBasket in list) {
                    dictionary.Add(propertyBasket.PropertyName, propertyBasket.Value);
                }
            } else {
                dictionary.Add(DefaultValue, defaultValue);
            }

            return dictionary;
        }

        /// <summary>
        /// Builds up a string containing each property and value in the class. The string displays the property name, property friendly name and property value.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="instance">Instance of the class</param>
        /// <param name="delimiter">What delimiter do you want between each property? Defaults to comma; can pass others like Environment.NewLine, etc.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>A string containing each property name, friendly name and value, separated by the delimiter and optionally sorted by property name.</returns>
        public static String ClassToString<T>(T instance, String delimiter = GlobalConstants.DefaultDelimiter, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {
            var sb = new StringBuilder(4096);
            var list = new List<SortablePropertyBasket>();

            foreach (var propInfo in PclReflection.GetPropertiesInfo(instance)) {
                var auditAttributes = propInfo.GetCustomAttributes<AuditAttribute>(false);
                var auditAttributeList = new List<AuditAttribute>(auditAttributes);

                AuditAttribute auditAttribute = null;

                if (auditAttributeList.Count == 1) {
                    auditAttribute = auditAttributeList[0];
                }

                var auditSequence = 1;

                if (auditAttribute != null) {
                    auditSequence = auditAttribute.AuditSequence;
                }
                if (propInfo.Name != "Item" && propInfo.Name != "ValidationErrors") {
                    list.Add(propInfo.GetValue(instance, null) != null ? new SortablePropertyBasket(auditSequence, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), propInfo.GetValue(instance, null).ToString()) : new SortablePropertyBasket(auditSequence, propInfo.Name, CamelCaseString.GetWords(propInfo.Name), Null));
                }
            }

            if (list.Count > 0) {
                if (sortByPropertyName == SortByPropertyName.Yes) {
                    list.Sort();
                }

                foreach (var propertyBasket in list) {
                    sb.Append(propertyBasket);
                    sb.Append(delimiter);
                }

                if (sb.Length > delimiter.Length) {
                    sb.Length -= delimiter.Length;
                }
            } else {
                sb.Append("Class has no properties");
            }

            return sb.ToString();
        }

    }
}
