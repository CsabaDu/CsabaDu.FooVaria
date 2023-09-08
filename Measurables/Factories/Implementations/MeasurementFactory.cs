namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasurementFactory : MeasurableFactory, IMeasurementFactory
{
    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = GetFirstMeasurement();

        measurement.SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);

        Enum measureUnit = measurement.GetMeasureUnit(customName)!;

        return GetMeasurement(measureUnit);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = GetFirstMeasurement();

        if (!Measurements.ContainsKey(measureUnit))
        {
            measurement.SetCustomMeasureUnit(measureUnit, exchangeRate, customName);

            return GetMeasurement(measureUnit);
        }

        measurement = GetMeasurement(measureUnit);
        measurement.ValidateExchangeRate(exchangeRate, measureUnit);

        if (customName == measurement.GetCustomName()) return measurement;

        throw CustomNameArgumentOutOfRangeException(customName);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        return GetMeasurement(measureUnit);
    }

    public IMeasurement Create(IMeasurement measurement)
    {
        return GetMeasurement(measurement);
    }

    public IMeasurement Create(string name)
    {
        IMeasurement measurement = GetFirstMeasurement();

        Enum? measureUnit = measurement.GetMeasureUnit(name);

        if (measureUnit != null) return GetMeasurement(measureUnit);

        throw new ArgumentOutOfRangeException(nameof(name), name, null);
    }
    #endregion

    #region Private methods
    private static IMeasurement GetFirstMeasurement()
    {
        return Measurements.First().Value;
    }
    #endregion
}
