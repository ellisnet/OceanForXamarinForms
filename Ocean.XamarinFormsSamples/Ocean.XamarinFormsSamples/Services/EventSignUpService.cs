namespace Ocean.XamarinFormsSamples.Services {
    using Ocean.Portable;
    using Ocean.XamarinForms.BusinessObject;
    using Ocean.XamarinFormsSamples.Model;

    public class EventSignUpService : IEventSignUpService {

        public EventSignUpService() {
        }

        public EventSignUp Create(AddPropertyNamesToIndexerErrorMessage addPropertyNamesToIndexerErrorMessage = AddPropertyNamesToIndexerErrorMessage.Yes) {
            var eventSignUp = new EventSignUp();
            eventSignUp.ActiveRuleSet = GlobalConstants.InsertRule;
            eventSignUp.CheckAllRules();
            eventSignUp.AddPropertyNamesToIndexerErrorMessage = addPropertyNamesToIndexerErrorMessage;

            return eventSignUp;
        }

    }
}
