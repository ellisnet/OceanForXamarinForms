namespace Ocean.Portable.Infrastructure {
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Class PclReflection.
    /// </summary>
    public static class PclReflection {

        /// <summary>
        /// Gets the properties information.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>IEnumerable&lt;PropertyInfo&gt;</returns>
        public static IEnumerable<PropertyInfo> GetPropertiesInfo(Object target) {
            return target?.GetType().GetRuntimeProperties();
        }

        /// <summary>
        /// Gets the property information for a property on the target object.  If the target is null, returns null.  If the property is not found, returns null.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>PropertyInfo</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public static PropertyInfo GetPropertyInfo(Object target, String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return target?.GetType().GetRuntimeProperty(propertyName);
        }

    }
}
