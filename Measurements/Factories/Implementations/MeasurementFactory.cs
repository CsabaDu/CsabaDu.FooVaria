namespace CsabaDu.FooVaria.Measurements.Factories.Implementations;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    #region Private properties
    #region Static properties
    private static Dictionary<object, IMeasurement> MeasurementCollection
        => ExchangeRateCollection.Keys.ToDictionary
        (
            x => x,
            CreateMeasurement
        );
    #endregion
    #endregion
    #endregion

    #region Public methods
    public IMeasurement Create(IBaseMeasurement baseMeasurement)
    {
        Enum measureUnit = NullChecked(baseMeasurement, nameof(baseMeasurement)).GetBaseMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        bool success = TryGetMeasureUnit(measureUnitCode, exchangeRate, out Enum? measureUnit)
            && TrySetCustomName(measureUnit, customName);

        if (success) return GetStoredMeasurement(measureUnit!);

        success = measureUnitCode.IsCustomMeasureUnitCode()
            && TrySetCustomMeasureUnit(customName, measureUnitCode, exchangeRate)
            && TryGetMeasureUnit(customName, out measureUnit);

        return GetStoredMeasurementOrNull(measureUnit, success);
    }

    public IMeasurement? Create(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (measureUnit is null) return null;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(measureUnit, nameof(measureUnit));
        measureUnit = measureUnitElements.MeasureUnit;

        bool success = ExchangeRateCollection.TryGetValue(measureUnit, out decimal validExchangeRate)
            && exchangeRate == validExchangeRate
            && TrySetCustomName(measureUnit, customName);

        if (success) return GetStoredMeasurement(measureUnit);

        MeasureUnitCode measureUnitCode = measureUnitElements.MeasureUnitCode;
        success = measureUnitCode.IsCustomMeasureUnitCode()
            && TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);

        return GetStoredMeasurementOrNull(measureUnit, success);
    }

    public IMeasurement Create(Enum context)
    {
        const string paramName = nameof(context);
        Enum measureUnit = GetMeasurementElements(context, paramName).MeasureUnit;

        return GetStoredMeasurement(measureUnit);
    }

    public IMeasurement Create(string name)
    {
        if (GetStoredMeasurementOrNull(NullChecked(name, nameof(name))) is Enum measureUnit) return GetStoredMeasurement(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public IBaseMeasurement? CreateBaseMeasurement(Enum context)
    {
        return context switch
        {
            MeasureUnitCode measureUnitCode => (IBaseMeasurement?)CreateDefault(measureUnitCode),
            Enum measureUnit => GetStoredMeasurementOrNull(measureUnit, true),

            _ => null,
        };
    }

    public IMeasurable? CreateDefault(MeasureUnitCode measureUnitCode)
    {
        Enum? measureUnit = measureUnitCode.GetDefaultMeasureUnit();

        return GetStoredMeasurementOrNull(measureUnit, true);
    }

    public IMeasurement CreateNew(IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetBaseMeasureUnit();

        return GetStoredMeasurement(measureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IMeasurement CreateMeasurement(object obj)
    {
        Enum measureUnit = (Enum)obj;

        MeasurementFactory factory = new();

        return GetDefinedMeasureUnitCode(measureUnit).IsCustomMeasureUnitCode() ?
            new CustomMeasurement(factory, measureUnit)
            : new ConstantMeasurement(factory, measureUnit);
    }

    private static IMeasurement GetStoredMeasurement(Enum measureUnit)
    {
        return MeasurementCollection[measureUnit];
    }

    private static IMeasurement? GetStoredMeasurementOrNull(string name)
    {
        Enum? measureUnit = getMeasureUnit(out bool success);

        return GetStoredMeasurementOrNull(measureUnit, success);

        #region Local methods
        object? getMeasureUnitByStoredName()
        {
            return GetNameCollection().FirstOrDefault(x => NameEquals(x.Value, name)).Key;
        }

        object? getMeasureUnitByDefaultName()
        {
            return GetNameCollection().Keys.FirstOrDefault(x => NameEquals(getDefaultName(x), name));
        }

        string getDefaultName(object measureUnit)
        {
            return GetDefaultName((Enum)measureUnit);
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

    private static IMeasurement? GetStoredMeasurementOrNull(Enum? measureUnit, bool success)
    {
        return measureUnit is not null && success ?
            MeasurementCollection.FirstOrDefault(x => x.Key == measureUnit).Value
            : null;
    }

    private static Dictionary<object, string> GetNameCollection()
    {
        return MeasurementCollection.Keys.ToDictionary
            (
                x => x,
                getMeasureUnitName
            );

        #region Local methods
        static string getMeasureUnitName(object measureUnit)
        {
            return GetMeasureUnitName((Enum)measureUnit);
        }
        #endregion
    }
    #endregion
    #endregion
}
