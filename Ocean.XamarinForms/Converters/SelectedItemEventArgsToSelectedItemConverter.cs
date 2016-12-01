namespace Ocean.XamarinForms.Converters {
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            var eventArgs = value as SelectedItemChangedEventArgs;
            return eventArgs?.SelectedItem;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}