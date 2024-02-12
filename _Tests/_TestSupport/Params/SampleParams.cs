namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class SampleParams
{
    public static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;

    public static Enum GetNotDefinedMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitCode);
        int count = Enum.GetNames(measureUnitType).Length;

        return (Enum)Enum.ToObject(measureUnitType, count);
    }

    //public const LimitMode DefaultLimitMode = default;
}