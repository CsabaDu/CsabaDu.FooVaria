namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure,/* Behaviors.IBaseMeasureQuantity, IQuantityTypeCode,IDecimalQuantity,*/ ILimitMode
        /*, IRateComponentCode/*, IExchangeRate, IExchange<IRateComponent, Enum>, IRound<IRateComponent> */
    {
        //IMeasurement Measurement { get; init; }
        //object Quantity { get; init; }

        //IRateComponent GetRateComponent(Enum measureUnit, ValueType quantity);
        //IRateComponent GetRateComponent(ValueType quantity);
        //IRateComponent GetRateComponent(string name, ValueType quantity);
        //IRateComponent? GetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        //IRateComponent? GetRateComponent(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);

        //public bool TryGetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out IRateComponent? rateComponent)
        //{
        //    rateComponent = GetRateComponent(measureUnit, exchangeRate, quantity, customName);

        //    return rateComponent != null;
        //}

        //public bool TryGetRateComponent(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out IRateComponent? rateComponent)
        //{
        //    rateComponent = GetRateComponent(customName, measureUnitCode, exchangeRate, quantity);

        //    return rateComponent != null;
        //}
        IRateComponent GetRateComponent(IMeasurement measurement, ValueType quantity);
    }

    //public interface IRateComponent<TSelf, TNum> : IRateComponent<TSelf>, IDefaultRateComponent<TSelf, TNum>
    //    where TSelf : class, IRateComponent, IDefaultRateComponent
    //    where TNum : struct
    //{
    //    TSelf GetRateComponent(TNum quantity);
    //}
}
