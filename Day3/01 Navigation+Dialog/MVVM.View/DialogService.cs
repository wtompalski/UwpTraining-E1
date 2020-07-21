using MVVM.ViewModel;
using System;

namespace MVVM.View
{
    /// <summary>
    /// Based on https://csharperimage.jeremylikness.com/2010/01/simple-dialog-service-in-silverlight.html
    /// </summary>
    public class DialogService : IDialogServiceCustom
    {
        public async void ShowDialog(string title, string message, Action<bool> response)
        {
            var dialog = new MessageDialog() { Title = title, Message = message, CloseAction = response };
            dialog.PrimaryButtonClick += Dialog_ButtonClick;
            dialog.SecondaryButtonClick += Dialog_ButtonClick;
            await dialog.ShowAsync();
        }

        private void Dialog_ButtonClick(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogButtonClickEventArgs args)
        {
            var dialog = sender as MessageDialog;

            if (dialog != null)
            {
                dialog.CloseButtonClick -= Dialog_ButtonClick;
                dialog.SecondaryButtonClick -= Dialog_ButtonClick;
                dialog.CloseAction(dialog.DialogResult == true);
            }
        }
    }
}
