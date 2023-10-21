namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IRateComponent, IRateComponentType, IRateComponentType<IBaseMeasure>, IQuantity<IBaseMeasure>, IExchangeRate<IBaseMeasure>, IExchange<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
    LimitMode? GetLimitMode();
}
