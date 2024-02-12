namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class SampleParams
{
    public static readonly int MeasureUnitCodeCount = Measurable.MeasureUnitCodes.Length;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;

    //public const LimitMode DefaultLimitMode = default;
}