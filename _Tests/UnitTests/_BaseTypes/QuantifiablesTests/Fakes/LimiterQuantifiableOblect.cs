namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal sealed class LimiterQuantifiableOblect(IRootObject rootObject, string paramName) : QuantifiableChild(rootObject, paramName), ILimiter
{
    public LimitMode? LimitMode { private get; set; }

    public decimal GetLimiterDefaultQuantity() => GetDefaultQuantity();

    public MeasureUnitCode GetLimiterMeasureUnitCode() => GetMeasureUnitCode();

    public LimitMode? GetLimitMode() => LimitMode;
}
