using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingsGPTBackend.Domain
{
    public enum Format
    {
        A4 = 297,
        A3 = 420,
        A2 = 594,
        A1 = 841,
        A0 = 1189
    }

 

    public static class EnumParser
    {
        public static (int length, int height) GetSheetDimensions(this Format format)
        {
            return format switch
            {
                Format.A4 => (210, 297),
                Format.A3 => (420, 297),
                Format.A2 => (594, 420),
                Format.A1 => (841, 594),
                Format.A0 => (1189, 841),
                _ => throw new NotImplementedException(),
            };
        }
    }

}
