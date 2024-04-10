namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;

internal sealed class LimiterBaseMeasureObject(IRootObject rootObject, string paramName) : BaseMeasureChild(rootObject, paramName), ILimiter
{
    internal static LimiterBaseMeasureObject GetLimiterBaseMeasureObject(Enum measureUnit, ValueType quantity, LimitMode limitMode, RateComponentCode? rateComponentCode = null)
    {
        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnit);

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = baseMeasurement,
                GetBaseQuantity = quantity,
                GetFactory = rateComponentCode.HasValue ?
                    GetBaseMeasureFactoryObject(rateComponentCode.Value)
                    : null,
                GetLimitMode = limitMode,
            }
        };
    }

    public LimitMode? LimitMode { private get; set; }

    public decimal GetLimiterDefaultQuantity() => GetDefaultQuantity();

    public MeasureUnitCode GetLimiterMeasureUnitCode() => GetMeasureUnitCode();
}
