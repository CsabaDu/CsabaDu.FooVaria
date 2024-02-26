using System.Security.AccessControl;

namespace CsabaDu.FooVaria.Measurements.Types;

public interface ICustomMeasurement : IMeasurement, ICustomMeasureUnit
{
    ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName);

    ICustomMeasurement? GetCustomMeasurement(string customName, MeasureUnitCode customMeasureUnitCode, decimal exchangeRate);

    ICustomMeasurement? GetCustomMeasurement(string customName, decimal exchangeRate);

}

