namespace Ocean.XamarinFormsSamples.Services {
    using Ocean.XamarinForms.BusinessObject;
    using Ocean.XamarinFormsSamples.Model;

    public interface ICustomerService {

        Customer Create(AddPropertyNamesToIndexerErrorMessage addPropertyNamesToIndexerErrorMessage = AddPropertyNamesToIndexerErrorMessage.Yes);

    }
}