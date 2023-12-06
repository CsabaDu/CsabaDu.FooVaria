namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class Measurement : MeasurementBase, IMeasurement
{
    #region Constructors
    #region Static constructor
    static Measurement()
    {
        CustomNameCollection = new Dictionary<object, string>();
    }
    #endregion

    private protected Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
        ExchangeRate = GetExchangeRate(measureUnit);
        MeasureUnit = measureUnit;
    }
    #endregion

    #region Properties
    public object MeasureUnit { get; init; }

    #region Override properties
    #region Sealed properties
    public override sealed decimal ExchangeRate { get; init; }
    #endregion
    #endregion

    #region Static prpperties
    public static IDictionary<object, string> CustomNameCollection { get; protected set; }
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

    public IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);
    }

    public IMeasurement GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetFactory().CreateDefault(measureUnitTypeCode);
    }

    public string GetDefaultName()
    {
        return Enum.GetName(MeasureUnit.GetType(), MeasureUnit)!;
    }

    public string GetDefaultName(Enum measureUnit)
    {
        Type measureUnitType = DefinedMeasureUnit(measureUnit, nameof(MeasureUnit)).GetType();

        return Enum.GetName(measureUnitType, measureUnit)!;
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return GetFactory().Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return GetFactory().Create(other);
    }

    public IMeasurement GetMeasurement(string name)
    {
        return GetFactory().Create(name);
    }

    public IMeasurement GetMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate);
    }

    public IMeasurement GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetFactory().Create(measureUnit, exchangeRate, customName);
    }

    public Enum? GetMeasureUnit(string name)
    {
        name ??= string.Empty;

        return (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == name).Value;
    }

    public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitTypeCode);
        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    public IDictionary<string, object> GetMeasureUnitCollection()
    {
        IEnumerable<object> validMeasureUnits = Statics.MeasureUnits.GetValidMeasureUnits();

        return GetMeasureUnitCollection(validMeasureUnits, CustomNameCollection);
    }

    public IEnumerable<object> GetValidMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return Statics.MeasureUnits.GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitTypeCode.GetMeasureUnitType()));
    }

    public void RestoreCustomNameCollection()
    {
        CustomNameCollection.Clear();
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
        if (TryGetMeasureUnit(MeasureUnitTypeCode, exchangeRate, out Enum? measureUnit) && measureUnit != null)
        {
            measurement = GetMeasurement(measureUnit);

            return true;
        }

        measurement = null;

        return false;
    }

    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        exchangeRate.ValidateExchangeRate();

        measureUnit = (Enum)GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
    }

    public bool TrySetCustomName(Enum measureUnit, string customName)
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

    public override sealed void RestoreConstantExchangeRates()
    {
        RestoreCustomNameCollection();

        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
    }
    #endregion
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
            return !names.Any(x => x.ToLower() == customName.ToLower());
        }
        #endregion
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static IDictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
    {
        IDictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary
            (
                x => MeasureUnitTypes.GetDefaultName((Enum)x),
                x => x
            );

        foreach (KeyValuePair<object, string> item in customNameCollection)
        {
            measureUnitCollection.Add(item.Value, item.Key);
        }

        return measureUnitCollection;
    }
    #endregion
    #endregion
}