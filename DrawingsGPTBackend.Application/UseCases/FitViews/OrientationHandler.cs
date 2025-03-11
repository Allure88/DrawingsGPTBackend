using DrawingsGPTBackend.Domain.Bodies.Views;

namespace DrawingsGPTBackend.Application.UseCases.FitViews
{
    public class OrientationHandler
    {
        public ViewOrientationTypeEnumBody GetBaseOrientation(double lengthModel,double widthModel)
        {
            ViewOrientationTypeEnumBody orientation;
            if (lengthModel > widthModel)
                orientation = ViewOrientationTypeEnumBody.kFrontViewOrientation;
            else
                orientation = ViewOrientationTypeEnumBody.kLeftViewOrientation;
            return orientation;
        }
    }

}
