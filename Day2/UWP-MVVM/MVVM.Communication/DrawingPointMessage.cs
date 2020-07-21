namespace MVVM.Communication
{
    public class DrawingPointMessage
    {
        public double X { get; }
        public double Y { get; }

        public DrawingPointMessage(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
