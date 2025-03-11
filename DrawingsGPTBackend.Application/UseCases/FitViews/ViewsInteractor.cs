using DrawingsGPTBackend.Domain.Bodies;
using DrawingsGPTBackend.Domain.Bodies.Views;

namespace DrawingsGPTBackend.Application.UseCases.FitViews
{

    public class ViewsInteractor(OrientationHandler orientationHandler, ScaleFormatHandler scaleHandler, ViewsSettler viewsSettler)
    {
        public ViewsResponce FitViews(BoundingBoxBody BoundingBox, DrawingsOptionsBody DrawingsOptions)
        {

            double lengthModel = Math.Abs(BoundingBox.RightUp.X - BoundingBox.LeftBottom.X);
            double heightModel = Math.Abs(BoundingBox.RightUp.Y - BoundingBox.LeftBottom.Y);
            double widthModel = Math.Abs(BoundingBox.RightUp.Z - BoundingBox.LeftBottom.Z);

            var orientation = orientationHandler.GetBaseOrientation(lengthModel,widthModel);

            var (scale, format) = scaleHandler.FitViews(lengthModel, heightModel, widthModel, orientation, DrawingsOptions.PriorityScale);

            List<ViewBody> views = viewsSettler.PlaceViews(lengthModel, heightModel, widthModel, orientation, scale, format);



            return new() { Views = views,  Format = format };
        }
    }


}
