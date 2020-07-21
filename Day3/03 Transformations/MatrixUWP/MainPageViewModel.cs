using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace MatrixUWP
{
    public class MainPageViewModel : ViewModelBase
    {
        private MatrixTransform _contentTransform = new MatrixTransform() { Matrix = Matrix.Identity };

        public MatrixTransform ContentTransform
        {
            get => _contentTransform;
            set
            {
                Set(nameof(ContentTransform), ref _contentTransform, value);
            }
        }

        public void WheelChanged(Point position, double delta)
        {
            CompositeTransform deltaTransform = new CompositeTransform()
            {
                CenterX = position.X,
                CenterY = position.Y
            };

            if (delta > 0)
            {
                deltaTransform.Rotation = 15;
            }
            else
            {
                deltaTransform.Rotation = -15;
            }

            RefreshContent(deltaTransform);
        }

        private void RefreshContent(CompositeTransform deltaTransform)
        {
            var transformGroup = new TransformGroup();

            transformGroup.Children.Add(ContentTransform);
            transformGroup.Children.Add(deltaTransform);
            ContentTransform.Matrix = transformGroup.Value;
        }
    }
}
