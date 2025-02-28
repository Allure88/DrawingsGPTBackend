using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Domain.Bodies
{
    public record BoundingBoxBody(Point3Dbody LeftBottom, Point3Dbody RightUp);
}
