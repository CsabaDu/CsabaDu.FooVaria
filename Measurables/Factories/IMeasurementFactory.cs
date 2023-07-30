namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasurementFactory : IMeasurableFactory, IRateComponentFactory
{
    IMeasurement Create(Enum measureUnit, decimal exchangeRate);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(IMeasurement measurement);

    Enum GetOrCreateValidMeasureUnit(Enum measureUnit, decimal? exchangeRate);
}
