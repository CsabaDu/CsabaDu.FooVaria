using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;
using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasurementFactory : BaseMeasurementFactory, IMeasurementFactory
{
    #region Properties
    #region Static properties
    private static IDictionary<object, IMeasurement> MeasurementCollection
        => BaseMeasurement.ExchangeRateCollection.Keys.ToDictionary
        (
            x => x,
            x => new Measurement(new MeasurementFactory(), (Enum)x) as IMeasurement
        );
    #endregion
    #endregion

    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = GetFirstStoredMeasurement();
        measurement.SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);
        Enum measureUnit = measurement.GetMeasureUnit(customName)!;

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = GetFirstStoredMeasurement();

        if (!IsValidMeasureUnit(measureUnit))
        {
            measurement.SetCustomMeasureUnit(measureUnit, exchangeRate, customName);

            return GetStoredMeasurement(measureUnit);
        }

        measurement = GetStoredMeasurement(measureUnit);
        measurement.ValidateExchangeRate(exchangeRate, nameof(exchangeRate));

        if (customName == measurement.GetCustomName()) return measurement;

        throw NameArgumentOutOfRangeException(customName, nameof(customName));
    }

    public IMeasurement Create(Enum measureUnit)
    {
        _ = NullChecked(measureUnit, nameof(measureUnit));

        if (IsValidMeasureUnit(measureUnit)) return GetStoredMeasurement(measureUnit);

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement Create(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement Create(string name)
    {
        IMeasurement measurement = GetFirstStoredMeasurement();
        Enum? measureUnit = measurement.GetMeasureUnit(name);

        if (measureUnit != null) return GetStoredMeasurement(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public override IMeasurement Create(IMeasurable other)
    {
        Enum measureUnit = NullChecked(other, nameof(other)) switch
        {
            Measurement measurement => measurement.GetMeasureUnit(),
            BaseMeasure baseMmeasure => getMeasureUnit(baseMmeasure),
            Rate rate => getMeasureUnit(rate.Denominator),

            _ => throw new InvalidOperationException(null),
        };

        return GetStoredMeasurement(measureUnit);

        #region Local methods
        static Enum getMeasureUnit(IBaseMeasure baseMmeasure)
        {
            return baseMmeasure.Measurement.GetMeasureUnit();
        }
        #endregion
    }

    public IMeasurement CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement GetFirstStoredMeasurement()
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
