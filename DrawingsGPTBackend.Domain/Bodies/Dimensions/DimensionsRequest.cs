#nullable enable
using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Domain.Bodies.Dimensions
{
    public class DimensionsRequest
    {
        public List<Line2DBody> Lines { get; set; } = null!;
    }

}
