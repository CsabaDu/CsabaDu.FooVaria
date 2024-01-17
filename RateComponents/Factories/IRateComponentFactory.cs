namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory/*<IRateComponent, Enum>*//*, IFactory<IRateComponent>*/
    {
        object DefaultRateComponentQuantity { get; }
        IMeasurementFactory MeasurementFactory { get; }
    }

    public interface IRateComponentFactory<T, TNum> : IRateComponentFactory, IBaseMeasureFactory<T>, IDefaultBaseMeasureFactory<T>
        where T : class, IRateComponent, IDefaultBaseMeasure
        where TNum : struct
    {
        T Create(IMeasurement measurement, TNum quantity);
    }
}
