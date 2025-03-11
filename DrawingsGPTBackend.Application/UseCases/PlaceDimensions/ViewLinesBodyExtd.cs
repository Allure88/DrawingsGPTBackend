using DrawingsGPTBackend.Domain.Bodies.Dimensions;
using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Application.UseCases.PlaceDimensions
{
    public class ViewLinesBodyExtd : ViewLinesBody
    {
        public double Left { get; }
        public Point2Dbody LeftPoint { get; }
        public double Right { get; }
        public Point2Dbody RightPoint { get; }
        public double Top { get; }
        public Point2Dbody TopPoint{ get; }
        public double Bottom { get; }
        public Point2Dbody BottomPoint { get; }

        public List<Line2DBody> OrdVerticalLines { get; }
        public List <Line2DBody> OrdHorizontalLines { get; }

        public ViewLinesBodyExtd(ViewLinesBody input)
        {
            Lines = input.Lines;
            List<Point2Dbody> allPoints = [.. Lines.Select(l => l.StartPoint)];
            allPoints.AddRange(Lines.Select(l => l.EndPoint));

            List<Point2Dbody> ordXallPoints = [.. allPoints.OrderBy(p => p.X)];
            List<Point2Dbody> ordYallPoints = [.. allPoints.OrderBy(p => p.Y)];

            Left = ordXallPoints[0].X;
            LeftPoint = ordXallPoints[0];

            Right = ordXallPoints[^1].X;
            RightPoint = ordXallPoints[^1];

            Bottom = ordYallPoints[0].Y;
            BottomPoint = ordYallPoints[0];


            Top = ordYallPoints[^1].Y;
            TopPoint = ordYallPoints[^1];

            OrdVerticalLines = [.. Lines.Where(l => l.IsVertical).OrderBy(l => l.StartPoint.X)];
            OrdHorizontalLines = [.. Lines.Where(l => l.IsHorizontal).OrderBy(l => l.StartPoint.Y)];
        }
    }
}
