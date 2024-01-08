namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IBaseMeasurementFactory : IMeasurableFactory
{
    IBaseMeasurement? CreateBaseMeasurement(object context);
}
