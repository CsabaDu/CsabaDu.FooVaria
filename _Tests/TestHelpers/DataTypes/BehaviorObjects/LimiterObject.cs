namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;

public sealed class LimiterObject : ILimiter
{
    public decimal DefaultQuantity { private get; set; }
    public MeasureUnitCode MeasureUnitCode { private get; set; }
    public LimitMode? LimitMode { private get; set; }

    public decimal GetLimiterDefaultQuantity() => DefaultQuantity;

    public MeasureUnitCode GetLimiterMeasureUnitCode() => MeasureUnitCode;

    public LimitMode? GetLimitMode() => LimitMode;
}
