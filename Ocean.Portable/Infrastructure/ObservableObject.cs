namespace Ocean.Portable.Infrastructure {
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ObservableObject : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            PropertyChanged?.Invoke(this, args);
        }

        protected void RaisePropertyChanged([CallerMemberName] String propertyName = null) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }
}
