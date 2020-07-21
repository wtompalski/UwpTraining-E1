using System.Collections.Generic;
using Windows.Foundation;

namespace MVVM.Model
{
    public class DrawingStroke
    {
        public List<Point> Points { get; }

        public DrawingStroke()
        {
            Points = new List<Point>();
        }

        public void AddPoint(Point point)
        {
            Points.Add(point);
        }
    }
}
