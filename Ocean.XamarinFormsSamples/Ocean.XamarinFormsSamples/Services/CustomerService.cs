namespace Ocean.XamarinFormsSamples.Services {
    using Ocean.Portable;
    using Ocean.XamarinForms.BusinessObject;
    using Ocean.XamarinFormsSamples.Model;

    public class CustomerService : ICustomerService {

        public CustomerService() {
        }

        public Customer Create(AddPropertyNamesToIndexerErrorMessage addPropertyNamesToIndexerErrorMessage = AddPropertyNamesToIndexerErrorMessage.Yes) {
            var customer = new Customer();
            customer.ActiveRuleSet = GlobalConstants.InsertRule;
            customer.CheckAllRules();
            customer.AddPropertyNamesToIndexerErrorMessage = addPropertyNamesToIndexerErrorMessage;

            return customer;
        }

    }
}
