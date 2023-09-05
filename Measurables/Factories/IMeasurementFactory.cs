namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasurementFactory : IMeasurableFactory, IRateComponentFactory<IMeasurement>
{
    IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(IMeasurement measurement);

    //Enum GetOrCreateValidMeasureUnit(Enum measureUnit, decimal? exchangeRate, string? customName);
}
