namespace MVVM.View
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;
    using MVVM.ViewModel;


    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Register();
        }

        public void Register()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<IDialogServiceCustom, DialogService>();
            SimpleIoc.Default.Register<INavigationService>(() => CreateNavigationService());
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("NewPage", typeof(NewPage));

            return navigationService;
        }
    }
}
