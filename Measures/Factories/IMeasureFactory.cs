namespace CsabaDu.FooVaria.Measures.Factories;

public interface IMeasureFactory : IBaseMeasureFactory<IMeasure>, IDefaultBaseMeasureFactory<IMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
}
