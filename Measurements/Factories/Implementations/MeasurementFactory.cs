﻿using CsabaDu.FooVaria.Measurements.Types.Implementations;

namespace CsabaDu.FooVaria.Measurements.Factories.Implementations;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    #region Private properties
    #region Static properties
    private static IDictionary<object, IMeasurement> MeasurementCollection
        => ExchangeRateCollection.Keys.ToDictionary
        (
            x => x,
            x => CreateMeasurement(x)
        );
    #endregion
    #endregion
    #endregion

    #region Public methods
    public IMeasurement? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement? measurement = CreateDefault(measureUnitTypeCode);

        if (measurement == null) return null;

        bool success = measurement.TryGetMeasureUnit(measureUnitTypeCode, exchangeRate, out Enum? measureUnit)
            && measurement.TrySetCustomName(measureUnit, customName);

        if (success) return GetStoredMeasurement(measureUnit!);

        success = measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);
        measureUnit = measurement.GetMeasureUnit(customName);
        success = success
            && measureUnit != null;

        return GetStoredMeasurementOrNull(measureUnit!, success);
    }

    public IMeasurement? Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) return null;

        bool success = MeasurementCollection.TryGetValue(measureUnit, out IMeasurement? measurement)
            && ExchangeRateCollection.TryGetValue(measureUnit, out decimal validExchangeRate)
            && exchangeRate == validExchangeRate
            && measurement.TrySetCustomName(measureUnit, customName);

        if (success) return GetStoredMeasurement(measureUnit);

        if (!TryGetMeasureUnitTypeCode(measureUnit, out MeasureUnitTypeCode? measureUnitTypeCode)) return null;

        measurement = CreateDefault(measureUnitTypeCode.Value);
        success = measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);

        return GetStoredMeasurementOrNull(measureUnit!, success);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        if (IsValidMeasureUnit(NullChecked(measureUnit, nameof(measureUnit)))) return GetStoredMeasurement(measureUnit);

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }
    public IBaseMeasurement CreateBaseMeasurement(Enum measureUnit)
    {
        return Create(measureUnit);
    }

    public IMeasurement CreateNew(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement Create(string name)
    {
        string nameToLower = NullChecked(name, nameof(name)).ToLower();
        object? obj = getMeasureUnitByStoredName() ?? getMeasureUnitByDefaultName();

        if (obj is Enum measureUnit) return GetStoredMeasurement(measureUnit);

        throw NameArgumentOutOfRangeException(name);

        #region Local methods
        object? getMeasureUnitByStoredName()
        {
            return GetNameCollection().FirstOrDefault(x => x.Value.ToLower() == nameToLower).Key;
        }

        object? getMeasureUnitByDefaultName()
        {
            return GetNameCollection().Keys.FirstOrDefault(x => getDefaultNameToLower(x) == nameToLower);
        }

        static string getDefaultNameToLower(object measureUnit)
        {
            return GetDefaultName((Enum)measureUnit).ToLower();
        }
        #endregion
    }

    public IMeasurement? CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum? measureUnit = GetDefaultMeasureUnit(measureUnitTypeCode);

        bool success = measureUnit != null;

        return GetStoredMeasurementOrNull(measureUnit!, success);
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

    private static IMeasurement GetStoredMeasurement(Enum measureUnit)
    {
        return MeasurementCollection[measureUnit];
    }

    private static IMeasurement? GetStoredMeasurementOrNull(Enum measureUnit, bool success)
    {
        return success ?
            GetStoredMeasurement(measureUnit)
            : null;
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
