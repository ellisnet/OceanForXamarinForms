
#pragma warning disable 4014

namespace Ocean.XamarinFormsSamples.ViewAsyncAwait {
    using System;
    using System.Threading.Tasks;
    using Ocean.Portable;
    using Ocean.XamarinForms.Services;
    using Ocean.XamarinForms.ViewModelAsyncAwait;
    using Ocean.XamarinFormsSamples.Model;
    using Ocean.XamarinFormsSamples.Services;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    public class SpeakerDetailPageViewModel : FormViewModelBaseBase {

        readonly ICrossTextToSpeechService _crossTextToSpeechService;
        readonly IDeviceService _deviceService;
        DelegateCommand _goToWebSiteCommand;
        Boolean _isSpeaking;
        Speaker _speaker;
        DelegateCommand _speakTextCommand;

        public DelegateCommand GoToWebSiteCommand => _goToWebSiteCommand ?? (_goToWebSiteCommand = new DelegateCommand(GoToWebSiteCommandExecute, CanGoToWebSiteCommandExecute).ObservesProperty(() => this.Speaker));

        public Boolean IsSpeaking {
            get { return _isSpeaking; }
            set {
                _isSpeaking = value;
                RaisePropertyChanged();
            }
        }

        public Speaker Speaker {
            get { return _speaker; }
            set {
                _speaker = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand SpeakTextCommand => _speakTextCommand ?? (_speakTextCommand = new DelegateCommand(SpeakTextCommandExecute, CanSpeakTextCommandExecute).ObservesProperty(() => this.Speaker));

        public SpeakerDetailPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService, ICrossTextToSpeechService crossTextToSpeechService, IDeviceService deviceService)
            : base(pageDialogService, navigationService) {
            if (crossTextToSpeechService == null) {
                throw new ArgumentNullException(nameof(crossTextToSpeechService));
            }
            if (deviceService == null) {
                throw new ArgumentNullException(nameof(deviceService));
            }
            _crossTextToSpeechService = crossTextToSpeechService;
            _deviceService = deviceService;
        }

        public override void OnNavigatedTo(NavigationParameters navigationParameters) {
            ProcessNavigatedTo(navigationParameters);
        }

        Boolean CanGoToWebSiteCommandExecute() {
            return !String.IsNullOrWhiteSpace(this.Speaker?.Website) && this.Speaker.Website.StartsWith("http");
        }

        Boolean CanSpeakTextCommandExecute() {
            return !this.IsSpeaking && !String.IsNullOrWhiteSpace(this.Speaker?.Description);
        }

        void GoToWebSiteCommandExecute() {
            if (!CanGoToWebSiteCommandExecute()) {
                return;
            }
            _deviceService.OpenUri(new Uri(this.Speaker.Website));
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        async Task ProcessNavigatedTo(NavigationParameters navigationParameters) {
            try {
                if (navigationParameters != null && navigationParameters.ContainsKey(GlobalConstants.Key)) {
                    var speaker = navigationParameters[GlobalConstants.Key] as Speaker;

                    if (speaker != null && speaker.Name.StartsWith("Star")) {
                        await base.DisplayDialog(GlobalConstants.Error, "Parameters are null or key is missing");
                        await base.GoBack();
                    } else {
                        this.Speaker = speaker;
                    }
                } else {
                    await base.DisplayDialog(GlobalConstants.Error, "Parameters are null or key is missing");
                    await base.GoBack();
                }
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        void SpeakTextCommandExecute() {
            if (!CanSpeakTextCommandExecute()) {
                return;
            }
            this.IsSpeaking = true;

            // Must place this here, using other techniques causes the button to not be disabled while speaking is in progress.  Timing issue with events.
            this.SpeakTextCommand.RaiseCanExecuteChanged();

            _crossTextToSpeechService.Speak(this.Speaker.Description);
            this.IsSpeaking = false;
        }

    }
}
