namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantifiable, IRateComponent, IRateComponentType, IRateComponentType<IBaseMeasure>, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>, IProportional<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; init; }
    decimal DefaultQuantity { get; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);

    bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
    LimitMode? GetLimitMode();
}
