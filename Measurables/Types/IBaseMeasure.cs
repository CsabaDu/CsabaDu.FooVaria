namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantifiable, IRateComponent<IBaseMeasure>, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>, IProportional<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; init; }
    decimal DefaultQuantity { get; }

    IBaseMeasure? GetBaseMeasure(ValueType quantity, string name);
    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    IBaseMeasure GetBaseMeasure(IBaseMeasure? baseMeasure = null);
    IBaseMeasure GetDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode? measureUnitTypeCode = null);
    bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
    IBaseMeasureFactory GetBaseMeasureFactory();
    LimitMode? GetLimitMode();
    RateComponentCode GetRateComponentCode(IBaseMeasure baseMeasure);
}

    //decimal GetDecimalQuantity(IBaseMeasure? baseMeasure = null);