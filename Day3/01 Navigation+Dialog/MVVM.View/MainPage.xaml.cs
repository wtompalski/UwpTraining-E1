namespace MVVM.View
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Messaging;
    using MVVM.ViewModel;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.MainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SubscribeToMessages();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            UnsubscribeMessages();
            MainViewModel.CleanUp();
        }

        private void SubscribeToMessages()
        {
            Messenger.Default.Register<bool>(this, (result) => OnIncomingDialogResult(result));
        }

        private void UnsubscribeMessages()
        {
            Messenger.Default.Unregister<bool>(this);
        }

        private void OnIncomingDialogResult(bool result)
        {
            TextDialog.Text = result ? "OK" : "Cancel";
        }
    }
}
