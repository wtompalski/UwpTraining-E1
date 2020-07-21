using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVVM.Communication;
using MVVM.Model;
using Windows.Foundation;

namespace MVVM.ViewModel
{
    public class DrawingViewModel
    {
        public RelayCommand<Point> PointPressedCommand { get; }
        public RelayCommand<Point> PointMovedCommand { get; }
        public RelayCommand<Point> PointReleasedCommand { get; }

        private Drawing _dataModel = new Drawing();
        private bool _isDrawing = false;

        public DrawingViewModel()
        {
            PointPressedCommand = new RelayCommand<Point>((point) => OnIncommingPointPressed(point));
            PointMovedCommand = new RelayCommand<Point>((point) => OnIncommingPointMoved(point));
            PointReleasedCommand = new RelayCommand<Point>((point) => OnIncommingPointReleased(point));
        }

        private void OnIncommingPointPressed(Point point)
        {
            _isDrawing = true;
            _dataModel.AddStroke(new DrawingStroke());
            Messenger.Default.Send<DrawingStrokeMessage>(new DrawingStrokeMessage());
        }

        private void OnIncommingPointMoved(Point point)
        {
            if (_isDrawing)
            {
                _dataModel.Strokes[_dataModel.Strokes.Count - 1].AddPoint(point);
                Messenger.Default.Send<DrawingPointMessage>(new DrawingPointMessage(point.X, point.Y));
            }
        }

        private void OnIncommingPointReleased(Point point)
        {
            _isDrawing = false;
        }
    }
}

