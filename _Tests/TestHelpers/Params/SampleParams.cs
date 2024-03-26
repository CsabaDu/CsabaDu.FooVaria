using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public static class SampleParams
{
    private static readonly int LimitModeCount = Enum.GetNames<LimitMode>().Length;
    private static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    private static readonly int RoundingModeCount = Enum.GetNames<RoundingMode>().Length;
    private static readonly int TypeCodeCount = Enum.GetNames<TypeCode>().Length;

    public static readonly LimitMode NotDefinedLimitMode = (LimitMode)LimitModeCount;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;
    public static readonly RoundingMode NotDefinedRoundingMode = (RoundingMode)RoundingModeCount;
    public static readonly TypeCode NotDefinedTypeCode = (TypeCode)TypeCodeCount;

    public const LimitMode DefaultLimitMode = default;

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
            TypeCode.Int32,
            //TypeCode.UInt32, // TODO Check why?
            TypeCode.Single,
            TypeCode.DateTime,
        ];

    public static IEnumerable<TypeCode> GetInvalidQuantityTypeCodes()
    {
        return Enum.GetValues<TypeCode>().Except(QuantityTypeCodes);
    }
}
