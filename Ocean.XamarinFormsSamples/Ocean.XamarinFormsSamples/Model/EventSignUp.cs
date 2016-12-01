namespace Ocean.XamarinFormsSamples.Model {
    using System;
    using Ocean.Portable;
    using Ocean.Portable.OceanValidation;
    using Ocean.XamarinForms.BusinessObject;

    public class EventSignUp : BusinessEntityBase {

        String _email;
        DateTime _eventDate;
        String _firstName;
        String _lastName;
        String _title;

        [StringLengthValidator(50, RuleSet = GlobalConstants.InsertUpdateRule)]
        [RegularExpressionValidator(RegularExpressionPatternType.Email, RequiredEntry.Yes, RuleSet = GlobalConstants.InsertUpdateRule)]
        public String Email {
            get { return _email; }
            set { base.SetPropertyValue(ref _email, value); }
        }

        public DateTime EventDate {
            get { return _eventDate; }
            set { base.SetPropertyValue(ref _eventDate, value); }
        }

        [StringLengthValidator(1, 50, RuleSet = GlobalConstants.InsertUpdateRule)]
        public String FirstName {
            get { return _firstName; }
            set { base.SetPropertyValue(ref _firstName, value); }
        }

        [StringLengthValidator(1, 50, RuleSet = GlobalConstants.InsertUpdateRule)]
        public String LastName {
            get { return _lastName; }
            set { base.SetPropertyValue(ref _lastName, value); }
        }

        [StringLengthValidator(1, 50, RuleSet = Constants.PageOneInsertUpdateRule)]
        public String Title {
            get { return _title; }
            set { base.SetPropertyValue(ref _title, value); }
        }

        public EventSignUp() {
        }

        protected override void AddSharedBusinessValidationRules(ValidationRulesManager validationRulesManager) {
            validationRulesManager.AddRule(ComparisonValidationRules.CompareValueRule, new CompareValueRuleDescriptor(ComparisonType.GreaterThanEqual, RequiredEntry.Yes, DateTime.Today, String.Empty, "EventDate", "EventDate", Constants.PageOneInsertUpdateRule, String.Empty), RuleType.Shared);
        }

    }
}
