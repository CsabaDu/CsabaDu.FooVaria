namespace CsabaDu.FooVaria.Measurements.Factories;

public interface IMeasurementFactory : IBaseMeasurementFactory, IFactory<IMeasurement>, IBaseMeasurableFactory<IMeasurement>
{
    IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(string name);
}
