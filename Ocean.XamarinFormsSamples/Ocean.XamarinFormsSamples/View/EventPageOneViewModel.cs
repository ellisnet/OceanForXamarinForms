namespace Ocean.XamarinFormsSamples.View {
    using System;
    using Ocean.Portable;
    using Ocean.XamarinForms.ViewModel;
    using Ocean.XamarinFormsSamples.Model;
    using Ocean.XamarinFormsSamples.Services;
    using Prism.Navigation;
    using Prism.Services;

    public class EventPageOneViewModel : FormViewModelBaseBase {

        EventSignUp _eventSignUp;
        readonly IEventSignUpService _eventSignUpService;

        public EventSignUp EventSignUp {
            get { return _eventSignUp; }
            set {
                _eventSignUp = value;
                base.BusinessObject = value;
                RaisePropertyChanged();

            }
        }

        public EventPageOneViewModel(IPageDialogService pageDialogService, INavigationService navigationService, IEventSignUpService eventSignUpService)
            : base(pageDialogService, navigationService) {
            if (eventSignUpService == null) {
                throw new ArgumentNullException(nameof(eventSignUpService));
            }
            _eventSignUpService = eventSignUpService;
        }

        public override void OnNavigatedTo(NavigationParameters parameters) {
            var eventSignUp = _eventSignUpService.Create();
            eventSignUp.EventDate = DateTime.Now;
            eventSignUp.ActiveRuleSet = Constants.PageOneRule; // now only PageOne rules are enforced on this page.
            eventSignUp.CheckAllRules();
            this.EventSignUp = eventSignUp;
        }

        protected override void OnSaveExecuted() {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(GlobalConstants.Key, this.EventSignUp);
            base.NavigateToUri(Constants.EventPageTwo, navigationParameters);
        }

    }
}
