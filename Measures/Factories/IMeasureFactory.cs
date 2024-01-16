namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IMeasureFactory : IBaseMeasureFactory<IMeasure>, IDefaultBaseMeasureFactory<IMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
}
