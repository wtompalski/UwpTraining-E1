using System;

namespace MVVM.ViewModel
{
    public interface IDialogServiceCustom
    {
        void ShowDialog(string title, string message, Action<bool> response);
    }
}
