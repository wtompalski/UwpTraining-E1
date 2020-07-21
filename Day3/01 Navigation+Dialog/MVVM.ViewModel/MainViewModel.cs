namespace MVVM.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using System;

    public class MainViewModel : ViewModelBase, IViewModel
    {
        private INavigationService _navigationService;
        private IDialogServiceCustom _dialogService;
 
        public RelayCommand NewPageCommand { get; }
        public RelayCommand ShowDialogCommand { get; }


        public MainViewModel(INavigationService navigationService, IDialogServiceCustom dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            NewPageCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo("NewPage");
            });

            ShowDialogCommand = new RelayCommand(() =>
            {
                _dialogService.ShowDialog(
                "Confirm This",
                "Click OK to confirm or click Cancel",
                result => { 

                    // Reacting to user's action
                    Messenger.Default.Send<bool>(result); 
                });
            });
        }

        public void CleanUp()
        {
            // Clean-up resources
        }
    }
}
