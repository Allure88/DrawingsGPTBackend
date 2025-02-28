namespace DrawingsGPTBackend.Domain.Bodies.Geometry2D
{
    public class Line2DBody
    {
        private Point2Dbody startPoint = null!;
        private Point2Dbody endPoint = null!;

        public Point2Dbody StartPoint
        {
            get => startPoint; set
            {
                startPoint = value;
                if (EndPoint != null)
                {
                    SetLength();
                }
            }
        }
        public Point2Dbody EndPoint
        {
            get => endPoint;
            set
            {
                endPoint = value;
                if (StartPoint != null)
                {
                    SetLength();
                }
            }
        }
        public Point2Dbody MidPoint { get; set; } = null!;

        public double Length { get; private set; }

        private void SetLength()
        {
            Length = Math.Sqrt(Math.Pow((EndPoint.X - StartPoint.X), 2) + Math.Pow((EndPoint.Y - StartPoint.Y), 2));
        }
    }

}
