using CsabaDu.FooVaria.Measurables.Types;

namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure,/* Behaviors.IBaseMeasureQuantity, IQuantityTypeCode,IDecimalQuantity,*/
        /*, IRateComponentCode/*, IExchangeRate, IExchange<IRateComponent, Enum>, IRound<IRateComponent> */
    {
        IMeasurement Measurement { get; init; }

        //object Quantity { get; init; }

        //IRateComponent GetBaseMeasure(Enum measureUnit, ValueType quantity);
        //IRateComponent GetBaseMeasure(ValueType quantity);
        //IRateComponent GetBaseMeasure(string name, ValueType quantity);
        //IRateComponent? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        //IRateComponent? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);

        //public bool TryGetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out IRateComponent? rateComponent)
        //{
        //    rateComponent = GetBaseMeasure(measureUnit, exchangeRate, quantity, customName);

        //    return rateComponent != null;
        //}

        //public bool TryGetRateComponent(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out IRateComponent? rateComponent)
        //{
        //    rateComponent = GetBaseMeasure(customName, measureUnitCode, exchangeRate, quantity);

        //    return rateComponent != null;
        //}
        IRateComponent GetRateComponent(IMeasurement measurement, ValueType quantity);
    }

    public interface IRateComponent<TSelf, TNum> : IRateComponent, IDefaultBaseMeasure<TSelf, TNum>, IDefaultMeasurable<TSelf>
        where TSelf : class, IRateComponent, IDefaultBaseMeasure
        where TNum : struct
    {
        TNum GetDefaultRateComponentQuantity();
    }
}
