namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure,/* Behaviors.IBaseMeasureQuantity, IQuantityTypeCode,IDecimalQuantity,*/ ILimitMode, IRateComponentCode/*, IExchangeRate*/, IExchange<IRateComponent, Enum>, IRound<IRateComponent>
    {
        IMeasurement Measurement { get; init; }
        object Quantity { get; init; }

        IRateComponent GetRateComponent(Enum measureUnit, ValueType quantity);
    }

    public interface IRateComponent<out TSelf> : IRateComponent
        where TSelf : class, IRateComponent
    {
        TSelf GetRateComponent(ValueType quantity);
        TSelf GetRateComponent(string name, ValueType quantity);
        TSelf GetRateComponent(IMeasurement measurement, ValueType quantity);
        TSelf? GetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        TSelf? GetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);

        public bool TryGetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out IRateComponent? rateComponent)
        {
            rateComponent = GetRateComponent(measureUnit, exchangeRate, quantity, customName);

            return rateComponent != null;
        }

        public bool TryGetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out IRateComponent? rateComponent)
        {
            rateComponent = GetRateComponent(customName, measureUnitTypeCode, exchangeRate, quantity);

            return rateComponent != null;
        }
    }

    public interface IRateComponent<TSelf, TNum> : IRateComponent<TSelf>, IDefaultRateComponent<TSelf, TNum>
        where TSelf : class, IRateComponent, IDefaultRateComponent
        where TNum : struct
    {
        TSelf GetRateComponent(TNum quantity);
    }
}
