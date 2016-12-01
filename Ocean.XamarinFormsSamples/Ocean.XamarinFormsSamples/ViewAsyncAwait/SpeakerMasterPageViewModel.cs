namespace Ocean.XamarinFormsSamples.ViewAsyncAwait {
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Ocean.XamarinForms.ViewModelAsyncAwait;
    using Ocean.XamarinFormsSamples.Model;
    using Ocean.XamarinFormsSamples.Services;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    public class SpeakerMasterPageViewModel : FormViewModelBaseBase, INavigationAware {

        DelegateCommand _getSpeakersCommand;
        Speaker _selectedSpeaker;
        ObservableCollection<Speaker> _speakers = new ObservableCollection<Speaker>();
        Boolean _speakersCollectionHasItems;
        DelegateCommand<Speaker> _speakerSelectedCommand;
        readonly ISpeakerService _speakerService;

        public DelegateCommand GetSpeakersCommand => _getSpeakersCommand ?? (_getSpeakersCommand = new DelegateCommand(async () => await GetSpeakersCommandExecute(), CanGetSpeakersCommandExecute).ObservesProperty(() => this.IsBusy));

        public Speaker SelectedSpeaker {
            get { return _selectedSpeaker; }
            set {
                _selectedSpeaker = value;
                base.RaisePropertyChanged();
            }
        }

        public ObservableCollection<Speaker> Speakers {
            get { return _speakers; }
            set {
                _speakers = value;
                base.RaisePropertyChanged();
            }
        }

        public Boolean SpeakersCollectionHasItems {
            get { return _speakersCollectionHasItems; }
            set {
                _speakersCollectionHasItems = value;
                base.RaisePropertyChanged();
            }
        }

        public DelegateCommand<Speaker> SpeakerSelectedCommand => _speakerSelectedCommand ?? (_speakerSelectedCommand = new DelegateCommand<Speaker>(async speaker => await SpeakerSelectedCommandExecute(speaker), CanSpeakerSelectedCommand));

        public SpeakerMasterPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService, ISpeakerService speakerService)
            : base(pageDialogService, navigationService) {
            if (speakerService == null) {
                throw new ArgumentNullException(nameof(speakerService));
            }
            _speakerService = speakerService;
        }

        public override void OnNavigatedTo(NavigationParameters parameters) {
            base.RaisePropertyChanged(nameof(SpeakerSelectedCommand));
        }

        Boolean CanGetSpeakersCommandExecute() {
            return !this.IsBusy;
        }

        Boolean CanSpeakerSelectedCommand(Speaker speaker) {
            return speaker != null;
        }

        async Task GetSpeakersCommandExecute() {
            if (!CanGetSpeakersCommandExecute()) {
                return;
            }

            try {
                this.IsBusy = true;
                var items = await _speakerService.Get(Constants.SpeakersUrl);

                this.SpeakersCollectionHasItems = false;
                this.Speakers.Clear();
                foreach (var speaker in items) {
                    this.Speakers.Add(speaker);
                }
                this.SpeakersCollectionHasItems = this.Speakers.Count > 0;
            } catch (Exception ex) {
                await base.HandleException(ex);
            } finally {
                this.IsBusy = false;
            }
        }

        async Task SpeakerSelectedCommandExecute(Speaker speaker) {
            if (!CanSpeakerSelectedCommand(speaker)) {
                return;
            }
            this.SelectedSpeaker = null;
            try {
                if (speaker.Name.StartsWith("M")) {
                    await base.NavigateToUri("BlowUp", speaker);
                } else {
                    await base.NavigateToUri(Constants.AsyncAwaitSpeakerDetailPage, speaker);
                }
            } catch (Exception ex) {
                await base.HandleException(ex);
            }
        }

    }
}
