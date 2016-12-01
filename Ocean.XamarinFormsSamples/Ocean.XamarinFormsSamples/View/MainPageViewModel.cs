namespace Ocean.XamarinFormsSamples.View {
    using Ocean.XamarinForms.ViewModel;
    using Prism.Navigation;
    using Prism.Services;

    public class MainPageViewModel : FormViewModelBaseBase {

        public MainPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
            : base(pageDialogService, navigationService) {
        }

    }
}
