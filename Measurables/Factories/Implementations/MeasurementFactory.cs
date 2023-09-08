namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasurementFactory : MeasurableFactory, IMeasurementFactory
{
    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return CreateCustomMeasurement(customName, measureUnitTypeCode, exchangeRate);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetOrCreateMeasurement(measureUnit, exchangeRate, customName);
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
        return GetMeasurement(name);
    }

    //public RateComponentCode GetValidRateComponentCode(RateComponentCode rateComponentCode)
    //{
    //    if (Enum.IsDefined(rateComponentCode)) return rateComponentCode;

    //    throw new InvalidEnumArgumentException(nameof(rateComponentCode), (int)rateComponentCode, rateComponentCode.GetType());
    //}
    #endregion

    #region Private methods
    private static IMeasurement GetOrCreateMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
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

    private static IMeasurement CreateCustomMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = GetFirstMeasurement();

        measurement.SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);

        Enum measureUnit = measurement.GetMeasureUnit(customName)!;

        return GetMeasurement(measureUnit);
    }

    private static IMeasurement GetMeasurement(string name)
    {
        IMeasurement measurement = GetFirstMeasurement();

        Enum? measureUnit = measurement.GetMeasureUnit(name);

        if (measureUnit != null) return GetMeasurement(measureUnit);

        throw new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    private static IMeasurement GetFirstMeasurement()
    {
        return Measurements.First().Value;
    }
    #endregion
}
