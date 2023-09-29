using CsabaDu.FooVaria.Measurables.Statics;
using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    #region Static properties
    private static IDictionary<object, IMeasurement> MeasurementCollection
        => ExchangeRates.GetValidMeasureUnits().ToDictionary
        (
            x => x,
            x => new Measurement(new MeasurementFactory(), (Enum)x) as IMeasurement
        );
    #endregion
    #endregion

    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = GetFirstMeasurement();
        measurement.SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);
        Enum measureUnit = measurement.GetMeasureUnit(customName)!;

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = GetFirstMeasurement();

        if (!ExchangeRates.IsValidMeasureUnit(measureUnit))
        {
            measurement.SetCustomMeasureUnit(measureUnit, exchangeRate, customName);

            return GetStoredMeasurement(measureUnit);
        }

        measurement = GetStoredMeasurement(measureUnit);
        measurement.ValidateExchangeRate(exchangeRate);

        if (customName == measurement.GetCustomName()) return measurement;

        throw CustomNameArgumentOutOfRangeException(customName);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        if (ExchangeRates.IsValidMeasureUnit(measureUnit)) return GetStoredMeasurement(measureUnit);

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement Create(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement Create(string name)
    {
        IMeasurement measurement = GetFirstMeasurement();
        Enum? measureUnit = measurement.GetMeasureUnit(name);

        if (measureUnit != null) return GetStoredMeasurement(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public IMeasurable Create(IMeasurable other)
    {
        Enum measureUnit = NullChecked(other, nameof(other)).GetMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement GetFirstMeasurement()
    {
        return MeasurementCollection.First().Value;
    }

    private static IMeasurement GetStoredMeasurement(Enum measureUnit)
    {
        return MeasurementCollection[measureUnit];
    }
    #endregion
    #endregion
}
