namespace Ocean.XamarinFormsSamples.View {
    using System.Diagnostics;
    using Ocean.Portable;
    using Ocean.XamarinForms.ViewModel;
    using Ocean.XamarinFormsSamples.Model;
    using Prism.Navigation;
    using Prism.Services;

    public class EventPageTwoViewModel : FormViewModelBaseBase {

        EventSignUp _eventSignUp;

        public EventSignUp EventSignUp {
            get { return _eventSignUp; }
            set {
                _eventSignUp = value;
                base.BusinessObject = value;
                RaisePropertyChanged();
            }
        }

        public EventPageTwoViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
            : base(pageDialogService, navigationService) {
        }

        public override void OnNavigatedTo(NavigationParameters parameters) {
            if (parameters != null && parameters.ContainsKey(GlobalConstants.Key)) {
                var eventSignUp = parameters[GlobalConstants.Key] as EventSignUp;

                if (eventSignUp != null) {
                    eventSignUp.ActiveRuleSet = GlobalConstants.InsertRule;  // change rule set to insert to match the operation we are doing.
                    eventSignUp.CheckAllRules();        // check all rules based on Insert Rule
                    eventSignUp.EndLoading();           // set is dirty to false to hide errors until properties on this page have been entered
                    this.EventSignUp = eventSignUp;

                    return;
                }
            }
            base.DisplayDialog(GlobalConstants.Error, "Parameters are null, key is missing, or parameter was not event sign up", GlobalConstants.OK, () => base.GoBack());
        }

        protected override void OnSaveExecuted() {
            base.OnSaveExecuted();

            //TODO: Write saved data to server

            this.DisplayDialog(GlobalConstants.Success, "Event Sign Up completed.", GlobalConstants.OK, () => {
                Debug.WriteLine("Dialog closed: saved");

                base.NavigateToUri(Constants.MainPage);
            });
        }

    }
}
