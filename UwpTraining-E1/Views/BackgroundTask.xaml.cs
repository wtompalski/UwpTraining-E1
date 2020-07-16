using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UwpTraining_E1.Background;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
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
        private IBackgroundTrigger trigger = null;

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

            trigger = new ToastNotificationActionTrigger();
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

            await UpdateUI();
        }

        private async void UnregisterBackgroundTask(object sender, RoutedEventArgs e)
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

            await UpdateUI();
        }

        private async void SignalBackgroundTask(object sender, RoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(AppBackgroundState.ApplicationTriggerTaskName);
            var p = new ValueSet();
            p.Add("par1", "val1");
            // var result = await trigger.RequestAsync(p);
            // AppBackgroundState.ApplicationTriggerTaskResult = "Signal result: " + result.ToString();
            await UpdateUI();
        }

        private async void Task_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            var progress = "Progress: " + args.Progress + "%";
            AppBackgroundState.ApplicationTriggerTaskProgress = progress;
            await UpdateUI();
        }

        private async void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            await UpdateUI();
            this.ShowMeToast();
        }

        private async Task UpdateUI()
        {
            object taskStatus;
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.TryGetValue(AppBackgroundState.ApplicationTriggerTaskName, out taskStatus);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    RegisterButton.IsEnabled = !AppBackgroundState.ApplicationTriggerTaskRegistered;
                    UnregisterButton.IsEnabled = AppBackgroundState.ApplicationTriggerTaskRegistered;
                    SignalButton.IsEnabled = AppBackgroundState.ApplicationTriggerTaskRegistered & (trigger != null);
                    Progress.Text = AppBackgroundState.ApplicationTriggerTaskProgress;
                    Result.Text = AppBackgroundState.ApplicationTriggerTaskResult;
                    Status.Text = AppBackgroundState.ApplicationTriggerTaskRegistered ? "Registered " : "Unregistered " + taskStatus;
                });
        }

        private void ShowToast_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMeToast();
        }

        private void ShowMeToast()
        {
            string title = "Alice sends you a cat picture";
            string content = "Look at this cuttie!";
            string image = "http://placekitten.com/200/300";
            string logo = "ms-appdata:///local/kitten.jpg";

            var visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },

                        new AdaptiveText()
                        {
                            Text = content
                        },

                        new AdaptiveImage()
                        {
                            Source = image
                        }
                    },

                    AppLogoOverride = new ToastGenericAppLogo()
                    {
                        Source = logo,
                        HintCrop = ToastGenericAppLogoCrop.Circle
                    }
                }
            };

            ToastActionsCustom actions = new ToastActionsCustom()
            {
                Inputs =
                {
                    new ToastTextBox("tbReply")
                    {
                        PlaceholderContent = "Type a response"
                    }
                },

                Buttons =
                {
                    new ToastButton("Reply", "action=reply")
                    {
                        ActivationType = ToastActivationType.Background,
                        ImageUri = "Assets/Reply.png",
 
                        // Reference the text box's ID in order to
                        // place this button next to the text box
                        TextBoxId = "tbReply"
                    },

                    new ToastButton("Like", "action=like")
                    {
                        ActivationType = ToastActivationType.Background
                    },

                    new ToastButton("View", "action=viewImage")
                }
            };


            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions
            };

            var xml = toastContent.GetXml().GetXml();

            Debug.WriteLine(xml);

            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
