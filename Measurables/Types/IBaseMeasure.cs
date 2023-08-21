namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantifiable, IRateComponent<IBaseMeasure>, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>, IProportional<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; init; }
    decimal DefaultQuantity { get; }

    IBaseMeasure? GetBaseMeasure(ValueType quantity, string name);
    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
    IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null);
    IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    IBaseMeasure GetBaseMeasure(IBaseMeasure? baseMeasure = null);
    IBaseMeasure GetDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode? measureUnitTypeCode = null);

    IBaseMeasureFactory GetBaseMeasureFactory();
    decimal GetDecimalQuantity(IBaseMeasure? baseMeasure = null);
    LimitMode? GetLimitMode();
    RateComponentCode GetRateComponentCode(IBaseMeasure baseMeasure);
}
