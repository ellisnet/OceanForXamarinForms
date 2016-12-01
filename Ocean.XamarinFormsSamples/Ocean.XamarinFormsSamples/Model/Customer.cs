namespace Ocean.XamarinFormsSamples.Model {
    using System;
    using Ocean.Portable;
    using Ocean.Portable.OceanValidation;
    using Ocean.XamarinForms.BusinessObject;

    public class Customer : BusinessEntityBase {

        String _comparePassword;
        String _email;
        Int32 _id;
        String _password;
        String _userName;

        [StringLengthValidator(5, 50)]
        [ComparePasswordValidator(nameof(Password))]
        public String ComparePassword {
            get { return _comparePassword; }
            set {
                base.SetPropertyValue(ref _comparePassword, value);
                base.CheckRulesForProperty(nameof(this.Password));
            }
        }

        [StringLengthValidator(50)]
        [RegularExpressionValidator(RegularExpressionPatternType.Email, RequiredEntry.Yes)]
        public String Email {
            get { return _email; }
            set { base.SetPropertyValue(ref _email, value); }
        }

        [CompareValueValidator(ComparisonType.Equal, 0, RequiredEntry.Yes, RuleSet = GlobalConstants.InsertRule)]
        [CompareValueValidator(ComparisonType.GreaterThan, 0, RequiredEntry.Yes, RuleSet = GlobalConstants.UpdateDeleteRule)]
        public Int32 Id {
            get { return _id; }
            set { base.SetPropertyValue(ref _id, value); }
        }

        [StringLengthValidator(5, 50)]
        public String Password {
            get { return _password; }
            set {
                base.SetPropertyValue(ref _password, value);
                base.CheckRulesForProperty(nameof(this.ComparePassword));
            }
        }

        [StringLengthValidator(1, 25)]
        public String UserName {
            get { return _userName; }
            set { base.SetPropertyValue(ref _userName, value); }
        }

        public Customer() {
        }

    }
}
