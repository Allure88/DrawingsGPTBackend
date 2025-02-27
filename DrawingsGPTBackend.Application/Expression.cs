using DrawingsGPTBackend.Application;

namespace DrawingsGPTBackend.Application;

public static class Expression
{
    public static bool EqualsWithTolerance(double value1, double value2, double Tolerance = 0.001)
    {
        return Math.Abs(value1 - value2) < Tolerance;
    }
    public static bool EqualsTo(this double value1, double value2, double Tolerance = 0.001)
    {
        return Math.Abs(value1 - value2) < Tolerance;
    }
    public static bool More(double value1, double value2, double Tolerance = 0.001)
    {
        return value1 - value2 > Tolerance;
    }
    public static bool Less(double value1, double value2, double Tolerance = 0.001)
    {
        return value1 - value2 < -Tolerance;
    }
    public static bool MoreOrEquals(double value1, double value2, double Tolerance = 0.001)
    {
        return value1 - value2 > Tolerance || Math.Abs(value1 - value2) < Tolerance;
    }
    public static bool LessOrEquals(double value1, double value2, double Tolerance = 0.001)
    {
        return value1 - value2 < Tolerance || Math.Abs(value1 - value2) < Tolerance;
    }
  
}
