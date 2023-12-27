namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseMeasurementFactory : IMeasurableFactory
{
    IBaseMeasurement? CreateBaseMeasurement(object context);
}
