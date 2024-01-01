namespace CsabaDu.FooVaria.Measurements.Factories;

public interface IMeasurementFactory : IBaseMeasurementFactory, IFactory<IMeasurement>, IDefaultMeasurableFactory<IMeasurement>
{
    IMeasurement Create(IBaseMeasurement baseMeasurement);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(string name);
    IMeasurement? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasurement? Create(Enum measureUnit, decimal exchangeRate, string customName);
}
