namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IRateComponent : IMeasurable, IQuantity, IQuantityType, IDecimalQuantity, ILimitMode, IRateComponentCode, IExchangeRate, IBaseMeasureTemp, IExchange<IRateComponent, Enum>, IRound<IRateComponent>
    {
        IMeasurement Measurement { get; }
        object Quantity { get; init; }
        TypeCode QuantityTypeCode { get; }

        bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IRateComponent? baseMeasure);
        IRateComponent GetBaseMeasure(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    }

    public interface IBaseMeasure<T> : IRateComponent where T : class, IRateComponent
    {
        T GetBaseMeasure(ValueType quantity);
        T GetBaseMeasure(string name, ValueType quantity);
        T GetBaseMeasure(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
        T GetBaseMeasure(Enum measureUnit, ValueType quantity);
        T GetBaseMeasure(IMeasurement measurement, ValueType quantity);
        T GetBaseMeasure(IRateComponent baseMeasure);
        T GetBaseMeasure(T other);
    }
}

