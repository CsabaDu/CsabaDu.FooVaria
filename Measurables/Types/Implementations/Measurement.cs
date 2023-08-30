namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Measurement : Measurable, IMeasurement
{
    #region Constants
    public const decimal DefaultExchangeRate = decimal.One;
    private const string DefaultCustomMeasureUnitName = "Default";
    #endregion

    #region Static properties
    private static IDictionary<Enum, decimal> ExchangeRateCollection { get; set; }
    private static IDictionary<Enum, string> CustomNameCollection { get; set; } = new SortedList<Enum, string>();
    #endregion

    #region Constructors
    #region Static constructor
    static Measurement()
    {
        ExchangeRateCollection = GetConstantExchangeRates(new SortedList<Enum, decimal>());
    }
    #endregion

    internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit) : base(measurementFactory, measureUnit)
    {
        ExchangeRate = GetExchangeRate(measureUnit);
        MeasureUnit = measureUnit;
    }

    internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit, decimal exchangeRate, string customName) : base(measurementFactory, measureUnit)
    {
        SetCustomMeasurementValidatedParams(measureUnit, exchangeRate, customName);

        ExchangeRate = exchangeRate;
        MeasureUnit = measureUnit;
    }

    internal Measurement(IMeasurementFactory measurementFactory, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(measurementFactory, measureUnitTypeCode)
    {
        SetCustomMeasurementValidatedParams(customName, measureUnitTypeCode, exchangeRate, out Enum measureUnit);

        ExchangeRate = exchangeRate;
        MeasureUnit = measureUnit;
    }
    #endregion

    #region Properties
    public object MeasureUnit { get; init; }
    public decimal ExchangeRate { get; init; }
    #endregion

    #region Public methods
    public int CompareTo(IMeasurement? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitTypeCode(other.MeasureUnitTypeCode);

        return ExchangeRate.CompareTo(other.ExchangeRate);
    }

    public bool Equals(IMeasurement? other)
    {
        return other?.IsExchangeableTo(MeasureUnitTypeCode) == true
            && other.ExchangeRate == ExchangeRate;
    }

    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other
            && Equals(other);
    }

    public IDictionary<Enum, decimal> GetConstantExchangeRateCollection()
    {
        return GetConstantExchangeRates(new SortedList<Enum, decimal>());
    }

    public ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(measureUnit)) return null;

        if (!(IsValidMeasureUnit(measureUnit, exchangeRate) && TrySetCustomName(measureUnit, customName))) return null;

        if (!(TrySetCustomMeasureUnit(measureUnit, exchangeRate) && TrySetCustomName(measureUnit, customName))) return null;

        return GetMeasurementFactory().Create(measureUnit, exchangeRate, customName);
    }

    public IEnumerable<MeasureUnitTypeCode> GetCustomMeasureUnitTypeCodes()
    {
        foreach (MeasureUnitTypeCode item in GetMeasureUnitTypeCodes())
        {
            if (IsCustomMeasureUnitTypeCode(item))
            {
                yield return item;
            }
        }
    }

    public string? GetCustomName(Enum? measureUnit = null)
    {
        measureUnit ??= GetMeasureUnit();

        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public IDictionary<Enum, string> GetCustomNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode != null) return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode.Value);

        return new SortedList<Enum, string>(CustomNameCollection);
    }

    public override IMeasurable GetDefault()
    {
        Enum measureUnit = GetDefaultMeasureUnit();

        return GetMeasurement(measureUnit);
    }

    public IMeasurement GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = GetDefaultMeasureUnit(measureUnitTypeCode);

        return GetMeasurement(measureUnit);
    }

    public decimal GetExchangeRate(string name)
    {
        Enum? measureUnit = GetMeasureUnit(name);
        decimal? exchangeRate = GetExchangeRateOrNull(measureUnit);

        if (exchangeRate != null) return exchangeRate.Value;

        throw new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public IDictionary<Enum, decimal> GetExchangeRateCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode == null) return new SortedList<Enum, decimal>(ExchangeRateCollection);

        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitTypeCode.Value);
    }

    public decimal GetExchangeRate(Enum measureUnit)
    {
        decimal? exchangeRate = GetExchangeRateOrNull(NullChecked(measureUnit, nameof(measureUnit)));

        if (exchangeRate != null) return exchangeRate.Value;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return GetMeasurementFactory().Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement? other = null)
    {
        return GetMeasurementFactory().Create(other ?? this);
    }

    public IMeasurement GetMeasurement(IBaseMeasure baseMeasure)
    {
        return NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
    }

    public IMeasurement GetMeasurement(string name)
    {
        Enum? measureUnit = GetMeasureUnit(name);

        if (measureUnit != null) return GetMeasurement(measureUnit);

        throw new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public IMeasurementFactory GetMeasurementFactory()
    {
        return MeasurableFactory as IMeasurementFactory ?? throw new InvalidOperationException(null);
    }

    public override Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public Enum? GetMeasureUnit(string name)
    {
        return GetMeasureUnitCollection().FirstOrDefault(x => x.Key == NullChecked(name, nameof(name))).Value;
    }

    public IDictionary<string, Enum> GetMeasureUnitCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        IDictionary<string, Enum> measureUnitCollection = CustomNameCollection.ToDictionary(x => x.Value, x => x.Key);

        foreach (Enum measureUnit in GetValidMeasureUnits(measureUnitTypeCode))
        {
            string defaultName = GetDefaultName(measureUnit);
            measureUnitCollection.Add(defaultName, measureUnit);
        }

        return measureUnitCollection;
    }

    public string GetName(Enum? measureUnit = null)
    {
        measureUnit ??= GetMeasureUnit();

        return GetCustomName(measureUnit) ?? GetDefaultName(measureUnit);
    }

    public ICustomMeasurement GetCustomMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetMeasurementFactory().Create(measureUnitTypeCode, exchangeRate, customName);
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        IEnumerable<MeasureUnitTypeCode> customMeasureUnitTypeCodes = getCustomMeasureUnitTypeCodes();
        IEnumerable<Enum> notUsedCustomMeasureUnits = getNotUsedCustomMeasureUnits(customMeasureUnitTypeCodes.First());
        int count = customMeasureUnitTypeCodes.Count();

        if (count == 1) return notUsedCustomMeasureUnits;

        for (int i = 1; i < count; i++)
        {
            IEnumerable<Enum> next = getNotUsedCustomMeasureUnits(customMeasureUnitTypeCodes.ElementAt(i));
            notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        }

        return notUsedCustomMeasureUnits;

        #region Local methods
        IEnumerable<Enum> getNotUsedCustomMeasureUnits(MeasureUnitTypeCode customMeasureUnitTypeCode)
        {
            Type customMeasureUnitType = GetMeasureUnitType(customMeasureUnitTypeCode);
            Array customMeasureUnits = Enum.GetValues(customMeasureUnitType);

            foreach (Enum item in customMeasureUnits)
            {
                if (!ExchangeRateCollection.ContainsKey(item))
                {
                    yield return item;
                }
            }
        }

        IEnumerable<MeasureUnitTypeCode> getCustomMeasureUnitTypeCodes()
        {
            if (measureUnitTypeCode == null) return GetCustomMeasureUnitTypeCodes();

            return getCustomMeasureUnitTypeCodesShortList();
        }

        IEnumerable<MeasureUnitTypeCode> getCustomMeasureUnitTypeCodesShortList()
        {
            yield return measureUnitTypeCode.Value;
        }
        #endregion
    }

    public IMeasurement? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        IBaseMeasure? baseMeasure = NullChecked(rate, nameof(rate)).GetRateComponent(rateComponentCode);

        return baseMeasure?.Measurement;
    }

    public RateComponentCode? GetRateComponentCode()
    {
        return null;
    }

    public IEnumerable<Enum> GetValidMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return ExchangeRateCollection.Keys;
    }

    public void InitiateCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        int count = customExchangeRateCollection?.Count ?? 0;

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(customExchangeRateCollection), count, null);

        IDictionary<Enum, decimal> exchangeRateCollection = ExchangeRateCollection;
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);
        IEnumerable<string> customNames = customExchangeRateCollection!.Keys;
        IEnumerable<decimal> exchangeRates = customExchangeRateCollection.Values;

        SetExchangeRateCollection(ref exchangeRateCollection, measureUnitType, exchangeRates, customNames);

        ExchangeRateCollection = exchangeRateCollection;
    }

    public bool IsCustomMeasureUnit(Enum measureUnit)
    {
        if (measureUnit == null || !IsDefinedMeasureUnit(measureUnit)) return false;

        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

        return IsCustomMeasureUnitTypeCode(measureUnitTypeCode);
    }

    public bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return IsCustomMeasureUnitTypeCode(measureUnitTypeCode ?? MeasureUnitTypeCode);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

        return IsExchangeableTo(context, MeasureUnitTypeCode);
    }

    public bool IsValidMeasureUnit(Enum? measureUnit, decimal? exchangeRate = null)
    {
        if (measureUnit == null) return false;

        if (!ExchangeRateCollection.ContainsKey(measureUnit)) return false;

        if (exchangeRate == null) return true;

        return ExchangeRateCollection[measureUnit] == exchangeRate.Value;
    }

    public decimal ProportionalTo(IMeasurement measurement)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurement, nameof(measurement)).MeasureUnitTypeCode;

        if (IsExchangeableTo(measureUnitTypeCode)) return ExchangeRate / measurement.ExchangeRate;

        throw new ArgumentOutOfRangeException(nameof(measurement), measureUnitTypeCode, null);
    }

    public void RestoreConstantExchangeRateCollection()
    {
        CustomNameCollection.Clear();
        ExchangeRateCollection = GetConstantExchangeRates(ExchangeRateCollection);
    }

    public void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate)
    {
        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public void SetCustomName(Enum measureUnit, string customName)
    {
        if (TrySetCustomName(measureUnit, customName)) return;

        throw CustomNameArgumentOutOfRangeException(customName);
    }

    public void SetOrReplaceCustomName(string customName)
    {
        ValidateCustomName(customName);

        Enum measureUnit = GetMeasureUnit();

        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

        if (CustomNameCollection.Remove(measureUnit) && CustomNameCollection.TryAdd(measureUnit, customName)) return;

        throw new InvalidOperationException(null);
    }

    public bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement)
    {
        customMeasurement = GetCustomMeasurement(measureUnit, exchangeRate, customName);

        return customMeasurement != null;
    }

    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate)
    {
        ValidateExchangeRate(exchangeRate);

        if (!IsCustomMeasureUnit(measureUnit)) return false;

        return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
    }

    public bool TrySetCustomName(Enum measureUnit, string customName)
    {
        return measureUnit != null
            && customName != null
            && CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName
            || IsValidMeasureUnit(measureUnit)
            && IsValidCustomName(customName)
            && CustomNameCollection.TryAdd(measureUnit!, customName!);
    }

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    public void ValidateCustomName(string? customName)
    {
        if (IsValidCustomName(customName)) return;
        
        throw CustomNameArgumentOutOfRangeException(customName);
    }

    public void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null)
    {
        if (NullChecked(exchangeRate, nameof(exchangeRate)) > 0)
        {
            if (measureUnit == null) return;

            if (GetExchangeRate(measureUnit) == exchangeRate) return;
        }

        throw ExchangeRateArgumentOutOfRangeException(exchangeRate);
    }

    public override void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (IsExchangeableTo(measureUnit, measureUnitTypeCode ?? MeasureUnitTypeCode)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (measureUnitTypeCode == MeasureUnitTypeCode) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }
    #endregion

    #region Private methods
    private static decimal? GetExchangeRateOrNull(Enum? measureUnit)
    {
        if (measureUnit == null) return null;

        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key == measureUnit).Value;

        if (exchangeRate == default) return null;

        return exchangeRate;
    }

    private static IDictionary<Enum, decimal> GetConstantExchangeRates(IDictionary<Enum, decimal> exchangeRateCollection)
    {
        exchangeRateCollection.Clear();

        SetConstantExchangeRates<AreaUnit>(ref exchangeRateCollection, 100, 10000, 1000000);
        SetConstantExchangeRates<Currency>(ref exchangeRateCollection);
        SetConstantExchangeRates<DistanceUnit>(ref exchangeRateCollection, 1000);
        SetConstantExchangeRates<Pieces>(ref exchangeRateCollection);
        SetConstantExchangeRates<ExtentUnit>(ref exchangeRateCollection, 10, 100, 1000);
        SetConstantExchangeRates<TimePeriodUnit>(ref exchangeRateCollection, 60, 1440, 10080, 14400);
        SetConstantExchangeRates<VolumeUnit>(ref exchangeRateCollection, 1000, 1000000, 1000000000);
        SetConstantExchangeRates<WeightUnit>(ref exchangeRateCollection, 1000, 1000000);

        return exchangeRateCollection;
    }

    private IDictionary<Enum, T> GetMeasureUnitBasedCollection<T>(IDictionary<Enum, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
    {
        return measureUnitBasedCollection
            .Where(x => x.Key.GetType() == GetMeasureUnitType(measureUnitTypeCode))
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    private bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IEnumerable<string> defaultNames = GetDefaultNames(measureUnitTypeCode);

        return defaultNames?.First() == DefaultCustomMeasureUnitName;
    }

    private bool IsExchangeableTo(Enum? measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
    {
        return IsValidMeasureUnit(measureUnit) && HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);
    }

    private static bool IsValidCustomName(string? customName)
    {
        return !string.IsNullOrWhiteSpace(customName)
            && customName != string.Empty
            && doesNotContainCustomName(CustomNameCollection.Values)
            && doesNotContainCustomName(getAllMeasureUnitDefaultNames());

        #region Local methods
        bool doesNotContainCustomName(IEnumerable<string> names)
        {
            return !names.Any(x => x.ToLower() == customName.ToLower());
        }

        static IEnumerable<string> getAllMeasureUnitDefaultNames()
        {
            foreach (MeasureUnitTypeCode item in Enum.GetValues<MeasureUnitTypeCode>())
            {
                foreach (string name in item.GetMeasureUnitDefaultNames())
                {
                    yield return name;
                }
            }
        }
        #endregion
    }

    private static void SetConstantExchangeRates<T>(ref IDictionary<Enum, decimal> exchangeRateCollection, params decimal[] exchangeRates) where T : struct, Enum
    {
        Type measureUnitType = typeof(T);
        int namesCount = Enum.GetNames(measureUnitType).Length;
        int exchangeRatesCount = exchangeRates?.Length ?? 0;

        if (exchangeRatesCount != 0 && exchangeRatesCount != namesCount - 1) throw new InvalidOperationException(null);

        ExchangeRateCollection.Add(default(T), DefaultExchangeRate);

        if (exchangeRatesCount > 0)
        {
            SetExchangeRateCollection(ref exchangeRateCollection, measureUnitType, exchangeRates!, null);
        }
    }

    private static void SetExchangeRateCollection(ref IDictionary<Enum, decimal> exchangeRateCollection, Type measureUnitType, IEnumerable<decimal> exchangeRates, IEnumerable<string>? customNames)
    {
        string[] defaultNames = Enum.GetNames(measureUnitType);

        for (int i = 0; i < exchangeRates.Count(); i++)
        {
            Enum measureUnit = (Enum)Enum.Parse(measureUnitType, defaultNames[i + 1]);
            string? customName = CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
            string? newCustomName = customNames?.ElementAt(i);

            if (customName != null
                && customName == newCustomName
                || IsValidCustomName(newCustomName)
                && CustomNameCollection.TryAdd(measureUnit, newCustomName!)
)
            {
                exchangeRateCollection.Add(measureUnit, exchangeRates.ElementAt(i));
            }
        }
    }

    private void SetCustomMeasurementValidatedParams(Enum measureUnit, decimal exchangeRate, string customName) // Check!
    {
        SetCustomMeasureUnit(measureUnit, exchangeRate);

        if (TrySetCustomName(measureUnit, customName)) return;

        if (ExchangeRateCollection.Remove(measureUnit))
        {
            throw CustomNameArgumentOutOfRangeException(customName);
        }

        throw new InvalidOperationException(null);
    }

    private void SetCustomMeasurementValidatedParams(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, out Enum measureUnit)
    {
        measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        SetCustomMeasurementValidatedParams(measureUnit, exchangeRate, customName);
    }
    #endregion
}
