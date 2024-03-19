namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public static class SampleParams
{
    private static readonly int LimitModeCount = Enum.GetNames<LimitMode>().Length;

    public const LimitMode DefaultLimitMode = default;
    public static readonly LimitMode NotDefinedLimitMode = (LimitMode)LimitModeCount;
    public static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;

    public static Enum GetNotDefinedMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        int count = Enum.GetNames(measureUnitType).Length;

        return (Enum)Enum.ToObject(measureUnitType, count);
    }

    public static TypeCode[] InvalidValueTypeCodes =>
        [
            TypeCode.Boolean,
            TypeCode.Char,
            TypeCode.SByte,
            TypeCode.Byte,
            TypeCode.Int16,
            TypeCode.UInt16,
            TypeCode.Single,
            TypeCode.DateTime,
        ];
}
