namespace Ocean.Portable.OceanValidation {
    using System.Collections.Generic;

    /// <summary>
    /// Represents ValidationRulesList
    /// </summary>
    public class ValidationRulesList {

        List<IValidationRuleMethod> _list;

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        public List<IValidationRuleMethod> List => _list ?? (_list = new List<IValidationRuleMethod>());

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesList"/> class.
        /// </summary>
        public ValidationRulesList() {
        }

    }
}
