namespace Ocean.XamarinFormsSamples.Services {
    using Ocean.XamarinForms.BusinessObject;
    using Ocean.XamarinFormsSamples.Model;

    public interface IEventSignUpService {

        EventSignUp Create(AddPropertyNamesToIndexerErrorMessage addPropertyNamesToIndexerErrorMessage = AddPropertyNamesToIndexerErrorMessage.Yes);

    }
}