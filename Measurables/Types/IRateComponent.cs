namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure, IQuantity, IQuantityType, IQuantityTypeCode, IDecimalQuantity, ILimitMode, IRateComponentCode, IExchangeRate, IExchange<IRateComponent, Enum>, IRound<IRateComponent>
    {
        IMeasurement Measurement { get; }
        object Quantity { get; init; }
        TypeCode QuantityTypeCode { get; }

        bool TryGetRateComponent(Enum measureUnit, ValueType quantity, decimal exchangeRate, string customName, [NotNullWhen(true)] out IRateComponent? baseMeasure);
        IRateComponent GetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    }

    public interface IRateComponent<out T> : IRateComponent where T : class, IRateComponent
    {
        T GetRateComponent(ValueType quantity);
        T GetRateComponent(string name, ValueType quantity);
        T GetRateComponent(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
        T GetRateComponent(Enum measureUnit, ValueType quantity);
        T GetRateComponent(IMeasurement measurement, ValueType quantity);
        T GetRateComponent(IRateComponent rateComponent);
    }
}

