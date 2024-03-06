namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Factories;

public interface IBaseMeasurementFactory : IMeasurableFactory
{
    IBaseMeasurement? CreateBaseMeasurement(Enum context);
}
