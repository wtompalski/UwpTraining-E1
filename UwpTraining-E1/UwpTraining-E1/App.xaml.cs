using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UwpTraining_E1.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UwpTraining_E1
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += OnResuming;

            this.EnteredBackground += OnEnteredBackground;
            this.LeavingBackground += OnLeavingBackground;
        }

        private void OnEnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            Debug.WriteLine("OnEnteredBackground with " + MemoryManager.AppMemoryUsageLevel);

            var def = e.GetDeferral();

            // ....
            def.Complete();
        }

        private void OnLeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            Debug.WriteLine("OnLeavingBackground");
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Debug.WriteLine("OnLaunched " + e.PreviousExecutionState);

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(Weather), e.Arguments);
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            Debug.WriteLine("OnSuspending");

            var deferral = e.SuspendingOperation.GetDeferral();

            await this.DoWork();

            deferral.Complete();

            Debug.WriteLine("OnSuspended");
        }

        private void OnResuming(object sender, object e)
        {
            Debug.WriteLine("OnResuming");
        }

        private async Task DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.WriteLine("DoWork " + i);
                await Task.Delay(10);
            }
            //await Task.Delay(10000);
        }

        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            base.OnBackgroundActivated(args);

            new UwpTraining_E1.Background.BackgroundTask().Run(args.TaskInstance);
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            var toastArgs = args as ToastNotificationActivatedEventArgs;

            Debug.WriteLine(toastArgs.Argument);

            base.OnActivated(args);
        }
    }
}
