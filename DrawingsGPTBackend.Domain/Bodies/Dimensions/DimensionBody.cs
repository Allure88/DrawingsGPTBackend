using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Domain.Bodies.Dimensions
{
    public class DimensionBody
    {
        public IntentLineBody Start { get; set; } = null!;
        public IntentLineBody End { get; set; } = null!;
        public Point2Dbody TextPoint { get; set; } = null!;
    }

}
