using DrawingsGPTBackend.Domain;
using DrawingsGPTBackend.Domain.Bodies.Dimensions;
using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Application.UseCases.PlaceDimensions;

public class CommonDimensionPlacer
{

    internal DimensionBody? PlaceCommonDim(List<Line2DBody> ordLines, Point2Dbody minViewPoint, Point2Dbody maxViewPoint, bool isHorizontDimensionMode, double rightOrTopCoord)
    {
        IntentLineBody startIntent;
        IntentLineBody endIntent;
        Point2Dbody textPoint;

        if(ordLines.Count == 0)
            return null;

        //min 

        double minLineCoord = isHorizontDimensionMode
                ? ordLines[0].StartPoint.X
                : ordLines[0].StartPoint.Y;

        double minViewCoord = isHorizontDimensionMode
               ? minViewPoint.X
               : minViewPoint.Y;



        if (minLineCoord.EqualsTo(minViewCoord))
        {
            startIntent = new()
            {
                Guid = ordLines[0].Guid,
                PointType = PointIntentTypeBody.None
            };
        }
        else
        {
            Line2DBody? pointLine = ordLines.FirstOrDefault(l => l.StartPoint.X.EqualsTo(minViewPoint.X)
            && l.StartPoint.Y.EqualsTo(minViewPoint.Y));
            pointLine = ordLines.First(l => l.EndPoint.X.EqualsTo(minViewPoint.X)
            && l.EndPoint.Y.EqualsTo(minViewPoint.Y));

            PointIntentTypeBody pointType = minViewPoint.IsStart
                ? PointIntentTypeBody.Start
                : PointIntentTypeBody.End;

            startIntent = new()
            {
                Guid = pointLine.Guid,
                PointType = pointType
            };
        }

        //max

        double maxLineCoord = isHorizontDimensionMode
                ? ordLines.Last().StartPoint.X
                : ordLines.Last().StartPoint.Y;

        double maxViewCoord = isHorizontDimensionMode
               ? maxViewPoint.X
               : maxViewPoint.Y;

        if (maxLineCoord.EqualsTo(maxViewCoord))
        {
            endIntent = new()
            {
                Guid = ordLines.Last().Guid,
                PointType = PointIntentTypeBody.None
            };
        }
        else
        {
            Line2DBody? pointLine = ordLines.FirstOrDefault(l => l.StartPoint.X.EqualsTo(maxViewPoint.X)
            && l.StartPoint.Y.EqualsTo(maxViewPoint.Y));
            pointLine = ordLines.First(l => l.EndPoint.X.EqualsTo(maxViewPoint.X)
            && l.EndPoint.Y.EqualsTo(maxViewPoint.Y));

            PointIntentTypeBody pointType = minViewPoint.IsStart
                ? PointIntentTypeBody.Start
                : PointIntentTypeBody.End;

            endIntent = new()
            {
                Guid = pointLine.Guid,
                PointType = pointType
            };
        }

        //text
        double X;
        double Y;

        if (isHorizontDimensionMode)
        {
            X = (minViewPoint.X + maxViewPoint.X) / 2;
            Y = rightOrTopCoord + 1.5;
        }
        else
        {
            X = rightOrTopCoord + 1.5;
            Y = (minViewPoint.Y + maxViewPoint.Y) / 2;
        }

        textPoint = new()
        {
            X = X,
            Y = Y
        };

        DimensionBody dimensionBody = new()
        {
            Start = startIntent,
            End = endIntent,
            TextPoint = textPoint
        };

        return dimensionBody;


    }
}
