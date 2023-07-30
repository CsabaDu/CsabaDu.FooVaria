namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>
{
    IMeasurement Measurement { get; init; }
    object Quantity { get; init; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);
    IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
    RateComponentCode GetRateComponentCode();
    decimal GetDefaultQuantity(IBaseMeasure? baseMeasure = null);
    LimitMode? GetLimitMode();
}
