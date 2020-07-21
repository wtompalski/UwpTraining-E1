
using System.Collections.Generic;

namespace MVVM.Model
{
    public class Drawing
    {
        public List<DrawingStroke> Strokes { get; }

        public Drawing()
        {
            Strokes = new List<DrawingStroke>();
        }

        public void AddStroke(DrawingStroke stroke)
        {
            Strokes.Add(stroke);
        }
    }
}

