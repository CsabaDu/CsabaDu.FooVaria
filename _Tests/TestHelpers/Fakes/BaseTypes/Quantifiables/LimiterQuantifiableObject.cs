namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Quantifiables;

public sealed class LimiterQuantifiableObject(IRootObject rootObject, string paramName) : QuantifiableChild(rootObject, paramName), ILimiter
{
    public static LimiterQuantifiableObject GetLimiterQuantifiableObject(LimitMode limitMode, Enum measureUnit, decimal defaultQuantity, IQuantifiableFactory factory = null)
    {
        DataFields fields = DataFields.Fields;

        return new(fields.RootObject, fields.paramName)
        {
            Return = GetReturn(measureUnit, defaultQuantity, factory),
            LimiterObject = new()
            {
                LimitMode = limitMode,
            },
        };
    }

    private LimiterObject LimiterObject { get; set; }

    public LimitMode? GetLimitMode() => LimiterObject.GetLimitMode();
}
