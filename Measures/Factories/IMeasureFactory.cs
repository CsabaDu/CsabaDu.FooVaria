namespace CsabaDu.FooVaria.Measures.Factories;

public interface IMeasureFactory : IBaseMeasureFactory<IMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
}
