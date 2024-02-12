namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class SampleParams
{
    public static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;

    public static Enum GetNotDefinedMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        int count = GetDefaultNames(measureUnitCode).Count();
        Type measureUnitType = GetMeasureUnitType(measureUnitCode);

        return (Enum)Enum.ToObject(measureUnitType, count);
    }

    //public const LimitMode DefaultLimitMode = default;
}