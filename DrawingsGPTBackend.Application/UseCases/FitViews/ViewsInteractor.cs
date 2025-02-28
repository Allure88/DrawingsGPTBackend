#nullable enable
using DrawingsGPTBackend.Domain.Bodies;
using DrawingsGPTBackend.Domain.Bodies.Views;

namespace DrawingsGPTBackend.Application.UseCases.FitViews
{

    public class ViewsInteractor(OrientationHandler orientationHandler, ScaleFormatHandler scaleHandler, ViewsSettler viewsSettler)
    {
        public ViewsResponce FitViews(BoundingBoxBody BoundingBox, DrawingsOptionsBody DrawingsOptions, bool IsAssembly)
        {

            double lengthModel = Math.Abs(BoundingBox.RightUp.X - BoundingBox.LeftBottom.X);
            double heightModel = Math.Abs(BoundingBox.RightUp.Y - BoundingBox.LeftBottom.Y);
            double widthModel = Math.Abs(BoundingBox.RightUp.Z - BoundingBox.LeftBottom.Z);

            var orientation = orientationHandler.GetBaseOrientation(lengthModel, heightModel, widthModel);

            var (scale, format) = scaleHandler.FitViews(lengthModel, heightModel, widthModel, orientation, DrawingsOptions.PriorityScale);

            (List<BaseViewBody> baseViews, List<ProjectViewBody> projectViews) = viewsSettler.PlaceViews(lengthModel, heightModel, widthModel, orientation, scale, format);

            return new() { BaseViews = baseViews, ProjectViews = projectViews, Format = format };
        }
    }


}
