namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure/*, Behaviors.IBaseMeasureQuantity, IQuantityTypeCode,IDecimalQuantity,*/
        /*, IRateComponentCode/*, IExchangeRate, IExchange<IRateComponent, Enum>, IRound<IRateComponent> */
    {
        IMeasurement Measurement { get; init; }
    }

    public interface IRateComponent<TSelf, TNum> : IRateComponent, IDefaultBaseMeasure<TSelf, TNum>, IDefaultMeasurable<TSelf>, IBaseMeasure<TSelf>
        where TSelf : class, IRateComponent, IDefaultBaseMeasure
        where TNum : struct
    {
        object GetDefaultRateComponentQuantity();
        TSelf GetRateComponent(IMeasurement measurement, TNum quantity);
    }
}
