namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests.Fakes;

internal sealed class LimiterBaseQuantifiableOblect(IRootObject rootObject, string paramName) : BaseQuantifiableChild(rootObject, paramName), ILimiter
{
    public LimitMode? LimitMode { private get; set; }

    public decimal GetLimiterDefaultQuantity() => GetDefaultQuantity();

    public MeasureUnitCode GetLimiterMeasureUnitCode() => GetMeasureUnitCode();

    public LimitMode? GetLimitMode() => LimitMode;
}
