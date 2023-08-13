namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantifiable, IRateComponent, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>, IProportional<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; init; }
    RateComponentCode RateComponentCode { get; init; }
    decimal DefaultQuantity { get; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null, string? customName = null);
    IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null);
    IBaseMeasure GetBaseMeasure(ValueType quantity, string name);
    IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
    decimal GetDecimalQuantity(IBaseMeasure? baseMeasure = null);
    LimitMode? GetLimitMode();
}
