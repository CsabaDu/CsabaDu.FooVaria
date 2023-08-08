namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantifiable, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>
{
    IMeasurement Measurement { get; init; }
    object Quantity { get; init; }
    RateComponentCode RateComponentCode { get; init; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);
    IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
    decimal GetDefaultQuantity(IBaseMeasure? baseMeasure = null);
    LimitMode? GetLimitMode();
}
