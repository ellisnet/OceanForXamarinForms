
#pragma warning disable 4014

namespace Ocean.XamarinFormsSamples {
    using System;
    using System.Threading.Tasks;
    using Microsoft.Practices.Unity;
    using Ocean.Portable.PrismUnity;
    using Ocean.XamarinForms.Services;
    using Ocean.XamarinFormsSamples.Services;
    using Ocean.XamarinFormsSamples.View;
    using Prism.Unity;
    using Xamarin.Forms;

    public partial class App : PrismApplication {

        public App(IPlatformInitializer initializer = null)
            : base(initializer) {
        }

        //TODO: Brian Prism app crashes when an unknown broken URL is passed here.
        //     Karl step through Prism code to find the issue.
        protected override void OnInitialized() {
            InitializeComponent();
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            NavigateToMainPage();
        }

        protected override void RegisterTypes() {
            // framework
            this.Container.RegisterType<IHttpClient, HttpClient>();
            this.Container.RegisterType<IResolver<IHttpClient>, UnityResolver<IHttpClient>>();

            // services
            this.Container.RegisterType<ISpeakerService, SpeakerService>();
            this.Container.RegisterType<ICrossTextToSpeechService, CrossTextToSpeechService>();
            this.Container.RegisterType<IDeviceService, DeviceService>();
            this.Container.RegisterType<ICustomerService, CustomerService>();
            this.Container.RegisterType<IEventSignUpService, EventSignUpService>();

            // pages
            this.Container.RegisterTypeForNavigation<NavigationPage>();
            this.Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
            this.Container.RegisterTypeForNavigation<SpeakerMasterPage, SpeakerMasterPageViewModel>();
            this.Container.RegisterTypeForNavigation<SpeakerDetailPage, SpeakerDetailPageViewModel>();
            this.Container.RegisterTypeForNavigation<ViewAsyncAwait.SpeakerMasterPage, ViewAsyncAwait.SpeakerMasterPageViewModel>(Constants.AsyncAwaitSpeakerMasterPage);
            this.Container.RegisterTypeForNavigation<ViewAsyncAwait.SpeakerDetailPage, ViewAsyncAwait.SpeakerDetailPageViewModel>(Constants.AsyncAwaitSpeakerDetailPage);
            this.Container.RegisterTypeForNavigation<ValidationDemoOnePage, ValidationDemoOnePageViewModel>();
            this.Container.RegisterTypeForNavigation<ValidationDemoTwoPage, ValidationDemoTwoPageViewModel>();
            this.Container.RegisterTypeForNavigation<EventPageOne, EventPageOneViewModel>();
            this.Container.RegisterTypeForNavigation<EventPageTwo, EventPageTwoViewModel>();
        }

        async Task NavigateToMainPage() {
            try {
                await NavigationService.NavigateAsync(Constants.InitialUrl);
            } catch (Exception ex) {
                ShowCrashPage(ex);
            }
        }

        void ShowCrashPage(Exception ex = null) {
            Device.BeginInvokeOnMainThread(() => this.MainPage = new CrashPage(ex?.Message));

            //TODO: Add logging and handle this the way you want
            //Logging would be a good idea too so you can correct the problem.
        }

        void TaskScheduler_UnobservedTaskException(Object sender, UnobservedTaskExceptionEventArgs e) {
            if (!e.Observed) {
                // prevents the app domain from being torn down
                e.SetObserved();

                // show the crash page
                ShowCrashPage(e.Exception.Flatten().GetBaseException());
            }
        }

    }
}
