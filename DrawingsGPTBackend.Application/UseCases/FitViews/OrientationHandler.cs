using DrawingsGPTBackend.Domain.Bodies;

namespace DrawingsGPTBackend.Application.UseCases.FitViews
{
    public class OrientationHandler
    {
        public ViewOrientationTypeEnumBody GetBaseOrientation(double lengthModel,double heightModel, double widthModel)
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
