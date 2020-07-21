using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace MVVM.View.Converters
{
    public class PointerEventToPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as PointerRoutedEventArgs;

            if (args != null)
            {
                var drawing = args.OriginalSource as UIElement;
                var pointer = args.GetCurrentPoint(drawing);
                return new Point(pointer.Position.X, pointer.Position.Y);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

