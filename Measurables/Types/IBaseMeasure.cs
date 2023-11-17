namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IBaseMeasure : IMeasurable, IQuantity, IQuantityType, IDecimalQuantity, ILimitMode, IRateComponentCode, IExchangeRate, IRateComponent, IBaseMeasureTemp, IRateComponent<IBaseMeasure>, IExchange<IBaseMeasure, Enum>, IRound<IBaseMeasure>
    {
        IMeasurement Measurement { get; }
        object Quantity { get; init; }
        TypeCode QuantityTypeCode { get; }

        bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);
        IBaseMeasure GetBaseMeasure(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    }

    public interface IBaseMeasure<T> : IBaseMeasure where T : class, IBaseMeasure
    {
        T GetBaseMeasure(ValueType quantity);
        T GetBaseMeasure(string name, ValueType quantity);
        T GetBaseMeasure(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
        T GetBaseMeasure(Enum measureUnit, ValueType quantity);
        T GetBaseMeasure(IMeasurement measurement, ValueType quantity);
        T GetBaseMeasure(IBaseMeasure baseMeasure);
        T GetBaseMeasure(T other);
    }
}

