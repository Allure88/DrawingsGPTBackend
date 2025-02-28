#nullable enable
using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Domain.Bodies.Dimensions
{
    public class DimensionBody
    {
        public Point2Dbody StartPoint { get; set; } = null!;
        public Point2Dbody EndPoint { get; set; } = null!;
        public Point2Dbody TextPoint { get; set; } = null!;
    }

}
