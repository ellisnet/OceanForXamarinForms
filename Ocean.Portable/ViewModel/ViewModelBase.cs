namespace Ocean.Portable.ViewModel {
    using System;
    using Ocean.Portable.Infrastructure;

    public abstract class ViewModelBase : ObservableObject {

        Boolean _isBusy;

        public Boolean IsBusy {
            get { return _isBusy; }
            set {
                _isBusy = value;
                RaisePropertyChanged();
                OnIsBusyChanged();
            }
        }

        protected ViewModelBase() {
        }

        protected virtual void OnIsBusyChanged() {
        }

    }
}
