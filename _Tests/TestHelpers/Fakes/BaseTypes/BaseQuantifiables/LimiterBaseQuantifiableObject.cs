namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseQuantifiables;

public sealed class LimiterBaseQuantifiableObject(IRootObject rootObject, string paramName) : BaseQuantifiableChild(rootObject, paramName), ILimiter
{
    public static LimiterBaseQuantifiableObject GetLimiterBaseQuantifiableObject(LimitMode? limitMode, Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
    {
        DataFields fields = DataFields.Fields;

        return new(fields.RootObject, fields.paramName)
        {
            LimitMode = limitMode,
            Return = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetDefaultQuantityValue = defaultQuantity,
                GetFactoryValue = factory,
            }
        };
    }
    public LimitMode? LimitMode { private get; set; }

    public decimal GetLimiterDefaultQuantity() => GetDefaultQuantity();

    public MeasureUnitCode GetLimiterMeasureUnitCode() => GetMeasureUnitCode();

    public LimitMode? GetLimitMode() => LimitMode;
}
