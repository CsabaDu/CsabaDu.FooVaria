namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantifiable, IRateComponent<IBaseMeasure>, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>, IProportional<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; init; }
    decimal DefaultQuantity { get; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    IBaseMeasure GetBaseMeasure(IMeasurable other);
    //IBaseMeasure GetBaseMeasure(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure);

    bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
    LimitMode? GetLimitMode();
}

    //RateComponentCode GetRateComponentCode(IBaseMeasure baseMeasure);
    //IBaseMeasure? GetBaseMeasure(ValueType quantity, string name);
    //IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    //IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement measurement);