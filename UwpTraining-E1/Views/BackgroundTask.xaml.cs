using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpTraining_E1.Background;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpTraining_E1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BackgroundTask : Page
    {
        private ApplicationTrigger trigger = null;

        public BackgroundTask()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == AppBackgroundState.ApplicationTriggerTaskName)
                {
                    task.Value.Progress += Task_Progress;
                    task.Value.Completed += Task_Completed;

                    AppBackgroundState.ApplicationTriggerTaskRegistered = true;
                    break;
                }
            }

            trigger = new ApplicationTrigger();
            UpdateUI();
        }

        private async void RegisterBackgroundTask(object sender, RoutedEventArgs e)
        {
            var requestResult = await BackgroundExecutionManager.RequestAccessAsync();

            if (requestResult == BackgroundAccessStatus.DeniedByUser)
            {
                return;
            }

            var builder = new BackgroundTaskBuilder();
            builder.Name = AppBackgroundState.ApplicationTriggerTaskName;
            // builder.TaskEntryPoint = ""
            builder.SetTrigger(trigger);

            BackgroundTaskRegistration task = builder.Register();

            AppBackgroundState.ApplicationTriggerTaskRegistered = true;

            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(AppBackgroundState.ApplicationTriggerTaskName);

            task.Progress += Task_Progress;
            task.Completed += Task_Completed;

            UpdateUI();
        }

        private void UnregisterBackgroundTask(object sender, RoutedEventArgs e)
        {
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == AppBackgroundState.ApplicationTriggerTaskName)
                {
                    cur.Value.Unregister(true);
                }
            }

            AppBackgroundState.ApplicationTriggerTaskRegistered = false;
            AppBackgroundState.ApplicationTriggerTaskProgress = string.Empty;

            UpdateUI();
        }

        private async void SignalBackgroundTask(object sender, RoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(AppBackgroundState.ApplicationTriggerTaskName);

            var result = await trigger.RequestAsync();
            AppBackgroundState.ApplicationTriggerTaskResult = "Signal result: " + result.ToString();
            UpdateUI();
        }

        private void Task_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            var progress = "Progress: " + args.Progress + "%";
            AppBackgroundState.ApplicationTriggerTaskProgress = progress;
            UpdateUI();
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            object taskStatus;
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.TryGetValue(AppBackgroundState.ApplicationTriggerTaskName, out taskStatus);

            RegisterButton.IsEnabled = !AppBackgroundState.ApplicationTriggerTaskRegistered;
            UnregisterButton.IsEnabled = AppBackgroundState.ApplicationTriggerTaskRegistered;
            SignalButton.IsEnabled = AppBackgroundState.ApplicationTriggerTaskRegistered & (trigger != null);
            Progress.Text = AppBackgroundState.ApplicationTriggerTaskProgress;
            Result.Text = AppBackgroundState.ApplicationTriggerTaskResult;
            Status.Text = AppBackgroundState.ApplicationTriggerTaskRegistered ? "Registered " : "Unregistered " + taskStatus;
        }
    }
}
