using System.Text.Json.Serialization;

namespace DrawingsGPTBackend.Domain.Bodies.Geometry2D
{
    public class Line2DBody
    {
        private Point2Dbody startPoint = null!;
        private Point2Dbody endPoint = null!;


        public string Guid { get; set; } = string.Empty;
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

        public UnitVector2dbody? Direction { get; set; }

        [JsonIgnore]
        public bool IsVertical => Direction?.X.EqualsTo(0) ?? false;
        [JsonIgnore]
        public bool IsHorizontal => Direction?.Y.EqualsTo(0) ?? false;

        [JsonIgnore]
        public double Length { get; private set; }

        private void SetLength()
        {
            Length = Math.Sqrt(Math.Pow((EndPoint.X - StartPoint.X), 2) + Math.Pow((EndPoint.Y - StartPoint.Y), 2));
        }
    }

}
