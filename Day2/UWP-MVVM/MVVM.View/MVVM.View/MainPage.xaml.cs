using GalaSoft.MvvmLight.Messaging;
using MVVM.Communication;
using MVVM.ViewModel;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MVVM.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Point? _lastPoint;
        bool _isDrawing = false;
        public DrawingViewModel DrawingViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.DrawingViewModel = new DrawingViewModel();
        }

        private void Drawing_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //_isDrawing = true;
        }

        private void Drawing_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //if (_isDrawing)
            //{
            //    var pointer = e.GetCurrentPoint(Drawing);
            //    AddPointToTheCurrentDrawing(pointer.Position.X, pointer.Position.Y);
            //}
        }

        private void Drawing_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //_isDrawing = false;
            //_lastPoint = null;
        }

        private void AddPointToTheCurrentDrawing(double x, double y)
        {
            Point endPoint = new Point(x, y);
            Point startPoint;

            if (_lastPoint != null)
            {
                startPoint = (Point)_lastPoint;
            }
            else
            {
                startPoint = endPoint;
            }

            LineGeometry line = new LineGeometry()
            {
                StartPoint = startPoint,
                EndPoint = endPoint
            };

            DrawingGeometry.Children.Add(line);
            _lastPoint = endPoint;
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
        }

        private void SubscribeToMessages()
        {
            Messenger.Default.Register<DrawingStrokeMessage>(this, (message) => OnIncomingDrawingStrokeMessage(message));
            Messenger.Default.Register<DrawingPointMessage>(this, (message) => OnIncomingDrawingPointMessage(message));
        }

        private void UnsubscribeMessages()
        {
            Messenger.Default.Unregister<DrawingStrokeMessage>(this);
            Messenger.Default.Unregister<DrawingPointMessage>(this);
        }

        private void OnIncomingDrawingStrokeMessage(DrawingStrokeMessage drawingStrokeMessage)
        {
            _lastPoint = null;
        }

        private void OnIncomingDrawingPointMessage(DrawingPointMessage drawingPointMessage)
        {
            AddPointToTheCurrentDrawing(drawingPointMessage.X, drawingPointMessage.Y);
        }
    }
}
