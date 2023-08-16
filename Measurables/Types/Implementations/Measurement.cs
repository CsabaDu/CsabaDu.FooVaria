namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Measurement : Measurable, IMeasurement
{
    #region Constants
    public const decimal DefaultExchangeRate = decimal.One;
    private const string DefaultCustomMeasureUnitName = "Default";
    #endregion

    #region Static properties
    private static IDictionary<Enum, decimal> ExchangeRateCollection { get; set; }
    private static IDictionary<Enum, string> CustomNameCollection { get; set; }
    #endregion

    #region Constructors
    static Measurement()
    {
        ExchangeRateCollection = GetConstantExchangeRates(new SortedList<Enum, decimal>());
        CustomNameCollection = new SortedList<Enum, string>();
    }

    internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit) : base(measurementFactory, measureUnit)
    {
        ExchangeRate = GetExchangeRate(measureUnit);
        MeasureUnit = measureUnit;
    }

    internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit, decimal exchangeRate, string? customName) : base(measurementFactory, measureUnit) // TODO
    {
        SetCustomMeasurementValidatedParams(measureUnit, exchangeRate, customName);

        ExchangeRate = exchangeRate;
        MeasureUnit = measureUnit;
    }

    internal Measurement(IMeasurementFactory measurementFactory, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName) : base(measurementFactory, measureUnitTypeCode) // TODO
    {
        SetCustomMeasurementValidatedParams(measureUnitTypeCode, exchangeRate, customName, out Enum measureUnit);

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
        return other?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) == true
            && other.ExchangeRate == ExchangeRate;
    }

    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other && Equals(other);
    }

    public IDictionary<Enum, decimal> GetConstantExchangeRateCollection()
    {
        var constantExchangeRateCollection = new SortedList<Enum, decimal>();

        return GetConstantExchangeRates(constantExchangeRateCollection);
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

    public IDictionary<Enum, decimal> GetExchangeRateCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode != null) return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitTypeCode.Value);

        return new SortedList<Enum, decimal>(ExchangeRateCollection);
    }

    public decimal GetExchangeRate(Enum measureUnit)
    {
        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key == measureUnit).Value;

        if (exchangeRate != default) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
    }

    public override IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
    {
        if (measurableFactory is IMeasurementFactory measurementFactory && measurable is IMeasurement measurement)
        {
            return measurementFactory.Create(measurement);
        }

        return base.GetMeasurable(measurableFactory, measurable);
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return GetMeasurementFactory().Create(measureUnit);
    }

    public IMeasurement GetMeasurement(Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        return GetMeasurementFactory().Create(measureUnit, exchangeRate, customName);
    }

    public IMeasurement GetMeasurement(IMeasurement? other = null)
    {
        return GetMeasurementFactory().Create(other ?? this);
    }

    public IMeasurement GetMeasurement(IBaseMeasure baseMeasure)
    {
        return NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
    }

    public IMeasurement? GetMeasurement(string name)
    {
        Enum? measureUnit = GetMeasureUnit(this, name);

        if (measureUnit == null) return null;
        
        return GetMeasurement(measureUnit);
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
        if (name == null) return null;

        return GetMeasureUnitCollection()[name];
    }

    public IDictionary<string, Enum> GetMeasureUnitCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        IDictionary<string, Enum> measureUnitCollection = CustomNameCollection.ToDictionary(x => x.Value, x => x.Key);

        foreach (Enum item in GetValidMeasureUnits(measureUnitTypeCode))
        {
            string defaultName = GetDefaultName(item);
            measureUnitCollection.Add(defaultName, item);
        }

        return measureUnitCollection;
    }

    public string GetName(Enum? measureUnit = null)
    {
        measureUnit ??= GetMeasureUnit();

        return GetCustomName(measureUnit) ?? GetDefaultName(measureUnit);
    }

    public ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName = null)
    {
        return GetMeasurementFactory().Create(customMeasureUnitTypeCode, exchangeRate, customName);
    }

    public IEnumerable<T> GetNotUsedCustomMeasureUnits<T>() where T : struct, Enum
    {
        T[] measureUnits = Enum.GetValues<T>();
        MeasureUnitTypeCode customMeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnits.First());

        ValidateCustomMeasureUnitTypeCode(customMeasureUnitTypeCode);

        return GetNotUsedCustomMeasureUnits<T>(measureUnits, customMeasureUnitTypeCode);
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode? customMeasureUnitTypeCode = null)
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
            Type measureUnitType = GetMeasureUnitType(customMeasureUnitTypeCode);
            Array measureUnits = Enum.GetValues(measureUnitType);

            return GetNotUsedCustomMeasureUnits<Enum>(measureUnits, customMeasureUnitTypeCode);
        }

        IEnumerable<MeasureUnitTypeCode> getCustomMeasureUnitTypeCodes()
        {
            if (customMeasureUnitTypeCode == null)
            {
                foreach (MeasureUnitTypeCode item in GetCustomMeasureUnitTypeCodes())
                {
                    yield return item;
                }
            }
            else
            {
                MeasureUnitTypeCode measureUnitTypeCode = customMeasureUnitTypeCode.Value;

                ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

                yield return measureUnitTypeCode;
            }
        }
        #endregion
    }

    public IEnumerable<Enum> GetValidMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return ExchangeRateCollection.Keys;
    }

    public IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        return GetMeasurementFactory().GetRateComponent(rate, rateComponentCode);
    }

    public void InitiateCustomExchangeRates(MeasureUnitTypeCode customMeasureUnitTypeCode, params decimal[] exchangeRates)
    {
        ValidateCustomMeasureUnitTypeCode(customMeasureUnitTypeCode);

        int count = exchangeRates?.Length ?? 0;

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(exchangeRates), count, null);

        Type measureUnitType = GetMeasureUnitType(customMeasureUnitTypeCode);
        IEnumerable<string> defaultNames = GetDefaultNames(customMeasureUnitTypeCode);

        SetExchangeRates(ExchangeRateCollection, measureUnitType, defaultNames, exchangeRates!);
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

    public bool IsExchangeableTo(Enum context)
    {
        if (context == null) return false;

        if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

        return IsExchangeableTo(context, MeasureUnitTypeCode);
    }

    public bool IsValidMeasureUnit(Enum measureUnit, decimal? exchangeRate = null)
    {
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

    public void RestoreConstantMeasureUnits()
    {
        CustomNameCollection.Clear();
        ExchangeRateCollection = GetConstantExchangeRates(ExchangeRateCollection);
    }

    public void SetCustomName(Enum measureUnit, string? customName)
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

    public bool TryGetMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IMeasurement? measurement)
    {
        measurement = null;

        if (IsValidMeasureUnit(measureUnit, exchangeRate) && TrySetCustomName(measureUnit, customName))
        {
            measurement = GetMeasurement(measureUnit);
        }

        else if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName))
        {
            measurement = GetMeasurement(measureUnit, exchangeRate);
        }

        return measurement != null;
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

    public bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        ValidateExchangeRate(exchangeRate);

        if (!IsCustomMeasureUnit(measureUnit)) return false;

        if (!ExchangeRateCollection.TryAdd(measureUnit, exchangeRate)) return false;

        if (TrySetCustomName(measureUnit, customName)) return true;

        if (ExchangeRateCollection.Remove(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }

    public bool TrySetCustomName(Enum measureUnit, string? customName)
    {
        return customName == null
            || CustomNameCollection.Contains(new KeyValuePair<Enum, string>(measureUnit, customName))
            || IsValidMeasureUnit(measureUnit)
            && IsValidCustomNameOrNull(customName)
            && CustomNameCollection.TryAdd(measureUnit, customName!);
    }

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    public void ValidateCustomName(string? customName)
    {
        _ = NullChecked(customName, nameof(customName));

        if (!IsValidCustomNameOrNull(customName)) throw CustomNameArgumentOutOfRangeException(customName);
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

    private static Enum? GetMeasureUnit(IMeasurement measurement, string name)
    {
        if (measurement.TryGetMeasureUnit(name, out Enum? measureUnit)) return measureUnit;

        foreach (KeyValuePair<Enum, decimal> item in measurement.GetExchangeRateCollection())
        {
            measureUnit = item.Key;

            if (name == measurement.GetDefaultName(measureUnit)) return measureUnit;
        }

        return null;
    }

    private IDictionary<Enum, T> GetMeasureUnitBasedCollection<T>(IDictionary<Enum, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
    {
        Type meeasureUnitType = GetMeasureUnitType(measureUnitTypeCode);

        return measureUnitBasedCollection
            .Where(x => x.Key.GetType() == meeasureUnitType)
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    private Enum GetNextNotUsedCustomMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetNotUsedCustomMeasureUnits(measureUnitTypeCode).First();
    }

    private IEnumerable<T> GetNotUsedCustomMeasureUnits<T>(Array measureUnits, MeasureUnitTypeCode customMeasureUnitTypeCode) where T : Enum
    {
        foreach (T measureUnit in measureUnits)
        {
            string defaultName = GetDefaultName(measureUnit);

            foreach (string item in GetDefaultNames(customMeasureUnitTypeCode))
            {
                if (item != defaultName)
                {
                    yield return measureUnit;
                }
            }
        }
    }

    private bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IEnumerable<string> defaultNames = GetDefaultNames(measureUnitTypeCode);

        return defaultNames?.First() == DefaultCustomMeasureUnitName;
    }

    private bool IsExchangeableTo(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
    {
        return IsValidMeasureUnit(measureUnit) && HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);
    }

    private bool IsValidCustomNameOrNull(string? customName)
    {
        return customName == null
            || !string.IsNullOrWhiteSpace(customName)
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

    private static void SetConstantExchangeRates<T>(ref IDictionary<Enum, decimal> exchangeRateCollection, params decimal[] exchangeRates) where T : struct, Enum
    {
        Type measureUnitType = typeof(T);
        string[] defaultNames = Enum.GetNames(measureUnitType);
        int namesCount = defaultNames.Length;
        int exchangeRatesCount = exchangeRates?.Length ?? 0;

        if (exchangeRatesCount != 0 && exchangeRatesCount != namesCount - 1) throw new InvalidOperationException(null);

        ExchangeRateCollection.Add(default(T), DefaultExchangeRate);

        if (exchangeRatesCount > 0)
        {
            SetExchangeRates(exchangeRateCollection, measureUnitType, defaultNames, exchangeRates!);
        }
    }

    private static void SetExchangeRates(IDictionary<Enum, decimal> exchangeRateCollection, Type measureUnitType, IEnumerable<string> defaultNames, decimal[] exchangeRates)
    {
        for (int i = 0; i < exchangeRates.Length; i++)
        {
            Enum measureUnit = (Enum)Enum.Parse(measureUnitType, defaultNames.ElementAt(i + 1));
            exchangeRateCollection.Add(measureUnit, exchangeRates[i]);
        }
    }

    private void SetCustomMeasurementValidatedParams(Enum measureUnit, decimal exchangeRate, string? customName) // Check!
    {
        if (exchangeRate <= 0)
        {
            throw ExchangeRateArgumentOutOfRangeException(exchangeRate);
        }

        if (IsValidMeasureUnit(measureUnit, exchangeRate))
        {
            if (TrySetCustomName(measureUnit, customName)) return;

            throw CustomNameArgumentOutOfRangeException(customName);
        }

        if (!IsCustomMeasureUnit(measureUnit))
        {
            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        if (!ExchangeRateCollection.TryAdd(measureUnit, exchangeRate))
        {
            throw ExchangeRateArgumentOutOfRangeException(exchangeRate);
        }

        if (TrySetCustomName(measureUnit, customName)) return;

        if (ExchangeRateCollection.Remove(measureUnit))
        {
            throw CustomNameArgumentOutOfRangeException(customName);
        }

        throw new InvalidOperationException(null);
    }

    private void SetCustomMeasurementValidatedParams(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName, out Enum measureUnit)
    {
        ValidateExchangeRate(exchangeRate);

        measureUnit = GetNextNotUsedCustomMeasureUnit(measureUnitTypeCode);
        ExchangeRateCollection.Add(measureUnit, exchangeRate);

        if (TrySetCustomName(measureUnit, customName)) return;

        if (ExchangeRateCollection.Remove(measureUnit))
        {
            throw CustomNameArgumentOutOfRangeException(customName);
        }

        throw new InvalidOperationException(null);
    }
    #endregion
}
