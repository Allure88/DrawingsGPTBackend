using DrawingsGPTBackend.Domain.Bodies.Dimensions;

namespace DrawingsGPTBackend.Application.UseCases.PlaceDimensions;

public class DimensionsInteractor(CommonDimensionPlacer commonDimensionPlacer)
{
    public DimensionsResponce PlaceCommonDimensions(DimensionsRequest request)
    {
        List<DimensionBody> result = [];

        (ViewLinesBodyExtd baseView, ViewLinesBodyExtd rightSideView) = GetSpecificViews(request.LineViews);



        DimensionBody? horizontalCommonDim =
            commonDimensionPlacer.PlaceCommonDim(baseView.OrdVerticalLines, baseView.LeftPoint, baseView.RightPoint, true, baseView.Top);
        DimensionBody? verticalCommonDim = commonDimensionPlacer.PlaceCommonDim(baseView.OrdHorizontalLines, baseView.BottomPoint, baseView.TopPoint, false, baseView.Right);


        DimensionBody? horizontalProjectCommonDim =
            commonDimensionPlacer.PlaceCommonDim(rightSideView.OrdVerticalLines, rightSideView.LeftPoint, rightSideView.RightPoint, true, rightSideView.Top);

        if (horizontalCommonDim != null)
            result.Add(horizontalCommonDim);
        if (verticalCommonDim != null)
            result.Add(verticalCommonDim);
        if (horizontalProjectCommonDim != null)
            result.Add(horizontalProjectCommonDim);


        return new(true, "", result);
    }



    //только если не более 3 видов на чертеже
    private (ViewLinesBodyExtd baseView, ViewLinesBodyExtd rightSideView) GetSpecificViews(List<ViewLinesBody> lineViews)
    {
        var views = lineViews.Select(v => new ViewLinesBodyExtd(v)).ToList();

        ViewLinesBodyExtd baseView = views[0];
        ViewLinesBodyExtd rightSideView = views[0];

        foreach (var view in views)
        {
            if (view.Left + 0.1 < baseView.Left)
                baseView = view;
            else if (view.Top - 0.1 > baseView.Top)
                baseView = view;

            if (view.Right - 0.1 > rightSideView.Right)
                rightSideView = view;
            else if (view.Top - 0.1 > rightSideView.Top)
                rightSideView = view;
        }
        return (baseView, rightSideView);
    }
}
