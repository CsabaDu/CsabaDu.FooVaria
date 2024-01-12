namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IMeasureFactory : IBaseMeasureFactory, IDefaultBaseMeasureFactory<IMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
}
