namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseRates;

public sealed class LimiterBaseRateObject(IRootObject rootObject, string paramName) : BaseRateChild(rootObject, paramName), ILimiter
{
    public static LimiterBaseRateObject GetLimiterBaseQuantifiableObject(LimitMode? limitMode, Enum measureUnit, decimal defaultQuantity, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            LimitMode = limitMode,
            Return = GetReturn(measureUnit, defaultQuantity, factory),
            DenominatorCode = denominatorCode,
        };
    }

    private LimitMode? LimitMode { get; set; }

    public override LimitMode? GetLimitMode() => LimitMode;
}
