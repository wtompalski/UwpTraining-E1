using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MatrixUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public readonly MainPageViewModel MainPageViewModel = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();
            Canvas.Width = Window.Current.Bounds.Width;
            Canvas.Height = Window.Current.Bounds.Height;
        }

        private void Space_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(Space);
            var delta = pointer.Properties.MouseWheelDelta;

            MainPageViewModel.WheelChanged(pointer.Position, delta);
            e.Handled = true;
        }
    }
}
