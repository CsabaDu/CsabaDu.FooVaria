using CsabaDu.FooVaria.Measurements.Types.Implementations;

namespace CsabaDu.FooVaria.Measurements.Factories.Implementations;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    #region Static properties
    private static IDictionary<object, IMeasurement> MeasurementCollection
        => ExchangeRateCollection.Keys.ToDictionary
        (
            x => x,
            x => CreateMeasurement(x)
        );
    #endregion
    #endregion

    #region Public methods
    public IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = CreateDefault(measureUnitTypeCode);

        if (measurement.TryGetMeasureUnit(measureUnitTypeCode, exchangeRate, out Enum? measureUnit))
        {
            if (measurement.TrySetCustomName(measureUnit, customName)) return MeasurementCollection[measureUnit];

            throw NameArgumentOutOfRangeException(customName, nameof(customName));
        }

        if (measurement is ICustomMeasurement customMeasurement)
        {
            exchangeRate.ValidateExchangeRate();

            if (customMeasurement.TrySetCustomMeasureUnit(NullChecked(customName, nameof(customName)), measureUnitTypeCode, exchangeRate))
            {
                measureUnit = measurement.GetMeasureUnit(customName)!;

                return MeasurementCollection[measureUnit];
            }

            throw NameArgumentOutOfRangeException(customName, nameof(customName));
        }

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    public IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (MeasurementCollection.TryGetValue(NullChecked(measureUnit, nameof(measureUnit)), out IMeasurement? measurement)
            && ExchangeRateCollection.TryGetValue(measureUnit, out decimal validExchangeRate))
        {
            if (exchangeRate != validExchangeRate) throw DecimalArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate);

            if (measurement.TrySetCustomName(measureUnit, customName)) return MeasurementCollection[measureUnit];

            throw NameArgumentOutOfRangeException(customName, nameof(customName));
        }

        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
        measurement = CreateDefault(measureUnitTypeCode);

        if (measurement is ICustomMeasurement customMeasurement)
        {
            exchangeRate.ValidateExchangeRate();

            if (customMeasurement.TrySetCustomMeasureUnit(measureUnit, exchangeRate, NullChecked(customName, nameof(customName))))
            {
                return MeasurementCollection[measureUnit];
            }

            throw NameArgumentOutOfRangeException(customName, nameof(customName));
        }

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        if (IsValidMeasureUnit(NullChecked(measureUnit, nameof(measureUnit)))) return MeasurementCollection[measureUnit];

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement Create(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return MeasurementCollection[measureUnit];
    }

    public IMeasurement Create(string name)
    {
        string nameToLower = NullChecked(name, nameof(name)).ToLower();
        object? obj = getMeasureUnitByStoredName() ?? getMeasureUnitByDefaultName();

        if (obj is Enum measureUnit) return MeasurementCollection[measureUnit];

        throw NameArgumentOutOfRangeException(name);

        #region Local methods
        object? getMeasureUnitByStoredName()
        {
            return GetNameCollection().FirstOrDefault(x => x.Value.ToLower() == nameToLower).Key;
        }

        object? getMeasureUnitByDefaultName()
        {
            return GetNameCollection().Keys.FirstOrDefault(x => GetDefaultName((Enum)x).ToLower() == nameToLower);
        }
        #endregion
    }

    public IMeasurement CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return MeasurementCollection[measureUnit];
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement CreateMeasurement(object obj)
    {
        Enum measureUnit = (Enum)obj;
        MeasurementFactory factory = new();

        return GetMeasureUnitTypeCode(measureUnit).IsCustomMeasureUnitTypeCode() ?
            new CustomMeasurement(factory, measureUnit)
            : new ConstantMeasurement(factory, measureUnit);
    }

    private static IDictionary<object, string> GetNameCollection()
    {
        return MeasurementCollection.ToDictionary
            (
                x => x.Key,
                x => x.Value.GetName()
            );
    }
    #endregion
    #endregion
}
