using DrawingsGPTBackend.Domain.Bodies.Geometry2D;
using System.Text.Json.Serialization;

namespace DrawingsGPTBackend.Domain.Bodies.Views;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BaseViewBody), typeDiscriminator: "BASE_VIEW")]
[JsonDerivedType(typeof(ProjectViewBody), typeDiscriminator: "PROJECT_VIEW")]
public abstract class ViewBody
{
    public string UniqueName { get; set; } = string.Empty;
    public Point2Dbody CenterPosition { get; set; } = null!;
}

public class BaseViewBody: ViewBody
{    
    public double Scale { get; set; }
    public ViewOrientationTypeEnumBody Orientation { get; set; }
}

public class ProjectViewBody : ViewBody
{
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
