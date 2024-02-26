namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class CustomMeasurement(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit), ICustomMeasurement
{
    #region Public methods
    public ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return (ICustomMeasurement?)GetFactory().Create(measureUnit, exchangeRate, customName);
    }

    public ICustomMeasurement? GetNextCustomMeasurement(string customName, MeasureUnitCode customMeasureUnitCode, decimal exchangeRate)
    {
        return (ICustomMeasurement?)GetFactory().Create(customName, customMeasureUnitCode, exchangeRate);
    }

    public ICustomMeasurement? GetNextCustomMeasurement(string customName, decimal exchangeRate)
    {
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode();

        return GetNextCustomMeasurement(customName, measureUnitCode, exchangeRate);
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        return GetNotUsedCustomMeasureUnits(GetMeasureUnitCode());
    }
    #endregion
}