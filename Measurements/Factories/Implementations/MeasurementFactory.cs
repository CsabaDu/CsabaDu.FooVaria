using CsabaDu.FooVaria.Measurements.Types.Implementations;

namespace CsabaDu.FooVaria.Measurements.Factories.Implementations;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    #region Static properties
    private static IDictionary<object, IMeasurement> MeasurementCollection
        => MeasurementBase.ExchangeRateCollection.Keys.ToDictionary
        (
            x => x,
            x => CreateMeasurement((Enum)x)
        );
    #endregion
    #endregion

    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = CreateDefault(measureUnitTypeCode);

        if (measurement.TryGetMeasureUnit(measureUnitTypeCode, exchangeRate, out Enum? measureUnit)
            && measurement.GetCustomName(measureUnit) == customName)
        {
            return GetStoredMeasurement(measureUnit);
        }

        if (measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate))
        {
            measureUnit = measurement.GetMeasureUnit(customName)!;

            return GetStoredMeasurement(measureUnit);
        }

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (MeasurementBase.ExchangeRateCollection.TryGetValue(measureUnit, out decimal storedExchangeRate)
            && storedExchangeRate == exchangeRate
            && Measurement.CustomNameCollection.TryGetValue(measureUnit, out string? storedCustomName)
            && storedCustomName == customName)
        {
            return GetStoredMeasurement(measureUnit);
        }

        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
        IMeasurement measurement = CreateDefault(measureUnitTypeCode);

        if (measurement is ICustomMeasurement customMeasurement 
            && customMeasurement.TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName))
        {
            return GetStoredMeasurement(measureUnit);
        }

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
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
        IMeasurement measurement = MeasurementCollection.First().Value; ;
        Enum? measureUnit = measurement.GetMeasureUnit(name);

        if (measureUnit != null) return GetStoredMeasurement(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public IMeasurement CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement CreateMeasurement(Enum measureUnit)
    {
        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
        MeasurementFactory factory = new();

        return measureUnitTypeCode.IsCustomMeasureUnitTypeCode() ?
            new CustomMeasurement(factory, measureUnit)
            : new ConstantMeasurement(factory, measureUnit);
    }

    private static IMeasurement GetStoredMeasurement(Enum measureUnit)
    {
        return MeasurementCollection[measureUnit];
    }
    #endregion
    #endregion
}
