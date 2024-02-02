namespace CsabaDu.FooVaria.Measurements.Types;

public interface ICustomMeasurement : IMeasurement, ICustomMeasureUnitCode, ICustomMeasureUnit, ICustomMExchangeRates
{
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);
}

