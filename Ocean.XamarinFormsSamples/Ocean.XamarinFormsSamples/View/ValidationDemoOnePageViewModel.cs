namespace Ocean.XamarinFormsSamples.View {
    using System;
    using System.Diagnostics;
    using Ocean.Portable;
    using Ocean.XamarinForms.BusinessObject;
    using Ocean.XamarinForms.ViewModel;
    using Ocean.XamarinFormsSamples.Model;
    using Ocean.XamarinFormsSamples.Services;
    using Prism.Navigation;
    using Prism.Services;

    public class ValidationDemoOnePageViewModel : FormViewModelBaseBase {

        Customer _customer;
        readonly ICustomerService _customerService;

        public Customer Customer {
            get { return _customer; }
            set {
                _customer = value;
                base.BusinessObject = this.Customer;
                base.RaisePropertyChanged();
            }
        }

        public ValidationDemoOnePageViewModel(IPageDialogService pageDialogService, INavigationService navigationService, ICustomerService customerService)
            : base(pageDialogService, navigationService) {
            if (customerService == null) {
                throw new ArgumentNullException(nameof(customerService));
            }
            _customerService = customerService;
        }

        public override void OnNavigatedTo(NavigationParameters parameters) {
            this.Customer = _customerService.Create(AddPropertyNamesToIndexerErrorMessage.No);
        }

        protected override void OnDeleteExecuted() {
            //TODO: Write delete data to server

            // restore simulated deleted Customer to update mode
            this.Customer.ActiveRuleSet = GlobalConstants.UpdateRule;
            this.Customer.CheckAllRules();
            this.Customer.EndLoading();

            this.DisplayDialog(GlobalConstants.Success, "Simulate Customer Deleted.", GlobalConstants.OK, () => {
                Debug.WriteLine("Dialog closed: deleted");
            });
        }

        protected override void OnSaveExecuted() {
            //TODO: Write saved data to server

            // simulate Customer record coming back with Id and updated ActiveRulesSet
            this.Customer.Id = 1;
            this.Customer.ActiveRuleSet = GlobalConstants.UpdateRule;
            this.Customer.CheckAllRules();
            this.Customer.EndLoading();

            this.DisplayDialog(GlobalConstants.Success, "Customer saved.", GlobalConstants.OK, () => {
                Debug.WriteLine("Dialog closed: saved");
            });
        }

    }
}
