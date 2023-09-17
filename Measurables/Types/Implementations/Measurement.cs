using CsabaDu.FooVaria.Measurables.Statics;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Measurement : Measurable, IMeasurement
{
    #region Constants
    private const string DefaultCustomMeasureUnitName = "Default";
    #endregion

    #region Static properties
    private static IDictionary<Enum, string> CustomNameCollection { get; set; } = new SortedList<Enum, string>();
    #endregion

    #region Constructors
    internal Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
        ExchangeRate = GetExchangeRate(measureUnit);
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
        return other?.MeasureUnitTypeCode.Equals(MeasureUnitTypeCode) == true
            && other.ExchangeRate == ExchangeRate;
    }

    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other
            && Equals(other);
    }

    public IDictionary<Enum, decimal> GetConstantExchangeRateCollection()
    {
        return ExchangeRates.GetConstantExchangeRateCollection();
    }

    public ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(measureUnit)) return null;

        if (!(IsValidMeasureUnit(measureUnit, exchangeRate) && TrySetCustomName(measureUnit, customName))) return null;

        if (!TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return null;

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
        IDictionary<Enum, decimal> exchangeRateCollection = ExchangeRates.GetExchangeRateCollection();

        if (measureUnitTypeCode == null) return exchangeRateCollection;

        return GetMeasureUnitBasedCollection(exchangeRateCollection, measureUnitTypeCode.Value);
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
        return GetMeasurementFactory().Create(name);
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
        IDictionary<string, Enum> measureUnitCollection = getMeasureUnitCollection();

        foreach (Enum measureUnit in GetValidMeasureUnits(measureUnitTypeCode))
        {
            string measureUnitTypeName = measureUnit.GetType().Name;
            string defaultName = GetDefaultName(measureUnit);
            string measureUnitName = measureUnitTypeName + "." + defaultName;
            measureUnitCollection.Add(measureUnitName, measureUnit);
        }

        return measureUnitCollection;

        #region Local methods
        IDictionary<string, Enum> getMeasureUnitCollection()
        {
            if (measureUnitTypeCode == null) return CustomNameCollection.ToDictionary(x => x.Value, x => x.Key);

            return CustomNameCollection
                .Where(x => x.GetType() == GetMeasureUnitType(measureUnitTypeCode))
                .ToDictionary(x => x.Value, x => x.Key);
        }
        #endregion
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

    public string GetDefaultName(Enum? measureUnit = null) // TODO
    {
        return MeasureUnitTypes.GetDefaultName(measureUnit ?? GetMeasureUnit());
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
                if (!ExchangeRates.GetExchangeRateCollection().ContainsKey(item))
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
        return GetValidMeasureUnits().Where(x => x.GetType() == GetMeasureUnitType(measureUnitTypeCode));
    }

    public void InitializeCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        int count = NullChecked(customExchangeRateCollection, nameof(customExchangeRateCollection)).Count;

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(customExchangeRateCollection), count, null);

        for (int i = 0; i < count; i++)
        {
            string? customName = customExchangeRateCollection.Keys.ElementAt(i);
            decimal exchangeRate = customExchangeRateCollection!.Values.ElementAt(i);
            Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).ElementAt(i);

            if (customName == null
                || customName == CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value
                || IsValidCustomNameParam(customName)
                && CustomNameCollection.TryAdd(measureUnit, customName))
            {
                ExchangeRates.SetExchangeRate(measureUnit, exchangeRate);
            }
        }
   }

    public bool IsCustomMeasureUnit(Enum measureUnit)
    {
        if (measureUnit == null || !IsDefinedMeasureUnit(measureUnit)) return false;

        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

        return IsCustomMeasureUnitTypeCode(measureUnitTypeCode);
    }

    public bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        IEnumerable<string> defaultNames = GetDefaultNames(measureUnitTypeCode ?? MeasureUnitTypeCode);

        return defaultNames?.First() == DefaultCustomMeasureUnitName;
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

        return IsExchangeableTo(context, MeasureUnitTypeCode);
    }

    public bool IsValidMeasureUnit(Enum? measureUnit, decimal? exchangeRate = null)
    {
        if (measureUnit == null) return false;

        IDictionary<Enum, decimal> exchangeRateCollection = ExchangeRates.GetExchangeRateCollection();

        if (!exchangeRateCollection.ContainsKey(measureUnit)) return false;

        if (exchangeRate == null) return true;

        return exchangeRateCollection[measureUnit] == exchangeRate.Value;
    }

    public decimal ProportionalTo(IMeasurement measurement)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurement, nameof(measurement)).MeasureUnitTypeCode;

        if (IsExchangeableTo(measureUnitTypeCode)) return ExchangeRate / measurement.ExchangeRate;

        throw new ArgumentOutOfRangeException(nameof(measurement), measureUnitTypeCode, null);
    }

    public void RestoreConstantExchangeRates()
    {
        CustomNameCollection.Clear();
        ExchangeRates.RestoreConstantExchangeRates();
    }

    public void SetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        SetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (measureUnit is MeasureUnitTypeCode measureUnitTypeCode)
        {
            SetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);
        }

        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

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

    public bool TrySetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        if (!IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return false;

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        return TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (measureUnit is MeasureUnitTypeCode measureUnitTypeCode)
        {
            return TrySetCustomMeasureUnit(customName, measureUnitTypeCode, exchangeRate);
        }

        if (!IsCustomMeasureUnit(measureUnit)) return false;

        if (!exchangeRate.IsValidExchangeRate()) return false;

        if (!ExchangeRates.TrySetExchangeRate(measureUnit, exchangeRate)) return false;

        if (TrySetCustomName(measureUnit, customName)) return true;

        if (ExchangeRates.RemoveExchangeRate(measureUnit)) return false;

        throw new InvalidOperationException(null);
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

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    public void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(customName)) return;
        
        throw CustomNameArgumentOutOfRangeException(customName);
    }

    public void ValidateExchangeRate(decimal exchangeRate, Enum? measureUnit = null)
    {
        if (exchangeRate > 0)
        {
            if (measureUnit == null) return;

            if (exchangeRate == GetExchangeRate(measureUnit)) return;
        }

        throw ExchangeRateArgumentOutOfRangeException(exchangeRate);
    }

    public override void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        measureUnitTypeCode ??= MeasureUnitTypeCode;
        if (IsExchangeableTo(measureUnit, measureUnitTypeCode.Value)) return;

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

        decimal exchangeRate = ExchangeRates.GetExchangeRateCollection().FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate == default) return null;

        return exchangeRate;
    }

    private IDictionary<Enum, T> GetMeasureUnitBasedCollection<T>(IDictionary<Enum, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
    {
        return measureUnitBasedCollection
            .Where(x => x.Key.GetType() == GetMeasureUnitType(measureUnitTypeCode))
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    private bool IsExchangeableTo(Enum? measureUnit, MeasureUnitTypeCode measureUnitTypeCode) // 1
    {
        return IsValidMeasureUnit(measureUnit) && HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);
    }

    private static bool IsValidCustomNameParam(string? customName)
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
    #endregion
}
