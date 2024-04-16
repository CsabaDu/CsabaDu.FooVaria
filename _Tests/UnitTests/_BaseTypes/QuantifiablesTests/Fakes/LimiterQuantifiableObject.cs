namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal sealed class LimiterQuantifiableObject(IRootObject rootObject, string paramName) : QuantifiableChild(rootObject, paramName), ILimiter
{
    internal static LimiterQuantifiableObject GetLimiterQuantifiableObject(LimitMode limitMode, Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = defaultQuantity,
                GetFactory = factory,
            },
            LimiterObject = new()
            {
                LimitMode = limitMode,
            },
        };
    }

    private LimiterObject LimiterObject { get; set; }

    public LimitMode? GetLimitMode() => LimiterObject.GetLimitMode();
}
