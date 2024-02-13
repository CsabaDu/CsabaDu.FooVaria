namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class Measurement(IMeasurementFactory factory, Enum measureUnit) : BaseMeasurement(factory, measureUnit), IMeasurement
{
    #region Properties
    public object MeasureUnit { get; init; } = measureUnit;

    #region Static properties
    public static Dictionary<object, string> CustomNameCollection { get; protected set; } = [];
    #endregion
    #endregion

    #region Public methods
    public string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public string? GetCustomName()
    {
        return GetCustomName(GetMeasureUnit());
    }

    public IDictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);
    }

    public IMeasurement GetDefault()
    {
        return (IMeasurement)GetDefault(MeasureUnitCode)!;
    }

    public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
    {
        return GetFactory().CreateDefault(measureUnitCode);
    }

    public string GetDefaultName()
    {
        Enum measureUnit = GetMeasureUnit();

        return GetDefaultName(measureUnit);
    }

    public new string GetDefaultName(Enum measureUnit)
    {
        return Measurable.GetDefaultName(measureUnit);
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return GetFactory().Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return GetFactory().CreateNew(other);
    }

    public IMeasurement GetMeasurement(string name)
    {
        return GetFactory().Create(name);
    }

    public IMeasurement? GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        return GetFactory().Create(customName, measureUnitCode, exchangeRate);
    }

    public IMeasurement? GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetFactory().Create(measureUnit, exchangeRate, customName);
    }

    public Enum? GetMeasureUnit(string name)
    {
        name ??= string.Empty;

        return (Enum)CustomNameCollection.FirstOrDefault(x => x.Value == name).Key
            ?? (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == name).Value;
    }

    public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitCode measureUnitCode)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitCode);
        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    public IDictionary<string, object> GetMeasureUnitCollection()
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits();

        return GetMeasureUnitCollection(validMeasureUnits, CustomNameCollection);
    }

    public IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        return GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitCode.GetMeasureUnitType()));
    }

    public void SetCustomName(Enum measureUnit, string customName)
    {
        if (TrySetCustomName(measureUnit, customName)) return;

        throw NameArgumentOutOfRangeException(customName);
    }

    public void SetOrReplaceCustomName(string customName)
    {
        ValidateCustomName(customName);

        Enum measureUnit = GetMeasureUnit();

        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

        if (CustomNameCollection.Remove(measureUnit)
            && CustomNameCollection.TryAdd(measureUnit, customName))
            return;

        throw new InvalidOperationException(null);
    }

    public bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement)
    {
        if (TryGetMeasureUnit(MeasureUnitCode, exchangeRate, out Enum? measureUnit) && measureUnit != null)
        {
            measurement = GetMeasurement(measureUnit);

            return true;
        }

        measurement = null;

        return false;
    }

    public bool TryGetMeasureUnit(MeasureUnitCode measureUnitCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
    }

    public bool TrySetCustomName(Enum? measureUnit, string? customName)
    {
        if (measureUnit == null) return false;

        if (customName == null) return false;

        if (!IsValidMeasureUnit(measureUnit)) return false;

        if (CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName) return true;

        if (!IsValidCustomNameParam(customName)) return false;

        return CustomNameCollection.TryAdd(measureUnit, customName);
    }

    public void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

        throw NameArgumentOutOfRangeException(nameof(customName), customName!);
    }

    #region Override methods
    #region Sealed methods
    public override sealed decimal GetExchangeRate(string name)
    {
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, nameof(name)));

        if (measureUnit != null) return GetExchangeRate(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public override sealed IMeasurementFactory GetFactory()
    {
        return (IMeasurementFactory)Factory;
    }

    public override sealed Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public override sealed string GetName()
    {
        return GetCustomName() ?? GetDefaultName();
    }

    //public override sealed void RestoreConstantExchangeRates() // TODO
    //{
    //    RestoreCustomNameCollection();
    //    ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
    //}
    #endregion
    #endregion
    #endregion

    #region Internal methods
    #region Static methods
    internal static void RestoreConstantExchangeRates()
    {
        CustomNameCollection.Clear();
        ExchangeRateCollection = new(ConstantExchangeRateCollection);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool IsValidCustomNameParam(string? customName)
    {
        return !string.IsNullOrWhiteSpace(customName)
            && customName != string.Empty
            && doesNotContainCustomName(CustomNameCollection.Values)
            && doesNotContainCustomName(GetDefaultNames());

        #region Local methods
        bool doesNotContainCustomName(IEnumerable<string> names)
        {
            return !names.Any(x => NameEquals(x, customName));
        }
        #endregion
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static Dictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
    {
        Dictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary
            (
                getDefaultName,
                x => x
            );

        foreach (KeyValuePair<object, string> item in customNameCollection)
        {
            measureUnitCollection.Add(item.Value, item.Key);
        }

        return measureUnitCollection;

        #region Local methods
        static string getDefaultName(object measureUnit)
        {
            return Measurable.GetDefaultName((Enum)measureUnit);
        }
        #endregion
    }
    #endregion
    #endregion
}