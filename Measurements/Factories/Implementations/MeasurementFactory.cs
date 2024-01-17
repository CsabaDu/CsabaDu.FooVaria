using CsabaDu.FooVaria.Measurements.Types.Implementations;

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
    public IMeasurement Create(IBaseMeasurement baseMeasurement)
    {
        Enum measureUnit = NullChecked(baseMeasurement, nameof(baseMeasurement)).GetMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        IMeasurement? measurement = CreateDefault(measureUnitCode);

        if (measurement == null) return null;

        bool success = measurement.TryGetMeasureUnit(measureUnitCode, exchangeRate, out Enum? measureUnit)
            && measurement.TrySetCustomName(measureUnit, customName);

        if (success) return GetStoredMeasurement(measureUnit!);

        success = measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(customName, measureUnitCode, exchangeRate);
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

        if (!TryGetMeasureUnitCode(measureUnit, out MeasureUnitCode? measureUnitCode)) return null;

        measurement = CreateDefault(measureUnitCode.Value);
        success = measurement is ICustomMeasurement customMeasurement
            && customMeasurement.TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);

        return GetStoredMeasurementOrNull(measureUnit!, success);
    }

    public IMeasurement Create(Enum measureUnit)
    {
        if (IsValidMeasureUnit(NullChecked(measureUnit, nameof(measureUnit)))) return GetStoredMeasurement(measureUnit);

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement Create(string name)
    {
        if (GetStoredMeasurementOrNull(NullChecked(name, nameof(name))) is Enum measureUnit) return GetStoredMeasurement(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public IBaseMeasurement? CreateBaseMeasurement(object context)
    {
        return context switch
        {
            string name => GetStoredMeasurementOrNull(name),
            MeasureUnitCode measureUnitCode => CreateDefault(measureUnitCode),
            Enum measureUnit => GetStoredMeasurementOrNull(measureUnit, true),
            BaseMeasurement baseMeasurement => Create(baseMeasurement),

            _ => null,
        };
    }

    public IMeasurement? CreateDefault(MeasureUnitCode measureUnitCode)
    {
        Enum? measureUnit = GetDefaultMeasureUnit(measureUnitCode);

        bool success = measureUnit != null;

        return GetStoredMeasurementOrNull(measureUnit!, success);
    }

    public IMeasurement CreateNew(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement CreateMeasurement(object obj)
    {
        Enum measureUnit = (Enum)obj;
        MeasurementFactory factory = new();

        return GetMeasureUnitCode(measureUnit).IsCustomMeasureUnitCode() ?
            new CustomMeasurement(factory, measureUnit)
            : new ConstantMeasurement(factory, measureUnit);
    }

    private static IMeasurement GetStoredMeasurement(Enum measureUnit)
    {
        return MeasurementCollection[measureUnit];
    }

    private static IMeasurement? GetStoredMeasurementOrNull(string name)
    {
        string? nameToLower = name?.ToLower();
        Enum? measureUnit = getMeasureUnit(out bool success);

        return GetStoredMeasurementOrNull(measureUnit!, success);

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

        Enum? getMeasureUnit(out bool success)
        {
            object? obj = getMeasureUnitByStoredName() ?? getMeasureUnitByDefaultName();

            if (obj is Enum measureUnit)
            {
                success = true;
                return measureUnit;
            }

            success = false;
            return null;
        }
        #endregion
    }

    private static IMeasurement? GetStoredMeasurementOrNull(Enum measureUnit, bool success)
    {
        return success ?
            MeasurementCollection.FirstOrDefault(x => x.Key == measureUnit).Value
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
