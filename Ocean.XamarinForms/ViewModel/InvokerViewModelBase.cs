namespace Ocean.XamarinForms.ViewModel {
    using System;
    using System.Threading.Tasks;
    using Ocean.Portable.Services;
    using Ocean.Portable.ViewModel;
    using Prism.Services;

    public abstract class InvokerViewModelBase : ViewModelBase {

        const String AntecedentExceptionNull = "Antecedent Exception was null, possibly collected before it was accessed here.";
        const String EnumError = "Value should be defined in the ShowDialog enum.";
        const String Error = "Error";
        const String OK = "OK";

        protected IPageDialogService PageDialogService { get; }

        protected InvokerViewModelBase(IPageDialogService pageDialogService) {
            if (pageDialogService == null) {
                throw new ArgumentNullException(nameof(pageDialogService));
            }
            this.PageDialogService = pageDialogService;
        }

        protected async Task InvokeAsync<T>(Func<Task<T>> method, Action<T> resultCallback, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
            if (method == null) {
                throw new ArgumentNullException(nameof(method));
            }
            if (resultCallback == null) {
                throw new ArgumentNullException(nameof(resultCallback));
            }
            if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
                throw new ArgumentOutOfRangeException(nameof(showDialog), "Value should be defined in the ShowDialog enum.");
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

        protected async void InvokeAsync(Func<Task> method, Action resultCallback = null, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
            if (method == null) {
                throw new ArgumentNullException(nameof(method));
            }
            if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
                throw new ArgumentOutOfRangeException(nameof(showDialog), "Value should be defined in the ShowDialog enum.");
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

        //protected void InvokeParallelTask<T>(Func<Task<T>> method, Action<T> resultCallback = null, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
        //    if (method == null) {
        //        throw new ArgumentNullException(nameof(method));
        //    }
        //    if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
        //        throw new ArgumentOutOfRangeException(nameof(showDialog), EnumError);
        //    }

        //    this.IsBusy = true;

        //    _taskInvokerService.Invoke(
        //        method,
        //        result => {
        //            this.IsBusy = false;
        //            resultCallback?.Invoke(result);
        //        }, ex => {
        //            this.IsBusy = false;
        //            if (showDialog == ShowDialog.Yes) {
        //                this.PageDialogService.DisplayAlertAsync(Error, ex.Message, OK);
        //            }
        //            errorCallback?.Invoke(ex);
        //        });
        //}

        async Task HandleAggregateException(AggregateException ae, Action<Exception> errorCallback, ShowDialog showDialog) {
            this.IsBusy = false;
            var baseException = ae?.Flatten()?.GetBaseException() ?? new Exception(AntecedentExceptionNull);
            if (showDialog == ShowDialog.Yes) {
                await this.PageDialogService.DisplayAlertAsync(Error, baseException.Message, OK);
            }
            errorCallback?.Invoke(baseException);
        }

        async Task HandleException(Exception ex, Action<Exception> errorCallback, ShowDialog showDialog) {
            this.IsBusy = false;
            var baseException = ex.GetBaseException();
            if (showDialog == ShowDialog.Yes) {
                await this.PageDialogService.DisplayAlertAsync(Error, baseException.Message, OK);
            }
            errorCallback?.Invoke(baseException);
        }

    }
}
