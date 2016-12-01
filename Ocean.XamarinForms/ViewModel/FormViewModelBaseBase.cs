
#pragma warning disable 4014

namespace Ocean.XamarinForms.ViewModel {
    using System;
    using System.Threading.Tasks;
    using Ocean.Portable;
    using Ocean.Portable.ComponentModel;
    using Ocean.Portable.ViewModel;
    using Ocean.XamarinForms.BusinessObject;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    public abstract class FormViewModelBaseBase : ViewModelBase, INavigationAware {

        BusinessEntityBase _businessObject;
        DelegateCommand _deleteCommand;
        DelegateCommand<String> _navigateToUriCommand;
        protected readonly INavigationService _navigationService;
        readonly IPageDialogService _pageDialogService;
        DelegateCommand _saveCommand;
        DelegateCommand _saveOnlyWhenFormValidCommand;

        protected BusinessEntityBase BusinessObject {
            get { return _businessObject; }
            set {
                if (_businessObject != null) {
                    _businessObject.PropertyChanged -= Customer_PropertyChanged;
                }
                _businessObject = value;
                if (_businessObject != null) {
                    _businessObject.PropertyChanged += Customer_PropertyChanged;
                }
            }
        }

        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteCommandExecute));

        public DelegateCommand<String> NavigateToUriCommand => _navigateToUriCommand ?? (_navigateToUriCommand = new DelegateCommand<String>(NavigateToUriCommandExecute, CanNavigateToUriCommandExecute));

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveCommandExecute));

        public DelegateCommand SaveOnlyWhenFormValidCommand => _saveOnlyWhenFormValidCommand ?? (_saveOnlyWhenFormValidCommand = new DelegateCommand(SaveOnlyWhenFormValidCommandExecute, CanSaveOnlyWhenFormValidCommandExecute));

        protected FormViewModelBaseBase(IPageDialogService pageDialogService, INavigationService navigationService) {
            if (pageDialogService == null) {
                throw new ArgumentNullException(nameof(pageDialogService));
            }

            if (navigationService == null) {
                throw new ArgumentNullException(nameof(navigationService));
            }

            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters) {
        }

        protected virtual Boolean CanNavigateToUriCommandExecute(String uriText) {
            return !String.IsNullOrWhiteSpace(uriText);
        }

        protected virtual void DeleteCommandExecute() {
            if (this.BusinessObject == null) {
                return;
            }
            var saveActiveRuleSet = this.BusinessObject.ActiveRuleSet;
            this.BusinessObject.ActiveRuleSet = GlobalConstants.DeleteRule;
            this.BusinessObject.CheckAllRules();
            if (this.BusinessObject.IsValid) {
                DisplayConfirmDeleteActionSheet(result => {
                    if (result == GlobalConstants.Delete) {
                        OnDeleteExecuted();
                    } else {
                        ResetBusinessObject(saveActiveRuleSet);
                    }
                });
            } else {
                DisplayDialog("Invalid Object", $"Object not valid for delete.  {this.BusinessObject.Error}", GlobalConstants.OK, () => {
                    ResetBusinessObject(saveActiveRuleSet);
                });
            }
        }

        protected void DisplayDialog(String title, String message, String buttonText = GlobalConstants.OK, Action callback = null, Action<Exception> errorCallback = null) {
            InvokeAsync(() => _pageDialogService.DisplayAlertAsync(title, message, buttonText), callback, errorCallback, ShowDialog.No);
        }

        protected void DisplayDialog(String title, String message, String acceptButtonText, String cancellationButtonText, Action<Boolean> callback = null, Action<Exception> errorCallback = null) {
            InvokeAsync(() => _pageDialogService.DisplayAlertAsync(title, message, acceptButtonText, cancellationButtonText), callback, errorCallback, ShowDialog.No);
        }

        protected void GoBack() {
            InvokeAsync(() => _navigationService.GoBackAsync());
        }

        protected async Task InvokeAsync<T>(Func<Task<T>> method, Action<T> resultCallback, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
            if (method == null) {
                throw new ArgumentNullException(nameof(method));
            }
            if (resultCallback == null) {
                throw new ArgumentNullException(nameof(resultCallback));
            }
            if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
                throw new InvalidEnumArgumentException(nameof(showDialog), (Int32)showDialog, typeof(ShowDialog));
            }
            try {
                this.IsBusy = true;
                var result = await method.Invoke();
                resultCallback(result);
                this.IsBusy = false;
            } catch (AggregateException ae) {
                await HandleAggregateException(ae, errorCallback, showDialog);
            } catch (Exception ex) {
                await HandleException(ex, errorCallback, showDialog);
            }
        }

        protected async Task InvokeAsync(Func<Task> method, Action resultCallback = null, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
            if (method == null) {
                throw new ArgumentNullException(nameof(method));
            }
            if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
                throw new InvalidEnumArgumentException(nameof(showDialog), (Int32)showDialog, typeof(ShowDialog));
            }

            try {
                this.IsBusy = true;
                await method.Invoke();
                resultCallback?.Invoke();
                this.IsBusy = false;
            } catch (AggregateException ae) {
                await HandleAggregateException(ae, errorCallback, showDialog);
            } catch (Exception ex) {
                await HandleException(ex, errorCallback, showDialog);
            }
        }

        protected void NavigateToUri(String uriText, Action callback = null, Action<Exception> errorCallback = null) {
            InvokeAsync(() => _navigationService.NavigateAsync(uriText), callback, errorCallback);
        }

        protected void NavigateToUri(String uriText, Object parameter, Action callback = null, Action<Exception> errorCallback = null) {
            if (String.IsNullOrWhiteSpace(uriText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uriText));
            }
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(GlobalConstants.Key, parameter);
            NavigateToUri(uriText, navigationParameters, callback, errorCallback);
        }

        protected void NavigateToUri(String uriText, NavigationParameters navigationParameters, Action callback = null, Action<Exception> errorCallback = null) {
            if (String.IsNullOrWhiteSpace(uriText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uriText));
            }
            if (navigationParameters == null) {
                throw new ArgumentNullException(nameof(navigationParameters));
            }
            InvokeAsync(() => _navigationService.NavigateAsync(uriText, navigationParameters), callback, errorCallback);
        }

        protected virtual void NavigateToUriCommandExecute(String uriText) {
            if (!CanNavigateToUriCommandExecute(uriText)) {
                return;
            }
            NavigateToUri(uriText);
        }

        protected virtual void OnDeleteExecuted() {
        }

        protected virtual void OnSaveExecuted() {
        }

        Boolean CanSaveOnlyWhenFormValidCommandExecute() {
            return this.BusinessObject != null && this.BusinessObject.IsValid;
        }

        void Customer_PropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsValid") {
                this.SaveOnlyWhenFormValidCommand.RaiseCanExecuteChanged();
            }
        }

        void DisplayConfirmDeleteActionSheet(Action<String> callback, String title = GlobalConstants.ConfirmDelete, String cancelButtonText = GlobalConstants.Cancel, String deleteButtonText = GlobalConstants.Delete, Action<Exception> errorCallback = null) {
            InvokeAsync(() => _pageDialogService.DisplayActionSheetAsync(title, cancelButtonText, deleteButtonText), callback, errorCallback, ShowDialog.No);
        }

        async Task HandleAggregateException(AggregateException ae, Action<Exception> errorCallback, ShowDialog showDialog) {
            this.IsBusy = false;
            var baseException = ae?.Flatten()?.GetBaseException() ?? new Exception(GlobalConstants.AntecedentExceptionNull);
            if (showDialog == ShowDialog.Yes) {
                await _pageDialogService.DisplayAlertAsync(GlobalConstants.Error, baseException.Message, GlobalConstants.OK);
            }
            errorCallback?.Invoke(baseException);
        }

        async Task HandleException(Exception ex, Action<Exception> errorCallback, ShowDialog showDialog) {
            this.IsBusy = false;
            var baseException = ex.GetBaseException();
            if (showDialog == ShowDialog.Yes) {
                await _pageDialogService.DisplayAlertAsync(GlobalConstants.Error, baseException.Message, GlobalConstants.OK);
            }
            errorCallback?.Invoke(baseException);
        }

        void ResetBusinessObject(String activeRuleSet) {
            this.BusinessObject.ActiveRuleSet = activeRuleSet;
            this.BusinessObject.CheckAllRules();
        }

        void SaveCommandExecute() {
            if (this.BusinessObject == null) {
                return;
            }
            this.BusinessObject.CheckAllRules();
            if (this.BusinessObject.IsNotValid && !this.BusinessObject.IsDirty) {
                this.BusinessObject.SetDirty();
            }
            if (this.BusinessObject.IsNotValid) {
                return;
            }
            OnSaveExecuted();
        }

        void SaveOnlyWhenFormValidCommandExecute() {
            if (CanSaveOnlyWhenFormValidCommandExecute()) {
                OnSaveExecuted();
            }
        }

    }
}
