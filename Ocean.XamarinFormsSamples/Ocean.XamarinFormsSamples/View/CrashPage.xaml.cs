namespace Ocean.XamarinFormsSamples.View {
    using System;
    using Xamarin.Forms;

    public partial class CrashPage : ContentPage {

        public String ExceptionMessage { get; } = String.Empty;

        public CrashPage() {
            InitializeComponent();
            this.BindingContext = this;
        }

        public CrashPage(String exceptionMessage) {
            this.ExceptionMessage = exceptionMessage;
            InitializeComponent();
            this.BindingContext = this;
        }

    }
}
