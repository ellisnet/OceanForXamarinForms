namespace Ocean.XamarinForms.Services {
    using System;
    using Xamarin.Forms;

    public interface IDeviceService {

        TargetPlatform OS { get; }

        void BeginInvokeOnMainThread(Action action);

        Double GetNamedSize(NamedSize size, Element targetElement);

        Double GetNamedSize(NamedSize size, Type targetElementType);

        void OnPlatform(Action iOS = null, Action android = null, Action windowsPhone = null, Action defaultAction = null);

        void OpenUri(Uri uri);

        void StartTimer(TimeSpan interval, Func<Boolean> callback);

    }
}