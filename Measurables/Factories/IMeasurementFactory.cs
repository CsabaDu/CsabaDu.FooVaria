namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasurementFactory : IMeasurableFactory, IRateComponentFactory
{
    IMeasurement Create(MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName);
    IMeasurement Create(Enum measureUnit, decimal exchangeRate, string? customName);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(IMeasurement measurement);

    Enum GetOrCreateValidMeasureUnit(Enum measureUnit, decimal? exchangeRate, string? customName);
}
