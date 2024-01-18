namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory/*<IBaseMeasure, Enum>*//*, IFactory<IBaseMeasure>*/
    {
        object DefaultRateComponentQuantity { get; }
        IMeasurementFactory MeasurementFactory { get; }
    }

    public interface IRateComponentFactory<T, TNum> : IRateComponentFactory, IBaseMeasureFactory<T>, IDefaultBaseMeasureFactory<T>
        where T : class, IBaseMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        T Create(IMeasurement measurement, TNum quantity);
    }
}
