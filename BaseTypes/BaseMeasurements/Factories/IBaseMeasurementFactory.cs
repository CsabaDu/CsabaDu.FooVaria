namespace CsabaDu.FooVaria.BaseMeasurements.Factories;

public interface IBaseMeasurementFactory : IMeasurableFactory
{
    IBaseMeasurement? CreateBaseMeasurement(object context);
}
