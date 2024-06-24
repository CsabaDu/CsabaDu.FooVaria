namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.BaseQuantifiables;

public sealed class LimiterBaseQuantifiableObject(IRootObject rootObject, string paramName) : BaseQuantifiableChild(rootObject, paramName), ILimiter
{
    public static LimiterBaseQuantifiableObject GetLimiterBaseQuantifiableObject(LimitMode? limitMode, Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
    {
        

        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitReturnValue = measureUnit,
                GetDefaultQuantityReturnValue = defaultQuantity,
                GetFactoryReturnValue = factory,
            },
            LimitMode = limitMode,
        };
    }

    private LimitMode? LimitMode { get; set; }

    public LimitMode? GetLimitMode() => LimitMode;
}
