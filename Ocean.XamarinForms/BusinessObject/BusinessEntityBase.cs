namespace Ocean.XamarinForms.BusinessObject {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.RegularExpressions;
    using Ocean.Portable;
    using Ocean.Portable.Audit;
    using Ocean.Portable.BusinessObject;
    using Ocean.Portable.ComponentModel;
    using Ocean.Portable.Infrastructure;
    using Ocean.Portable.InputStringFormatting;
    using Ocean.Portable.OceanValidation;

    /// <summary>
    /// Base class for business entity objects.
    /// </summary>
    public abstract class BusinessEntityBase : ObservableObject, IDataErrorInfo, IBusinessEntityAudit, IBusinessEntity, ILoadable, IMarkUpdated {
        #region  Declarations

        const String _STRING_ERROR = "Error";
        const String _STRING_ISDIRTY = "IsDirty";
        const String _STRING_ISNOTVALID = "IsNotValid";
        const String _STRING_ISVALID = "IsValid";
        static readonly Object _LockObject = new Object();
        String _activeRuleSet = GlobalConstants.InsertRule;
        ValidationRulesManager _instanceValidationRulesManager;
        Boolean _isDirty;
        Boolean _isDuplicateRow;
        Boolean _markedForDeletion;
        Dictionary<String, ValidationError> _validationErrors;
        readonly HashSet<String> _dirtyProperties = new HashSet<String>();

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the active rule set. Use this property to have a specific rule set checked in addition to all rules that are not assigned a specific rule set.  For example, if you set this property to, Insert; when the rules are checked, all general rules will be checked and the Insert rules will also be checked.
        /// </summary>
        /// <value>The active rule set.</value>
        /// <exception cref="System.InvalidOperationException">Active rule set can not contain any pipe symbols. This is normally caused when a validation constant is passed that is for multiple rule sets and not a single rule set that this property requires.</exception>
        public String ActiveRuleSet {
            get { return _activeRuleSet; }
            set {
                if (value.Contains("|")) {
                    throw new InvalidOperationException("Active rule set can not contain any pipe symbols. This is normally caused when a validation constant is passed that is for multiple rule sets and not a single rule set that this property requires.");
                }
                _activeRuleSet = value;
                ClearValidationErrors();
                InternalRaisePropertyChanged(nameof(ActiveRuleSet));
            }
        }

        /// <summary>
        /// Gets or sets the add property names to indexer error message. Default value Yes.
        /// </summary>
        /// <value>The add property names to indexer error message.</value>
        public AddPropertyNamesToIndexerErrorMessage AddPropertyNamesToIndexerErrorMessage { get; set; } = AddPropertyNamesToIndexerErrorMessage.Yes;

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this Object.  Every error in the Broken Rules collection is returned.
        /// </summary>
        /// <value>The error.</value>
        /// <exception cref="System.InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <exception cref="InvalidOperationException">if <see cref="BusinessEntityBase.IsLoading" /> is <c>true</c>.</exception>
        /// <returns>String that represents all broken rule descriptions for this instance.</returns>
        public String Error {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException("EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.");
                }

                var sb = new StringBuilder(4096);

                foreach (ValidationError obj in this.GetAllBrokenRules()) {
                    if (!String.IsNullOrWhiteSpace(obj.BrokenRule.RuleBase.OverrideMessage)) {
                        sb.AppendLine(obj.BrokenRule.RuleBase.OverrideMessage);
                    } else if (!String.IsNullOrWhiteSpace(obj.BrokenRule.RuleBase.CustomMessage)) {
                        sb.Append(obj.BrokenRule.RuleBase.CustomMessage);
                        sb.Append(" : ");
                        sb.AppendLine(obj.BrokenRule.RuleBase.BrokenRuleDescription);
                    } else {
                        sb.AppendLine(obj.BrokenRule.RuleBase.BrokenRuleDescription);
                    }

                    sb.AppendLine();
                }

                //this removes the last append line characters
                if (sb.Length > 2) {
                    sb.Length = sb.Length - 2;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Used by this base class internally as a method of getting the Instance Validation RulesManager.  This property uses lazy Object creation.
        /// </summary>
        /// <value>The instance validation rules manager.</value>
        /// <exception cref="System.InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <exception cref="InvalidOperationException">if <see cref="BusinessEntityBase.IsLoading" /> is <c>true</c>.</exception>
        /// <returns><see cref="ValidationRulesManager" /> for this instance.</returns>
        ValidationRulesManager InstanceValidationRulesManager {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException("EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.");
                }

                return _instanceValidationRulesManager ?? (_instanceValidationRulesManager = new ValidationRulesManager());
            }
        }

        /// <summary>
        /// Is this Object dirty? Have changes been made since the Object was loaded or a new Object was constructed. This is automatically kept track of by this base class.
        /// </summary>
        /// <value>The is dirty.</value>
        /// <exception cref="System.InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns><c>True</c> if this instance is dirty; otherwise <c>false</c>.</returns>
        public Boolean IsDirty {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException("EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.");
                }

                return _isDirty;
            }
            private set {
                _isDirty = value;
                InternalRaisePropertyChanged(_STRING_ISDIRTY);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is duplicate row.
        /// </summary>
        /// <value><c>true</c> if this instance is duplicate row; otherwise, <c>false</c>.</value>
        public Boolean IsDuplicateRow {
            get { return _isDuplicateRow; }
            set {
                _isDuplicateRow = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsLoading { get; private set; }

        /// <summary>
        /// Is this Object not valid? Property is exposed for data binding purposes so that consumers do not need to use a converter when requiring a negative true result.
        /// </summary>
        /// <value>The is not valid.</value>
        /// <exception cref="System.InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns><c>True</c> if this instance is not valid; otherwise <c>false</c>.</returns>
        public Boolean IsNotValid {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException("EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.");
                }

                return !this.IsValid;
            }
        }

        /// <summary>
        /// Is this Object valid?
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        /// <exception cref="System.InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns><c>True</c> if this instance is valid; otherwise <c>false</c>.</returns>
        public Boolean IsValid {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException("EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.");
                }

                return this.ValidationErrors.Count == 0;
            }
        }

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this property. Every error for this property in the Broken Rules collection is returned.
        /// </summary>
        /// <param name="propertyName">Name of the column.</param>
        /// <returns>String containing all error messages for the property; if the property is valid returns an empty String.</returns>
        public String this[String propertyName] {
            get {
                if (!_dirtyProperties.Contains(propertyName)) {
                    _dirtyProperties.Add(propertyName);
                    return String.Empty;
                }

                if (this.AddPropertyNamesToIndexerErrorMessage == AddPropertyNamesToIndexerErrorMessage.No) {
                    return GetNoNameErrorTextForProperty(propertyName);
                }
                return GetErrorTextForProperty(propertyName);
            }
        }

        String GetNoNameErrorTextForProperty(String propertyName) {
            var sb = new StringBuilder(128);

            foreach (var validationError in this.GetBrokenRulesForProperty(propertyName)) {
                if (!String.IsNullOrEmpty(validationError.BrokenRule.RuleBase.OverrideMessage)) {
                    sb.Append(validationError.BrokenRule.RuleBase.OverrideMessage);
                    break;
                }
                if (!String.IsNullOrWhiteSpace(validationError.BrokenRule.RuleBase.CustomMessage)) {
                    sb.Append(validationError.BrokenRule.RuleBase.CustomMessage);
                    sb.Append(" : ");
                    sb.Append(validationError.BrokenRule.RuleBase.BrokenRuleDescription.Replace(Ocean.Portable.InputStringFormatting.CamelCaseString.GetWords(propertyName), String.Empty));
                    break;
                }
                var words = Ocean.Portable.InputStringFormatting.CamelCaseString.GetWords(propertyName);
                if (validationError.BrokenRule.RuleBase.BrokenRuleDescription.StartsWith(words)) {
                    sb.Append(validationError.BrokenRule.RuleBase.BrokenRuleDescription.Substring(words.Length));
                } else {
                    sb.Append(validationError.BrokenRule.RuleBase.BrokenRuleDescription);
                }
                break;
            }

            return sb.ToString();
        }

        String GetErrorTextForProperty(String propertyName) {
            var sb = new StringBuilder(1024);

            foreach (var validationError in this.GetBrokenRulesForProperty(propertyName)) {
                if (!String.IsNullOrEmpty(validationError.BrokenRule.RuleBase.OverrideMessage)) {
                    sb.AppendLine(validationError.BrokenRule.RuleBase.OverrideMessage);
                } else if (!String.IsNullOrWhiteSpace(validationError.BrokenRule.RuleBase.CustomMessage)) {
                    sb.Append(validationError.BrokenRule.RuleBase.CustomMessage);
                    sb.Append(" : ");
                    sb.AppendLine(validationError.BrokenRule.RuleBase.BrokenRuleDescription);
                } else {
                    sb.AppendLine(validationError.BrokenRule.RuleBase.BrokenRuleDescription);
                }
                sb.AppendLine();
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been marked for deletion.
        /// </summary>
        /// <value><c>true</c> if this instance has been marked for deletion; otherwise, <c>false</c>.</value>
        public Boolean MarkedForDeletion {
            get { return _markedForDeletion; }
            set {
                _markedForDeletion = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the row number. Used when this instance is part of a collection. Row number is set after the collection is populated.
        /// </summary>
        /// <value>The row number.</value>
        public Int32 RowNumber { get; set; }

        /// <summary>
        /// A dictionary Object of all broken rules (validation errors)
        /// </summary>
        /// <value>The validation errors.</value>
        /// <exception cref="System.InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns>Dictionary containing all validation rules.</returns>
        public Dictionary<String, ValidationError> ValidationErrors {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException("EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.");
                }
                return _validationErrors ?? (_validationErrors = new Dictionary<String, ValidationError>());
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes the <see cref="BusinessEntityBase"/> class.
        /// </summary>
        static BusinessEntityBase() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessEntityBase"/> class.
        /// </summary>
        /// <remarks>
        /// Constructor sets the <see cref="BusinessEntityBase.ActiveRuleSet"/> to <see cref="String.Empty"/>, invokes AddInstanceBusinessValidationRules and AddSharedBusinessRules./>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected BusinessEntityBase() {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Initialize();
            this.ActiveRuleSet = String.Empty;
            AddInstanceBusinessValidationRules();
            AddSharedBusinessRules();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Override this method to set up event handlers so user code in a partial class can respond to events raised by generated code.
        /// </summary>
        protected virtual void Initialize() {
        }

        #endregion

        #region  Rules Methods

        /// <summary>
        /// This is used by sub classes and classes that consume the sub class business entity and need to add an instance rule to the list of rules to be enforced.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="ruleDescriptor">The rule descriptor.</param>
        public void AddInstanceRule(RuleHandler handler, RuleDescriptorBase ruleDescriptor) {
            this.InstanceValidationRulesManager.GetRulesForProperty(ruleDescriptor.PropertyName).List.Add(new Validator(handler, ruleDescriptor, RuleType.Instance));
        }

        /// <summary>
        /// Validates the entity against all shared and instance rules.
        /// </summary>
        public void CheckAllRules() {
            Boolean raiseErrorPropertyChanged = false;
            ValidationRulesManager mgr = SharedValidationRules.GetManager(this.GetType());
            if (this.ActiveRuleSet == GlobalConstants.Delete) {
                ClearValidationErrors();
            }

            try {
                foreach (ValidationRulesList vrl in mgr.RulesDictionary.Values) {
                    foreach (IValidationRuleMethod obj in vrl.List) {
                        if (RuleSetMatches(obj.RuleBase.RuleSet)) {
                            //remove broken rule if it exists, if not does nothing
                            if (this.ValidationErrors.Remove(obj.RuleName)) {
                                raiseErrorPropertyChanged = true;
                            }

                            if (obj.Invoke(this) == false) {
                                raiseErrorPropertyChanged = true;
                                this.ValidationErrors.Add(obj.RuleName, new ValidationError(obj));
                                InternalRaisePropertyChanged(obj.RuleBase.PropertyName);
                            }
                        }
                        InternalRaisePropertyChanged($"Item[{obj.RuleBase.PropertyName}]");
                    }
                }

#pragma warning disable 168
            } catch (InvalidOperationException) {
#pragma warning restore 168
                // this is here because one time in 10,000,000,000 I got an invalid operation exception.
            }

            if (_instanceValidationRulesManager != null) {
                foreach (ValidationRulesList vrl in this.InstanceValidationRulesManager.RulesDictionary.Values) {
                    foreach (IValidationRuleMethod obj in vrl.List) {
                        if (RuleSetMatches(obj.RuleBase.RuleSet)) {
                            //remove broken rule if it exists, if not does nothing
                            if (this.ValidationErrors.Remove(obj.RuleName)) {
                                raiseErrorPropertyChanged = true;
                            }

                            if (obj.Invoke(this) == false) {
                                raiseErrorPropertyChanged = true;
                                this.ValidationErrors.Add(obj.RuleName, new ValidationError(obj));
                                InternalRaisePropertyChanged(obj.RuleBase.PropertyName);
                            }
                        }
                        InternalRaisePropertyChanged($"Item[{obj.RuleBase.PropertyName}]");
                    }
                }
            }

            if (raiseErrorPropertyChanged) {
                InternalRaisePropertyChanged(_STRING_ERROR);
                InternalRaisePropertyChanged(_STRING_ISVALID);
                InternalRaisePropertyChanged(_STRING_ISNOTVALID);
            }
        }

        /// <summary>
        /// Validates the property against all shared and instance rules for the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void CheckRulesForProperty(String propertyName) {
            Boolean raiseErrorPropertyChanged = false;
            ValidationRulesManager mgr = SharedValidationRules.GetManager(this.GetType());

            foreach (IValidationRuleMethod obj in mgr.GetRulesForProperty(propertyName).List) {
                if (RuleSetMatches(obj.RuleBase.RuleSet)) {
                    //remove broken rule if it exists, if not does nothing
                    if (this.ValidationErrors.Remove(obj.RuleName)) {
                        raiseErrorPropertyChanged = true;
                    }

                    if (obj.Invoke(this) == false) {
                        raiseErrorPropertyChanged = true;
                        this.ValidationErrors.Add(obj.RuleName, new ValidationError(obj));
                    }
                }
            }

            if (_instanceValidationRulesManager != null) {
                foreach (IValidationRuleMethod obj in this.InstanceValidationRulesManager.GetRulesForProperty(propertyName).List) {
                    if (RuleSetMatches(obj.RuleBase.RuleSet)) {
                        //remove broken rule if it exists, if not does nothing
                        if (this.ValidationErrors.Remove(obj.RuleName)) {
                            raiseErrorPropertyChanged = true;
                        }

                        if (obj.Invoke(this) == false) {
                            raiseErrorPropertyChanged = true;
                            this.ValidationErrors.Add(obj.RuleName, new ValidationError(obj));
                        }
                    }
                }
            }

            if (raiseErrorPropertyChanged) {
                InternalRaisePropertyChanged(_STRING_ERROR);
                InternalRaisePropertyChanged(_STRING_ISVALID);
                InternalRaisePropertyChanged(_STRING_ISNOTVALID);
                InternalRaisePropertyChanged($"Item[{propertyName}]");
            }
        }

        void ClearValidationErrors() {
            this.ValidationErrors.Clear();
        }

        /// <summary>
        /// A List of ValidationError objects for the Object.
        /// </summary>
        /// <returns>List of <see cref="ValidationError"/> for all broken rules for this instance.</returns>
        public List<ValidationError> GetAllBrokenRules() {
            return this.ValidationErrors.Select(obj => obj.Value).ToList();
        }

        /// <summary>
        /// A List of ValidationError objects for the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>List of <see cref="ValidationError"/> for all broken rules for the property name.</returns>
        public List<ValidationError> GetBrokenRulesForProperty(String propertyName) {
            return (from obj in this.ValidationErrors
                    where String.Compare(obj.Value.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase) == 0
                    select obj.Value).ToList();
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up business rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// Use the instance method, AddInstanceRule to in deriving classes to add instance rules to the Object.
        /// </summary>
        protected virtual void AddInstanceBusinessValidationRules() {
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up SHARED business rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// To add shared rules to business objects to deriving class properties, override this method in deriving classes and add the rules to the ValidationRulesManager.
        /// This method will only be called once; the first time the deriving class is created.
        /// </summary>
        /// <param name="validationRulesManager">The validation rules manager.</param>
        /// <example>mgrValidation.AddRule(ComparisonValidationRules.CompareValueRule, new CompareValueRuleDescriptor(ComparisonType.LessThan, RequiredEntry.No, DateTime.Today, String.Empty, "Birthday", "Birthday", String.Empty, String.Empty), RuleType.Shared);</example>
        protected virtual void AddSharedBusinessValidationRules(ValidationRulesManager validationRulesManager) {
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up SHARED character casing rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// To add shared character case formatting to deriving class properties, override this method in deriving classes and add the rules to the CharacterCasingRulesManager.
        /// This method will only be called once; the first time the deriving class is created.
        /// </summary>
        /// <param name="characterCasingRulesManager">The character casing rules manager.</param>
        protected virtual void AddSharedCharacterCasingFormattingRules(CharacterFormattingRulesManager characterCasingRulesManager) {
        }

        /// <summary>
        /// This code gets called on the first time this Object type is constructed
        /// </summary>
        void AddSharedBusinessRules() {
            lock (_LockObject) {
                if (!SharedValidationRules.RulesExistFor(this.GetType())) {
                    ValidationRulesManager mgrValidation = SharedValidationRules.GetManager(this.GetType());
                    CharacterFormattingRulesManager mgrCharacterCasing = SharedCharacterFormattingRules.GetManager(this.GetType());

                    foreach (var propInfo in PclReflection.GetPropertiesInfo(this)) {
                        foreach (var atr in propInfo.GetCustomAttributes<BaseValidatorAttribute>(false)) {
                            mgrValidation.AddRule(atr.Create(propInfo.Name), propInfo.Name);
                        }

                        foreach (var atr in propInfo.GetCustomAttributes<CharacterFormattingAttribute>(false)) {
                            mgrCharacterCasing.AddRule(propInfo.Name, atr.CharacterCasing, atr.RemoveSpace);
                        }
                    }

                    AddSharedBusinessValidationRules(mgrValidation);
                    AddSharedCharacterCasingFormattingRules(mgrCharacterCasing);
                }
            }
        }

        Boolean RuleSetMatches(String ruleSet) {
            if (this.ActiveRuleSet == GlobalConstants.Delete && String.IsNullOrWhiteSpace(ruleSet)) {
                return false;
            }
            if (String.IsNullOrWhiteSpace(this.ActiveRuleSet) || String.IsNullOrWhiteSpace(ruleSet)) {
                return true;
            }

            String[] separators = { GlobalConstants.RuleSetDelimiter };

            if (this.ActiveRuleSet == GlobalConstants.DeleteRule) {
                var v = ruleSet.Split(separators, StringSplitOptions.RemoveEmptyEntries).Any(s => String.Compare(s, this.ActiveRuleSet, StringComparison.OrdinalIgnoreCase) == 0);
                return v;
            }

            return ruleSet.Split(separators, StringSplitOptions.RemoveEmptyEntries).Any(s => String.Compare(s, this.ActiveRuleSet, StringComparison.OrdinalIgnoreCase) == 0);
        }

        #endregion

        #region  Property Set and Change Notification Methods

        /// <summary> 
        /// Derived classes can override this method to execute logic after the property is set. The base implementation does nothing. 
        /// </summary> 
        /// <param name="propertyName"> 
        /// The property which was changed. 
        /// </param> 
        protected virtual void AfterPropertyChanged(String propertyName) {
        }

        /// <summary> 
        /// Derived classes can override this method to execute logic before the property is set. The base implementation does nothing. 
        /// </summary> 
        /// <param name="propertyName"> 
        /// The property which was changed. 
        /// </param> 
        protected virtual void BeforePropertyChanged(String propertyName) {
        }

        /// <summary>
        /// Sets the dirty, use to ensure an object is dirty so that error messages appear in the UI.
        /// </summary>
        public void SetDirty() {
            this.IsDirty = true;
        }

        /// <summary>
        /// Called by in business entity sub-classes in their property setters to set the value of the property.
        /// If the business Object is not in a loading state, this method performs validation on the property.
        /// This method also runs the String compression to remove extra spaces if the property is attributed with the CharacterFormattingAttribute.
        /// 
        /// <example>Example:
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue(ref _strSL_DatabaseConnection, Value)
        ///   End Set
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="currentValue">Current property value.</param>
        /// <param name="newValue">Value argument from the property Setter Set.</param>
        /// <param name="propertyName">Property Name.</param>
        protected void SetPropertyValue(ref String currentValue, String newValue, [CallerMemberName] String propertyName = null) {
            if (currentValue == null) {
                if (newValue == null) {
                    return;
                }
            } else if (newValue != null && currentValue.Equals(newValue)) {
                return;
            }

            if (!this.IsLoading) {
                this.IsDirty = true;
                this.BeforePropertyChanged(propertyName);

                //only apply character casing rules after the Object is loaded.
                var characterFormat = SharedCharacterFormattingRules.GetManager(this.GetType()).GetRuleForProperty(propertyName);

                if (characterFormat != null) {
                    currentValue = characterFormat.CharacterCasing != CharacterCasing.None ? FormatText.ApplyCharacterCasing(newValue, characterFormat.CharacterCasing) : newValue;
                    if (!String.IsNullOrWhiteSpace(currentValue) && characterFormat.RemoveSpace == RemoveSpace.MultipleSpaces) {
                        currentValue = Regex.Replace(currentValue, @"\s+", " ");
                    } else if (!String.IsNullOrWhiteSpace(currentValue) && characterFormat.RemoveSpace == RemoveSpace.AllSpaces) {
                        currentValue = currentValue.Replace(" ", String.Empty).Trim();
                    }
                } else {
                    currentValue = newValue;
                }

                CheckRulesForProperty(propertyName);
                InternalRaisePropertyChanged(propertyName);

                this.AfterPropertyChanged(propertyName);
            } else {
                //since we are loading, just set the value
                currentValue = newValue;
            }
        }

        /// <summary>
        /// Called by business entity sub-classes in their property setters to set the value of the property.
        /// If the business Object is not in a loading state, this method performs validation on the property
        /// <example>Example:
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue(ref _datBirthDay, Value)
        ///   End Set
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="T">Property Type.</typeparam>
        /// <param name="currentValue">variable that holds the current value of the property.</param>
        /// <param name="newValue">Value argument from the property Setter Set.</param>
        /// <param name="propertyName">Property Name.</param>
        protected void SetPropertyValue<T>(ref T currentValue, T newValue, [CallerMemberName] String propertyName = null) {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (currentValue == null) {
                // ReSharper restore CompareNonConstrainedGenericWithNull

                // ReSharper disable CompareNonConstrainedGenericWithNull
                if (newValue == null) {
                    // ReSharper restore CompareNonConstrainedGenericWithNull
                    return;
                }

                // ReSharper disable CompareNonConstrainedGenericWithNull
            } else if (newValue != null && currentValue.Equals(newValue)) {
                // ReSharper restore CompareNonConstrainedGenericWithNull
                return;
            }

            if (!this.IsLoading) {
                this.IsDirty = true;
                this.BeforePropertyChanged(propertyName);
                currentValue = newValue;
                CheckRulesForProperty(propertyName);
                InternalRaisePropertyChanged(propertyName);

                this.AfterPropertyChanged(propertyName);
            } else {
                //if we are loading the Object then just assign the value
                currentValue = newValue;
            }
        }

        void InternalRaisePropertyChanged(String propertyName) {
            if (!this.IsLoading) {
                RaisePropertyChanged(propertyName);
            }
            this.AfterPropertyChanged(propertyName);
        }

        #endregion

        #region  Object Loading, Complete Loading & Persisted Methods

        /// <summary>
        /// Called when the business Object is being loaded from a database.  This saves time and processing; by passing property setter logic during loading.  After the business Object has been loaded the EndLoading MUST be called.
        /// </summary>
        public void BeginLoading() {
            this.IsLoading = true;
        }

        /// <summary>
        /// After a business Object has been loaded and the BeginLoading method was called, developers must call this method, EndLoading.  This method marks the entity IsDirty = False, HasBeenValidated = False and raises these property changed events.
        /// </summary>
        public void EndLoading() {
            _dirtyProperties.Clear();
            this.IsLoading = false;
            this.IsDirty = false;
        }

        #endregion

        #region  Audit Methods

        /// <summary>
        /// Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>.
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated. This could be the Data property of an exception Object that you want to populate, etc.</param>
        /// <returns>IDictionary populated with properties and values.</returns>
        public IDictionary<String, String> ToAuditIDictionary(String defaultValue, IDictionary<String, String> dictionary) {
            return ClassMessageCreationHelper.AuditToIDictionary(this, defaultValue, dictionary);
        }

        /// <summary>
        /// Builds up a String containing each property and value in the class decorated with the AuditAttribute. The String displays the property name, property friendly name and property value.
        /// </summary>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="delimiter">What delimiter do you want between each property? Defaults to comma; can pass others like Environment.NewLine, etc.</param>
        /// <param name="includeAllProperties">if set to <c>true</c> [include all properties].</param>
        /// <returns>A String containing each property name, friendly name and value, separated by the delimiter and sorted by AuditAttribute.AuditSequence and then property name.</returns>
        public String ToAuditString(String defaultValue, String delimiter = GlobalConstants.DefaultDelimiter, Boolean includeAllProperties = false) {
            return ClassMessageCreationHelper.AuditToString(this, defaultValue, delimiter, includeAllProperties);
        }

        /// <summary>
        /// Populates the passed in IDictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>.
        /// </summary>
        /// <param name="defaultValue">If no class properties are decorated with the <see cref="AuditAttribute"/> then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.</param>
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated. This could be the Data property of an exception Object that you want to populate, etc.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>The dictionary passed in populated with properties and values.</returns>
        public IDictionary<String, String> ToClassIDictionary(String defaultValue, IDictionary<String, String> dictionary, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {
            return ClassMessageCreationHelper.ClassToIDictionary(this, defaultValue, dictionary, sortByPropertyName);
        }

        /// <summary>
        /// Builds up a String containing each property and value in the class. The String displays the property name, property friendly name and property value.
        /// </summary>
        /// <param name="delimiter">What delimiter do you want between each property? Defaults to comma; can pass others like Environment.NewLine, etc.</param>
        /// <param name="sortByPropertyName">If set to <c>SortByPropertyName.Yes</c> then output will be sorted by AuditAttribute.AuditSequence and then property name; otherwise no additional sorting is performed and properties; will be left in ordinal order.</param>
        /// <returns>A String containing each property name, friendly name and value, separated by the delimiter and optionally sorted by property name.</returns>
        public String ToClassString(String delimiter = GlobalConstants.DefaultDelimiter, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {
            return ClassMessageCreationHelper.ClassToString(this, delimiter, sortByPropertyName);
        }

        #endregion

        #region IMarkUpdated Implementation

        /// <summary>
        /// Called by the generic layers after a successful update or insert. This method sets IsDirty to false;
        /// </summary>
        public void Updated() {
            this.IsDirty = false;
        }

        #endregion //IMarkUpdated Implementation

        #region  Json.NET

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context) {
            this.BeginLoading();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context) {
            this.EndLoading();
        }

        #endregion  Json.NET
    }
}