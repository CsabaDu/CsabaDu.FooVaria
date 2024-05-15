namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseQuantifiables;

public sealed class LimiterBaseQuantifiableObject(IRootObject rootObject, string paramName) : BaseQuantifiableChild(rootObject, paramName), ILimiter
{
    public static LimiterBaseQuantifiableObject GetLimiterBaseQuantifiableObject(LimitMode? limitMode, Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
    {
        

        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetDefaultQuantityValue = defaultQuantity,
                GetFactoryValue = factory,
            },
            LimitMode = limitMode,
        };
    }

    private LimitMode? LimitMode { get; set; }

    public LimitMode? GetLimitMode() => LimitMode;
}
