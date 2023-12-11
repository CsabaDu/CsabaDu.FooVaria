namespace CsabaDu.FooVaria.Measurements.Factories;

public interface IMeasurementFactory : IFactory<IMeasurement>, IDefaultMeasurableFactory<IMeasurement>
{
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(string name);
    IMeasurement? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasurement? Create(Enum measureUnit, decimal exchangeRate, string customName);
}
