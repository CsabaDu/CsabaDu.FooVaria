namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class SampleParams
{
    public static readonly int MeasureUnitTypeCodeCount = Enum.GetNames(typeof(MeasureUnitTypeCode)).Length;
    public static readonly MeasureUnitTypeCode InvalidMeasureUnitTypeCode = (MeasureUnitTypeCode)MeasureUnitTypeCodeCount;

    public const LimitMode DefaultLimitMode = default;
}