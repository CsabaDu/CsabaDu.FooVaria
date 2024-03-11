namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class SampleParams
{
    internal static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    internal static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;
    internal static readonly RootObject rootObject = new();

    internal static Enum GetNotDefinedMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        int count = Enum.GetNames(measureUnitType).Length;

        return (Enum)Enum.ToObject(measureUnitType, count);
    }

    //public const LimitMode DefaultLimitMode = default;
}
