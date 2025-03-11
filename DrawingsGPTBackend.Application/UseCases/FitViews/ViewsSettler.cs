using DrawingsGPTBackend.Domain;
using DrawingsGPTBackend.Domain.Bodies.Geometry2D;
using DrawingsGPTBackend.Domain.Bodies.Views;

namespace DrawingsGPTBackend.Application.UseCases.FitViews;

public class ViewsSettler
{
    internal List<ViewBody> PlaceViews(double lengthModel, double heightModel, double widthModel, ViewOrientationTypeEnumBody orientation, double scale, Format format)
    {
        GetDrawingViewsDimensions(lengthModel, heightModel, widthModel, orientation, scale,
            out double baseWidth,
            out double baseHeight,
            out double rightSideWidth,
            out double downSideHeight);

        var (sheetWidth, sheetHeight) = format.GetSheetDimensions();

        (Point2Dbody basePosition, Point2Dbody rightSidePosition, Point2Dbody downSidePosition)
            = GetCoordinates(baseWidth, baseHeight, rightSideWidth, downSideHeight, sheetWidth, sheetHeight);

        BaseViewBody baseViewBody = new()
        {
            UniqueName = "Базовый вид",
            CenterPosition = basePosition,
            Scale = scale,
            Orientation = orientation,
        };

        ProjectViewBody rightSideView = new()
        {
            UniqueName = "Справа",
            CenterPosition = rightSidePosition,
            ParentView = baseViewBody,
        };

        ProjectViewBody downSideView = new()
        {
            UniqueName = "Сперху",
            CenterPosition = downSidePosition,
            ParentView = baseViewBody,
        };

        return ([baseViewBody,rightSideView, downSideView]);
    }

    private static void GetDrawingViewsDimensions(double lengthModel, double heightModel, double widthModel, ViewOrientationTypeEnumBody orientation, double scale, out double baseWidth, out double baseHeight, out double rightSideWidth, out double downSideHeight)
    {
        baseWidth = orientation == ViewOrientationTypeEnumBody.kFrontViewOrientation
            ? lengthModel * scale
            : widthModel * scale;
        baseHeight = heightModel * scale;
        rightSideWidth = orientation == ViewOrientationTypeEnumBody.kFrontViewOrientation
            ? widthModel * scale
            : lengthModel * scale;
        //double righSideHeight = heightModel;

        //double downSideWidth = baseWidth;
        downSideHeight = orientation == ViewOrientationTypeEnumBody.kFrontViewOrientation
            ? widthModel * scale
            : lengthModel * scale;
    }

    private (Point2Dbody basePosition, Point2Dbody rightSidePosition, Point2Dbody downSidePosition) GetCoordinates(double baseWidth, double baseHeight, double rightSideWidth, double downSideHeight, int sheetWidth, int sheetHeight)
    {
        sheetWidth -= 25;//коррекция на рамку для ровных зазоров
        sheetHeight -= 60;
        double widthGap = (sheetWidth - baseWidth - rightSideWidth) / 3;
        double baseX = 20 + widthGap + baseWidth / 2;
        double downSideX = baseX;
        double rightSideX = 20 + widthGap * 2 + baseWidth + rightSideWidth / 2;

        double heightGap = (sheetHeight - baseHeight - downSideHeight) / 3;
        double baseY = sheetHeight + 55 - heightGap - baseHeight / 2;
        double rightSideY = baseY;
        double downSideY = sheetHeight + 55  - heightGap * 2 - baseHeight - downSideHeight / 2;

        Point2Dbody centerPosition = new() { X = baseX, Y = baseY };
        Point2Dbody righSidePosition = new() { X = rightSideX, Y = rightSideY };
        Point2Dbody downSidePosition = new() { X = downSideX, Y = downSideY };

        return (centerPosition, righSidePosition, downSidePosition);
    }
}
