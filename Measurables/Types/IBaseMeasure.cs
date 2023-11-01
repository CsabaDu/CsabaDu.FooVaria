namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasure : IMeasurable, IQuantity, IQuantityType, IDecimalQuantity, ILimitMode, IRateComponent, IExchangeRate, IRateComponentType, IRateComponentType<IBaseMeasure>, IExchange<IBaseMeasure, Enum>, IRound<IBaseMeasure>
{
    IMeasurement Measurement { get; }
    object Quantity { get; init; }
    TypeCode QuantityTypeCode { get; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
}
