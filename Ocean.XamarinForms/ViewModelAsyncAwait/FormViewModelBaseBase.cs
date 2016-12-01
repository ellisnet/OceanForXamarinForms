namespace Ocean.XamarinForms.ViewModelAsyncAwait {
    using System;
    using System.Threading.Tasks;
    using Ocean.Portable;
    using Ocean.Portable.ViewModel;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    public abstract class FormViewModelBaseBase : ViewModelBase, INavigationAware {

        DelegateCommand<String> _navigateToUriCommand;

        public DelegateCommand<String> NavigateToUriCommand => _navigateToUriCommand ?? (_navigateToUriCommand = new DelegateCommand<String>(async param => await NavigateToUriCommandExecute(param), CanNavigateToUriCommandExecute));

        protected INavigationService NavigationService { get; }

        protected IPageDialogService PageDialogService { get; }

        protected FormViewModelBaseBase(IPageDialogService pageDialogService, INavigationService navigationService) {
            if (pageDialogService == null) {
                throw new ArgumentNullException(nameof(pageDialogService));
            }
            if (navigationService == null) {
                throw new ArgumentNullException(nameof(navigationService));
            }
            this.PageDialogService = pageDialogService;
            this.NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters) {
        }

        protected async Task DisplayDialog(String title, String message, String buttonText = GlobalConstants.OK) {
            try {
                await this.PageDialogService.DisplayAlertAsync(title, message, buttonText);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        protected async Task<Boolean> DisplayDialog(String title, String message, String acceptButtonText, String cancellationButtonText) {
            try {
                return await this.PageDialogService.DisplayAlertAsync(title, message, acceptButtonText, cancellationButtonText);
            } catch (Exception ex) {
                await HandleException(ex);
                return false;
            }
        }

        protected async Task GoBack() {
            try {
                await this.NavigationService.GoBackAsync();
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        protected Task HandleException(Exception ex) {
            this.IsBusy = false;
            var baseException = ex.GetBaseException();
            return this.PageDialogService.DisplayAlertAsync(GlobalConstants.Error, baseException.Message, GlobalConstants.OK);
        }

        protected async Task NavigateToUri(String uriText) {
            if (String.IsNullOrWhiteSpace(uriText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uriText));
            }
            try {
                await this.NavigationService.NavigateAsync(uriText);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        protected async Task NavigateToUri(String uriText, Object parameter) {
            if (String.IsNullOrWhiteSpace(uriText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uriText));
            }
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            try {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add(GlobalConstants.Key, parameter);
                await NavigateToUri(uriText, navigationParameters);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        protected async Task NavigateToUri(String uriText, NavigationParameters navigationParameters) {
            if (String.IsNullOrWhiteSpace(uriText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uriText));
            }
            if (navigationParameters == null) {
                throw new ArgumentNullException(nameof(navigationParameters));
            }
            try {
                await this.NavigationService.NavigateAsync(uriText, navigationParameters);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        Boolean CanNavigateToUriCommandExecute(String uriText) {
            return !String.IsNullOrWhiteSpace(uriText);
        }

        async Task NavigateToUriCommandExecute(String uriText) {
            try {
                this.IsBusy = true;
                await NavigateToUri(uriText);
            } catch (Exception ex) {
                await HandleException(ex);
            } finally {
                this.IsBusy = false;
            }
        }

    }
}
