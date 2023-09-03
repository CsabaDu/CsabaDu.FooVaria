using System.ComponentModel;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasurementFactory : MeasurableFactory, IMeasurementFactory
{
    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return CreateMeasurement(customName, measureUnitTypeCode, exchangeRate);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetOrCreateMeasurement(measureUnit, exchangeRate, customName);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        return GetMeasurement(measureUnit);
    }

    public IMeasurement Create(IMeasurement other)
    {
        return GetMeasurement(other);
    }

    public RateComponentCode GetValidRateComponentCode(RateComponentCode rateComponentCode)
    {
        if (Enum.IsDefined(rateComponentCode)) return rateComponentCode;

        throw new InvalidEnumArgumentException(nameof(rateComponentCode), (int)rateComponentCode, rateComponentCode.GetType());
    }
    #endregion

    #region Private methods
    private static IMeasurement GetOrCreateMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = GetMeasurement();

        if (!Measurements.ContainsKey(measureUnit))
        {
            measurement.SetCustomMeasureUnit(measureUnit, exchangeRate, customName);

            return getMeasurement();
        }

        measurement = getMeasurement();
        measurement.ValidateExchangeRate(exchangeRate, measureUnit);

        if (customName == measurement.GetCustomName()) return measurement;

        throw CustomNameArgumentOutOfRangeException(customName);

        #region Local methods
        IMeasurement getMeasurement() => Measurements[measureUnit];
        #endregion
    }

    private static IMeasurement CreateMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = GetMeasurement();

        measurement.SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);

        Enum measureUnit = measurement.GetMeasureUnit(customName)!;

        return GetMeasurement(measureUnit);
    }

    private static IMeasurement GetMeasurement()
    {
        return Measurements.First().Value;
    }
    #endregion
}
