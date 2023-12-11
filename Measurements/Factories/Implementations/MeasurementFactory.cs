using CsabaDu.FooVaria.Measurements.Types;
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
    public IMeasurement? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement? measurement = CreateDefault(measureUnitTypeCode);

        if (measurement == null) return null;

        bool success;

        if (measurement.TryGetMeasureUnit(measureUnitTypeCode, exchangeRate, out Enum? measureUnit))
        {
            success = measurement!.TrySetCustomName(measureUnit, customName);

            return GetStoredMeasurementOrNull(success, measureUnit);
        }  

        success = measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);

        if (!success) return null;

        measureUnit = measurement.GetMeasureUnit(customName)!;

        return GetStoredMeasurementOrNull(success, measureUnit);
    }

    public IMeasurement? Create(Enum measureUnit, decimal exchangeRate, string customName) // TODO
    {
        IMeasurement? measurement;
        bool success;

        if (MeasurementCollection.TryGetValue(NullChecked(measureUnit, nameof(measureUnit)), out measurement))
        {
            success = ExchangeRateCollection.TryGetValue(measureUnit, out decimal validExchangeRate)
                && exchangeRate != validExchangeRate
                && measurement.TrySetCustomName(measureUnit, customName);

            return GetStoredMeasurementOrNull(success, measureUnit);
        }

        if (!TryGetMeasureUnitTypeCode(measureUnit, out MeasureUnitTypeCode? measureUnitTypeCode)) return null;

        measurement = CreateDefault(measureUnitTypeCode.Value);

        success = measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);

        return GetStoredMeasurementOrNull(success, measureUnit);
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

    public IMeasurement? CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum? measureUnit = GetDefaultMeasureUnit(measureUnitTypeCode);

        return measureUnit != null ?
            MeasurementCollection[measureUnit]
            : null;
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

    private static IMeasurement? GetStoredMeasurementOrNull(bool success, object? measureUnit)
    {
        return success && measureUnit != null ?
            MeasurementCollection[measureUnit]
            : null;
    }
    #endregion
    #endregion
}
