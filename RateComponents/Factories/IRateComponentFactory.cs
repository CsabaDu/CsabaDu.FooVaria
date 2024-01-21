using CsabaDu.FooVaria.Common.Factories;
using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory, IDefaultMeasurableFactory
    {
        IMeasurementFactory MeasurementFactory { get; }
        object DefaultRateComponentQuantity { get; }
        TypeCode QuantityTypeCode { get; }
    }

    public interface IRateComponentFactory<T> : IRateComponentFactory, IFactory<T>
        where T : class, IBaseMeasure
    {
    }
    //public interface IRateComponentFactory<T, TNum> : IRateComponentFactory<T>, IBaseMeasureFactory<T>
    //    where T : class, IBaseMeasure/*, IDefaultBaseMeasure*/
    //    where TNum : struct
    //{
    //    T Create(IMeasurement measurement, TNum quantity);
    //}
}
