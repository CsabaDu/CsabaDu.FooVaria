using CsabaDu.FooVaria.Measurables.Statics;
using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    #region Static properties
    private static IDictionary<object, IMeasurement> Measurements => GetMeasurements();
    #endregion
    #endregion

    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = GetFirstMeasurement();
        measurement.SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);
        Enum measureUnit = measurement.GetMeasureUnit(customName)!;

        return Create(measureUnit);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = GetFirstMeasurement();

        if (!ExchangeRates.IsValidMeasureUnit(measureUnit))
        {
            measurement.SetCustomMeasureUnit(measureUnit, exchangeRate, customName);

            return Create(measureUnit);
        }

        measurement = Create(measureUnit);
        measurement.ValidateExchangeRate(exchangeRate);

        if (customName == measurement.GetCustomName()) return measurement;

        throw CustomNameArgumentOutOfRangeException(customName);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        if (ExchangeRates.IsValidMeasureUnit(measureUnit)) return Measurements[measureUnit];

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement Create(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return Create(measureUnit);
    }

    public IMeasurement Create(string name)
    {
        IMeasurement measurement = GetFirstMeasurement();
        Enum? measureUnit = measurement.GetMeasureUnit(name);

        if (measureUnit != null) return Create(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public IMeasurable Create(IMeasurable other)
    {
        Enum measureUnit = NullChecked(other, nameof(other)).GetMeasureUnit();

        return Create(measureUnit);
    }

    public IMeasurement CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return Create(measureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement GetFirstMeasurement()
    {
        return Measurements.First().Value;
    }

    private static Dictionary<object, IMeasurement> GetMeasurements()
    {
        return getMeasurements().ToDictionary(x => x.Key, x => x.Value);

        #region Local methods
        static IEnumerable<KeyValuePair<object, IMeasurement>> getMeasurements()
        {
            foreach (object item in ExchangeRates.GetValidMeasureUnits())
            {
                IMeasurement measurement = new Measurement(new MeasurementFactory(), (Enum)item);

                yield return new KeyValuePair<object, IMeasurement>(item, measurement);
            }
        }
        #endregion
    }
    #endregion
    #endregion
}
