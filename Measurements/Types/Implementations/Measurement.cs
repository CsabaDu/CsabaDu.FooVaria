namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class Measurement : BaseMeasurement, IMeasurement
{
    #region Constructors
    private protected Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, nameof(factory))
    {
        ValidateMeasureUnit(measureUnit, nameof(measureUnit));

        MeasureUnit = measureUnit;
        Factory = factory;
    }
    #endregion

    #region Properties
    public IMeasurementFactory Factory { get; init; }
    public object MeasureUnit { get; init; }

    #region Static properties
    public static Dictionary<object, string> CustomNameCollection { get; protected set; } = [];
    #endregion
    #endregion

    #region Public methods
    public static string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public string? GetCustomName()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetCustomName(measureUnit);
    }

    public static Dictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);
    }

    public IDictionary<object, string> GetCustomNameCollection()
    {
        return GetCustomNameCollection(GetMeasureUnitCode());
    }

    public IMeasurement GetDefault()
    {
        return (IMeasurement)GetDefault(GetMeasureUnitCode())!;
    }

    public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
    {
        return Factory.CreateDefault(measureUnitCode);
    }

    public string GetDefaultName()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetDefaultName(measureUnit);
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return Factory.Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return Factory.CreateNew(other);
    }

    public IMeasurement GetMeasurement(string name)
    {
        return Factory.Create(name);
    }

    public IMeasurement? GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        return Factory.Create(customName, measureUnitCode, exchangeRate);
    }

    public IMeasurement? GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return Factory.Create(measureUnit, exchangeRate, customName);
    }

    public void SetCustomName(string customName)
    {
        SetCustomName(GetBaseMeasureUnit(), customName);
    }

    public void SetOrReplaceCustomName(string customName)
    {
        ValidateCustomName(customName);

        Enum measureUnit = GetBaseMeasureUnit();

        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

        if (CustomNameCollection.Remove(measureUnit)
            && CustomNameCollection.TryAdd(measureUnit, customName))
        {
            return;
        }

        throw new InvalidOperationException(null);
    }

    public bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement)
    {
        if (TryGetMeasureUnit(GetMeasureUnitCode(), exchangeRate, out Enum? measureUnit) && measureUnit != null)
        {
            measurement = GetMeasurement(measureUnit);

            return true;
        }

        measurement = null;

        return false;
    }

    public bool TrySetCustomName(string? customName)
    {
        return TrySetCustomName(GetBaseMeasureUnit(), customName);
    }

    public static void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

        throw NameArgumentOutOfRangeException(nameof(customName), customName!);
    }

    #region Override methods
    #region Sealed methods
    public static decimal GetExchangeRate(string name)
    {
        const string paramName = nameof(name);
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, paramName));

        if (measureUnit != null) return GetExchangeRate(measureUnit, paramName);

        throw NameArgumentOutOfRangeException(name);
    }

    public override sealed IMeasurementFactory GetFactory()
    {
        return Factory;
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public override sealed MeasureUnitCode GetMeasureUnitCode()
    {
        Enum measuureUnit = GetBaseMeasureUnit();

        return GetMeasureUnitCode(measuureUnit);
    }

    public override sealed string GetName()
    {
        return GetCustomName() ?? GetDefaultName();
    }
    #endregion
    #endregion

    #region Static methods
    public static IEnumerable<Enum> GetAllNotUsedCustomMeasureUnits()
    {
        IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(MeasureUnitCodes[0]);

        for (int i = 1; i < MeasureUnitCodes.Length; i++)
        {
            IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(MeasureUnitCodes[i]);
            notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        }

        return notUsedCustomMeasureUnits;
    }

    public static string GetMeasureUnitName(Enum measureUnit)
    {
        return GetCustomName(measureUnit) ?? GetDefaultName(measureUnit);
    }

    public static Enum? GetMeasureUnit(string name)
    {
        name ??= string.Empty;

        return (Enum)CustomNameCollection.FirstOrDefault(x => x.Value == name).Key
            ?? (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == name).Value;
    }

    public static Dictionary<string, object> GetMeasureUnitCollection(MeasureUnitCode measureUnitCode)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitCode);
        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    public static Dictionary<string, object> GetMeasureUnitCollection()
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits();

        return GetMeasureUnitCollection(validMeasureUnits, CustomNameCollection);
    }

    public static IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        return GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitCode.GetMeasureUnitType()));
    }
    public static IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        IEnumerable<Enum> customMeasureUnits = Enum.GetValues(measureUnitType).Cast<Enum>();

        return customMeasureUnits.Where(x => !ExchangeRateCollection.ContainsKey(x));
    }

    public static void InitCustomExchangeRates(MeasureUnitCode measureUnitCode, IDictionary<string, decimal> customExchangeRateCollection)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        int count = NullChecked(customExchangeRateCollection, nameof(customExchangeRateCollection)).Count;

        for (int i = 0; i < count; i++)
        {
            KeyValuePair<string, decimal> namedCustomExchangeRate = customExchangeRateCollection.ElementAt(i);
            string? customName = namedCustomExchangeRate.Key;
            decimal exchangeRate = namedCustomExchangeRate.Value;
            Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).First();

            if (!TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName))
            {
                throw DecimalArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate);
            }
        }
    }

    public static void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void SetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).OrderBy(x => x).First();

        SetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public static void SetCustomName(Enum measureUnit, string customName)
    {
        if (TrySetCustomName(measureUnit, customName)) return;

        throw NameArgumentOutOfRangeException(customName);
    }

    public static bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
    }

    public static bool TryGetMeasureUnit(MeasureUnitCode measureUnitCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public static bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(measureUnit)) return false;

        if ((int)(object)measureUnit == default) return false;

        if (!exchangeRate.IsValidExchangeRate()) return false;

        if (!TrySetExchangeRate(measureUnit, exchangeRate)) return false;

        if (TrySetCustomName(measureUnit, customName)) return true;

        if (RemoveExchangeRate(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }

    public static bool TrySetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        if (!measureUnitCode.IsCustomMeasureUnitCode()) return false;

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).OrderBy(x => x).First();

        return TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public static bool TrySetCustomName(Enum? measureUnit, string? customName)
    {
        if (measureUnit == null) return false;

        if (customName == null) return false;

        if (!IsValidMeasureUnit(measureUnit)) return false;

        if (CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName) return true;

        if (!IsValidCustomNameParam(customName)) return false;

        return CustomNameCollection.TryAdd(measureUnit, customName);
    }

    public static void ValidateCustomMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        if (measureUnitCode.IsCustomMeasureUnitCode()) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);
    }

    #endregion
    #endregion

    #region Internal methods
    #region Static methods
    internal static void RestoreConstantExchangeRates() // for unittest class restore purposes
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

    protected static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        if (ExchangeRateCollection.ContainsKey(measureUnit) && exchangeRate == ExchangeRateCollection[measureUnit]) return true;

        if (exchangeRate <= 0) return false;

        return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static bool RemoveExchangeRate(Enum measureUnit)
    {
        return ExchangeRateCollection.Remove(measureUnit);
    }

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
            return GetDefaultName((Enum)measureUnit);
        }
        #endregion
    }
    #endregion
    #endregion
}