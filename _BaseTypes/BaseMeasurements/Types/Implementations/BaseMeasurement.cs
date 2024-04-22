namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;

public abstract class BaseMeasurement(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName), IBaseMeasurement
{
    #region Records
    public sealed record MeasurementElements(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, decimal ExchangeRate) : MeasureUnitElements(MeasureUnit, MeasureUnitCode);
    #endregion

    #region Static fields
    public static readonly int ConstantExchangeRateCount;

    #region Private static fields
    private static readonly decimal[] AreaExchangeRates = [100, 10000, 1000000];
    private static readonly decimal[] DistanceExchangeRates = [1000];
    private static readonly decimal[] ExtentExchangeRates = [10, 100, 1000];
    private static readonly decimal[] TimePeriodExchangeRates = [60, 1440, 10080, 14400];
    private static readonly decimal[] VolumeExchangeRates = [1000, 1000000, 1000000000];
    private static readonly decimal[] WeightExchangeRates = [1000, 1000000];
    #endregion
    #endregion

    #region Constructors
    #region Static constructor
    static BaseMeasurement()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ConstantExchangeRateCount = ConstantExchangeRateCollection.Count;
        ExchangeRateCollection = new(ConstantExchangeRateCollection);
        CustomNameCollection = [];
    }

    #endregion
    #endregion

    #region Properties
    #region Static properties
    public static Dictionary<object, decimal> ConstantExchangeRateCollection { get; }
    public static Dictionary<object, decimal> ExchangeRateCollection { get; protected set; }
    public static Dictionary<object, string> CustomNameCollection { get; protected set; }
    #endregion
    #endregion

    #region Public methods
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

    public static string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public static Dictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);
    }

    public static decimal GetExchangeRate(string name)
    {
        const string paramName = nameof(name);
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, paramName));

        if (measureUnit is not null) return GetExchangeRate(measureUnit, paramName);

        throw NameArgumentOutOfRangeException(name);
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

    //public static IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode)
    //{
    //    return GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitCode.GetMeasureUnitType()));
    //}

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

        return measureUnit is not null;
    }

    public static bool TryGetMeasureUnit(MeasureUnitCode measureUnitCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit is not null;
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
        if (measureUnit is null) return false;

        if (customName is null) return false;

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

    public static Dictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitCode);
    }

    public static IEnumerable<object> GetConstantMeasureUnits()
    {
        foreach (object item in ConstantExchangeRateCollection.Keys)
        {
            yield return item;
        }
    }

    public static decimal GetExchangeRate(Enum? measureUnit, string paramName)
    {
        measureUnit = GetMeasureUnitElements(measureUnit, paramName).MeasureUnit;
        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate != default) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }

    public static Dictionary<object, decimal> GetExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitCode);
    }

    public static MeasurementElements GetMeasurementElements(Enum? context, string paramName)
    {
        Enum? measureUnit = context is MeasureUnitCode measureUnitCode ?
            Defined(measureUnitCode, paramName).GetDefaultMeasureUnit()
            : DefinedMeasureUnit(context, paramName);
        decimal exchangeRate = GetExchangeRate(measureUnit, paramName);
        measureUnitCode = context!.Equals(measureUnit) ?
            GetMeasureUnitCode(context)
            : (MeasureUnitCode)context!;

        return new(measureUnit!, measureUnitCode, exchangeRate);
    }

    public static IEnumerable<object> GetValidMeasureUnits()
    {
        return ExchangeRateCollection.Keys;
    }

    public static IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();

        return GetValidMeasureUnits().Where(x => x.GetType() == measureUnitType);
    }

    public static bool IsCustomMeasureUnit(Enum measureUnit)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        MeasureUnitCode measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);

        return measureUnitCode.IsCustomMeasureUnitCode();
    }

    public static bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return exchangeRate == GetExchangeRate(measureUnit, nameof(measureUnit));
    }

    public static bool IsValidMeasureUnit(Enum? measureUnit)
    {
        return measureUnit is not null
            && GetValidMeasureUnits().Contains(measureUnit);
    }

    public static bool IsValidMeasureUnit(Enum? measureUnit, MeasureUnitCode measureUnitCode)
    {
        return measureUnit is not null
            && GetValidMeasureUnits(measureUnitCode).Contains(measureUnit);
    }

    public static bool NameEquals(string name, string otherName)
    {
        return string.Equals(name, otherName, StringComparison.OrdinalIgnoreCase);
    }

    public static void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

        throw NameArgumentOutOfRangeException(nameof(customName), customName!);
    }

    public static void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit)
    {
        if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

        throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
    }
    #endregion

    #region Abstract methods
    public abstract string GetName();
    #endregion

    #region Override methods
    #region Sealed methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseMeasurement other
            && Equals(other);
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(GetMeasureUnitCode(), GetExchangeRate());
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return base.HasMeasureUnitCode(measureUnitCode);
    }

    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        if (IsExchangeableTo(NullChecked(measureUnit, paramName))) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }
    #endregion
    #endregion

    public int CompareTo(IBaseMeasurement? other)
    {
        if (other is null) return 1;

        other.ValidateMeasureUnitCode(GetMeasureUnitCode(), nameof(other));

        return GetExchangeRate().CompareTo(other.GetExchangeRate());
    }

    public bool Equals(IBaseMeasurement? other)
    {
        return base.Equals(other)
            && GetExchangeRate() == other.GetExchangeRate();
    }

    public IBaseMeasurement? GetBaseMeasurement(Enum context)
    {
        IBaseMeasurementFactory factory = (IBaseMeasurementFactory)GetFactory();

        return factory.CreateBaseMeasurement(context);
    }

    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return GetConstantExchangeRateCollection(GetMeasureUnitCode());
    }

    public decimal GetExchangeRate()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetExchangeRate(measureUnit, string.Empty);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(GetMeasureUnitCode());
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return HasMeasureUnitCode(measureUnitCode);

        return IsValidMeasureUnit(context) && HasMeasureUnitCode(GetMeasureUnitCode(), context!);
    }

    public decimal ProportionalTo(IBaseMeasurement? other)
    {
        const string paramName = nameof(other);

        MeasureUnitCode measureUnitCode = NullChecked(other, paramName).GetMeasureUnitCode();

        if (HasMeasureUnitCode(measureUnitCode)) return GetExchangeRate() / other!.GetExchangeRate();

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }

    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        ValidateExchangeRate(exchangeRate, paramName, GetBaseMeasureUnit());
    }
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

    protected static Dictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitCode measureUnitCode)
        where T : notnull
    {
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

        string measureUnitCodeName = Enum.GetName(measureUnitCode)!;

        return measureUnitBasedCollection
            .Where(x => x.Key.GetType().Name == measureUnitCodeName)
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
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

    private static Dictionary<object, decimal> InitConstantExchangeRateCollection()
    {
        return initConstantExchangeRates<AreaUnit>(AreaExchangeRates)
            .Union(initConstantExchangeRates<Currency>())
            .Union(initConstantExchangeRates<DistanceUnit>(DistanceExchangeRates))
            .Union(initConstantExchangeRates<ExtentUnit>(ExtentExchangeRates))
            .Union(initConstantExchangeRates<Pieces>())
            .Union(initConstantExchangeRates<TimePeriodUnit>(TimePeriodExchangeRates))
            .Union(initConstantExchangeRates<VolumeUnit>(VolumeExchangeRates))
            .Union(initConstantExchangeRates<WeightUnit>(WeightExchangeRates))
            .ToDictionary(x => x.Key, x => x.Value);

        #region Local methods
        static IEnumerable<KeyValuePair<object, decimal>> initConstantExchangeRates<T>(params decimal[] exchangeRates)
            where T : struct, Enum
        {
            yield return getMeasureUnitExchangeRatePair(default(T), decimal.One);

            int exchangeRateCount = exchangeRates?.Length ?? 0;

            if (exchangeRateCount > 0)
            {
                T[] measureUnits = Enum.GetValues<T>();
                int measureUnitCount = measureUnits.Length;

                if (measureUnitCount != exchangeRateCount + 1) throw new InvalidOperationException(null);

                int i = 0;

                foreach (decimal item in exchangeRates!)
                {
                    yield return getMeasureUnitExchangeRatePair(measureUnits[++i], item);
                }
            }
        }

        static KeyValuePair<object, decimal> getMeasureUnitExchangeRatePair(Enum measureUnit, decimal exchangeRate)
        {
            return new KeyValuePair<object, decimal>(measureUnit, exchangeRate);
        }
        #endregion
    }
    #endregion
    #endregion
}
