using AppView.Shared;

namespace AppView.Services
{
    public interface IToastService
    {
        void ShowSuccess(string title, string message, int autoHideDelay = 5000);
        void ShowError(string title, string message, int autoHideDelay = 8000);
        void ShowWarning(string title, string message, int autoHideDelay = 6000);
        void ShowInfo(string title, string message, int autoHideDelay = 5000);
        void ShowToast(string title, string message, Toast.ToastType type = Toast.ToastType.Info, int autoHideDelay = 5000);
        void ClearAll();
    }
}
