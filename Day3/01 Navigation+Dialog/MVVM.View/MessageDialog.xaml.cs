using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace MVVM.View
{
    /// <summary>
    /// Based on https://csharperimage.jeremylikness.com/2010/01/simple-dialog-service-in-silverlight.html
    /// </summary>
    public sealed partial class MessageDialog : ContentDialog
    {
        public bool DialogResult = false;
        public MessageDialog()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DialogResult = true;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DialogResult = false;
        }

        public Action<bool> CloseAction { get; set; }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message",
            typeof(string),
            typeof(MessageDialog),
            null);


        public string Message
        {
            get { return GetValue(MessageProperty).ToString(); }
            set { SetValue(MessageProperty, value); }
        }
    }
}
