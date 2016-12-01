namespace Ocean.XamarinForms.Services {
    using System;
    using Xamarin.Forms;

    public class DeviceService : IDeviceService {

        public TargetPlatform OS => Device.OS;

        public DeviceService() {
        }

        public void BeginInvokeOnMainThread(Action action) {
            Device.BeginInvokeOnMainThread(action);
        }

        public Double GetNamedSize(NamedSize size, Element targetElement) {
            return Device.GetNamedSize(size, targetElement);
        }

        public Double GetNamedSize(NamedSize size, Type targetElementType) {
            return Device.GetNamedSize(size, targetElementType);
        }

        public void OnPlatform(Action iOS = null, Action android = null, Action windowsPhone = null, Action defaultAction = null) {
            Device.OnPlatform(iOS, android, windowsPhone, defaultAction);
        }

        public void OpenUri(Uri uri) {
            Device.OpenUri(uri);
        }

        public void StartTimer(TimeSpan interval, Func<Boolean> callback) {
            Device.StartTimer(interval, callback);
        }

    }
}
