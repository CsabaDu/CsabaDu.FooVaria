namespace CsabaDu.FooVaria.Measurements.Types;

public interface ICustomMeasurement : IMeasurement, ICustomMeasureUnit
{
    ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
    ICustomMeasurement? GetNextCustomMeasurement(string customName, MeasureUnitCode customMeasureUnitCode, decimal exchangeRate);
    ICustomMeasurement? GetNextCustomMeasurement(string customName, decimal exchangeRate);
}
