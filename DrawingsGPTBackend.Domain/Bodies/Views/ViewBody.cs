using DrawingsGPTBackend.Domain.Bodies.Geometry2D;

namespace DrawingsGPTBackend.Domain.Bodies.Views;
public class BaseViewBody
{
    public string UniqueName { get; set; } = string.Empty;
    public Point2Dbody CenterPosition { get; set; } = null!;
    public double Scale { get; set; }
    public ViewOrientationTypeEnumBody Orientation { get; set; }
}

public class ProjectViewBody
{
    public string UniqueName { get; set; } = string.Empty;
    public Point2Dbody CenterPosition { get; set; } = null!;
    public BaseViewBody ParentView { get; set; } = null!;
}

public enum ViewOrientationTypeEnumBody
{
    kDefaultViewOrientation,
    kTopViewOrientation,
    kRightViewOrientation,
    kBackViewOrientation,
    kBottomViewOrientation,
    kLeftViewOrientation,
    kIsoTopRightViewOrientation,
    kIsoTopLeftViewOrientation,
    kIsoBottomRightViewOrientation,
    kIsoBottomLeftViewOrientation,
    kArbitraryViewOrientation,
    kFrontViewOrientation,
    kCurrentViewOrientation,
    kSavedCameraViewOrientation,
    kFlatPivotRightViewOrientation,
    kFlatPivotLeftViewOrientation,
    kFlatPivot180ViewOrientation,
    kFlatBacksideViewOrientation,
    kFlatBacksidePivotRightViewOrientation,
    kFlatBacksidePivotLeftViewOrientation,
    kFlatBacksidePivot180ViewOrientation
}
