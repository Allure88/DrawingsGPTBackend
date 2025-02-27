using DrawingsGPTBackend.Domain;
using DrawingsGPTBackend.Domain.Bodies;

namespace DrawingsGPTBackend.Application.UseCases.FitViews
{

    public class ScaleWr(double value)
    {
        public double Value { get; } = value;
    }

    public class ScaleFormatHandler
    {
        public readonly List<ScaleWr> scales = [
            new(0.01),
            new(1 / 75d),
            new(0.02),
            new(0.025),
            new(0.03333333),
            new(0.04),
            new(0.05),
            new(0.1),
            new(0.2),
            new(0.25),
            new(0.5),
            new(1d),
            new(2d),
            new(4d),
            new(5d)
            ];

        public const double SHEET_FILLING = 0.7;

        internal (double scale, Format format) FitViews(double lengthModel, double heightModel, double widthModel, ViewOrientationTypeEnumBody orientation, double priorityScale)
        {
            Format formatFitted = Format.A4;
            double scaleFitted = priorityScale;

            double netWidth = lengthModel + widthModel;
            double netHeight = orientation == ViewOrientationTypeEnumBody.kFrontViewOrientation
                ? heightModel + widthModel
                : heightModel + lengthModel;

            double normalWidth = netWidth / SHEET_FILLING;
            double normalHeight = netHeight / SHEET_FILLING;

            //идем от А4 вверх
            foreach (Format format in Enum.GetValues<Format>())
            {
                (int sheetLength, int sheetHeight) = format.GetSheetDimensions();

                //если помещается
                if (sheetLength > normalWidth * scaleFitted && sheetHeight > normalHeight * scaleFitted)
                {
                    ScaleWr scaleWrFitted = scales.First(s => s.Value.EqualsTo(scaleFitted));
                    int index = scales.IndexOf(scaleWrFitted);

                    if (index != scales.Count - 1)
                    {
                        //пытаемся увеличить масштаб
                        for (int i = index + 1; i < scales.Count; i++)
                        {
                            if (sheetLength > normalWidth * scales[i].Value && sheetHeight > normalHeight * scales[i].Value)
                            {
                                scaleFitted = scales[i].Value;
                            }
                            else
                                break;
                        }
                    }

                    formatFitted = format;
                    break;
                }
                else if (format == Format.A0)
                {
                    ScaleWr scaleWrFitted = scales.First(s => s.Value.EqualsTo(scaleFitted));
                    int index = scales.IndexOf(scaleWrFitted);

                    //пытаемся уменьшить масштаб
                    if (index != 0)
                    {
                        for (int i = index - 1; i >= 0; i--)
                        {
                            scaleFitted = scales[i].Value;
                            if (sheetLength > normalWidth * scales[i].Value && sheetHeight > normalHeight * scales[i].Value)
                            {
                                break;
                            }
                        }
                    }
                }
                formatFitted = format;
            }


            return (scaleFitted, formatFitted);

        }
    }


}
