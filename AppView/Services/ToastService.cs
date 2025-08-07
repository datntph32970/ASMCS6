using AppView.Shared;

namespace AppView.Services
{
    public class ToastService : IToastService
    {
        private Toast? _toastComponent;

        public void SetToastComponent(Toast toastComponent)
        {
            _toastComponent = toastComponent;
        }

        public void ShowSuccess(string title, string message, int autoHideDelay = 5000)
        {
            _toastComponent?.ShowSuccess(title, message, autoHideDelay);
        }

        public void ShowError(string title, string message, int autoHideDelay = 8000)
        {
            _toastComponent?.ShowError(title, message, autoHideDelay);
        }

        public void ShowWarning(string title, string message, int autoHideDelay = 6000)
        {
            _toastComponent?.ShowWarning(title, message, autoHideDelay);
        }

        public void ShowInfo(string title, string message, int autoHideDelay = 5000)
        {
            _toastComponent?.ShowInfo(title, message, autoHideDelay);
        }

        public void ShowToast(string title, string message, Toast.ToastType type = Toast.ToastType.Info, int autoHideDelay = 5000)
        {
            _toastComponent?.ShowToast(title, message, type, autoHideDelay);
        }

        public void ClearAll()
        {
            _toastComponent?.ClearAll();
        }
    }
}
