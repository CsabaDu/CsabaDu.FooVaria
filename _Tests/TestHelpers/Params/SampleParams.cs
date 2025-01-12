namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public static class SampleParams
{
    public static readonly IEnumerable<MeasureUnitCode> CustomMeasureUnitCodes = MeasureUnitCodes.Where(x => x.IsCustomMeasureUnitCode());
    public static readonly IEnumerable<MeasureUnitCode> ConstantMeasureUnitCodes = MeasureUnitCodes.Where(x => !x.IsCustomMeasureUnitCode());
    public static readonly IEnumerable<MeasureUnitCode> SpreadMeasureUnitCodes = MeasureUnitCodes.Where(x => x.IsSpreadMeasureUnitCode());

    public static readonly int LimitModeCount = Enum.GetNames<LimitMode>().Length;
    public static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    public static readonly int RoundingModeCount = Enum.GetNames<RoundingMode>().Length;
    public static readonly int TypeCodeCount = Enum.GetNames<TypeCode>().Length;
    public static readonly int RateComponentCodeCount = Enum.GetNames<RateComponentCode>().Length;

    public static readonly LimitMode NotDefinedLimitMode = (LimitMode)LimitModeCount;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;
    public static readonly RoundingMode NotDefinedRoundingMode = (RoundingMode)RoundingModeCount;
    public static readonly TypeCode NotDefinedTypeCode = (TypeCode)TypeCodeCount;
    public static readonly RateComponentCode NotDefinedRateComponentCode = (RateComponentCode)RateComponentCodeCount;

    public const LimitMode DefaultLimitMode = default;

    public static TEnum GetNotDefinedEnum<TEnum>() where TEnum : struct, Enum
    => (TEnum)(object)Enum.GetNames<TEnum>().Length;

    public static object GetNotDefinedEnum(Type enumType)
    => Enum.GetNames(enumType).Length;

    public static Enum GetNotDefinedMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        int index = Enum.GetNames(measureUnitType).Length;

        return (Enum)Enum.ToObject(measureUnitType, index);
    }

    public static TypeCode[] InvalidValueTypeCodes
    => [
            TypeCode.Boolean,
            TypeCode.Char,
            TypeCode.SByte,
            TypeCode.Byte,
            TypeCode.Int16,
            TypeCode.UInt16,
            TypeCode.Int32,
            //TypeCode.UInt32, // TODO Check why wrong?
            TypeCode.Single,
            TypeCode.DateTime,
        ];

    public static IEnumerable<TypeCode> GetInvalidQuantityTypeCodes()
    => Enum.GetValues<TypeCode>().Except(QuantityTypeCodes);

    public static MeasureUnitCode GetOtherSpreadMeasureUnitCode(MeasureUnitCode measureUnitCode)
    => SpreadMeasureUnitCodes.First(x => x != measureUnitCode);
}
