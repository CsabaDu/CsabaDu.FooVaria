using CsabaDu.FooVaria.Measurables.Statics;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Measurement : Measurable, IMeasurement
{
    #region Constants
    private const string DefaultCustomMeasureUnitName = "Default";
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

    #region Static properties
    private static IDictionary<object, string> CustomNameCollection { get; set; } = new SortedList<object, string>();
    #endregion
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

    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return ExchangeRates.GetConstantExchangeRateCollection();
    }

    public ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(measureUnit)) return null;

        if (!exchangeRate.IsValidExchangeRate()) return null;

        if (IsValidMeasureUnit(measureUnit))
        {
            if (!IsValidExchangeRate(exchangeRate, measureUnit)) return null;

            if (!TrySetCustomName(measureUnit, customName)) return null;
        }

        if (!TrySetCustomMeasureUnit(this, measureUnit, exchangeRate, customName)) return null;

        return GetFactory().Create(measureUnit, exchangeRate, customName);
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

    public string? GetCustomName()
    {
        return GetCustomName(GetMeasureUnit());
    }

    public string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public IDictionary<object, string> GetCustomNameCollection()
    {
        return CustomNameCollection;
    }

    public IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, MeasureUnitTypeCode);
    }

    public decimal GetExchangeRate(string name)
    {
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, nameof(name)));

        if (measureUnit != null)
        {
            decimal? exchangeRate = GetExchangeRateOrNull(measureUnit);

            if (exchangeRate != null) return exchangeRate.Value;
        }

        throw new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IDictionary<object, decimal> exchangeRateCollection = ExchangeRates.GetExchangeRateCollection();

        return GetMeasureUnitBasedCollection(exchangeRateCollection, measureUnitTypeCode);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(MeasureUnitTypeCode);
    }

    public decimal GetExchangeRate(Enum measureUnit)
    {
        decimal? exchangeRate = GetExchangeRateOrNull(NullChecked(measureUnit, nameof(measureUnit)));

        if (exchangeRate != null) return exchangeRate.Value;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return GetFactory().Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return GetFactory().Create(other);
    }

    public IMeasurement GetMeasurement(IBaseMeasure baseMeasure)
    {
        return NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
    }

    public IMeasurement GetMeasurement(string name)
    {
        return GetFactory().Create(name);
    }

    public Enum? GetMeasureUnit(string name)
    {
        return (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == NullChecked(name, nameof(name))).Value;
    }

    public IDictionary<string, object> GetMeasureUnitCollection()
    {
        return GetMeasureUnitCollection(null);
    }

    public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitCollection(measureUnitTypeCode as MeasureUnitTypeCode?);
    }

    public string GetName()
    {
        Enum measureUnit = GetMeasureUnit();

        return GetCustomName(measureUnit) ?? GetDefaultName(measureUnit);
    }

    public ICustomMeasurement GetCustomMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetFactory().Create(measureUnitTypeCode, exchangeRate, customName);
    }

    public string GetDefaultName(Enum? measureUnit = null) // TODO
    {
        return MeasureUnitTypes.GetDefaultName(measureUnit ?? GetMeasureUnit());
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        return GetNotUsedCustomMeasureUnits(null);
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetNotUsedCustomMeasureUnits(measureUnitTypeCode as MeasureUnitTypeCode?);
    }

    public IEnumerable<object> GetValidMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return ExchangeRates.GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitTypeCode.GetMeasureUnitType()));
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

    public bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IEnumerable<string> defaultNames = MeasureUnitTypes.GetDefaultNames(measureUnitTypeCode);

        return defaultNames?.First() == DefaultCustomMeasureUnitName;
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

        return IsValidMeasureUnit(context) && HasMeasureUnitTypeCode(MeasureUnitTypeCode, context!);
    }

    public bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return exchangeRate == GetExchangeRate(measureUnit);
    }

    public bool IsValidMeasureUnit(Enum? measureUnit)
    {
        return ExchangeRates.IsValidMeasureUnit(measureUnit);
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

    public void SetOrReplaceCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

        if (!IsCustomMeasureUnit(NullChecked(measureUnit, nameof(measureUnit)))) throw InvalidMeasureUnitEnumArgumentException(measureUnit);

        if (!exchangeRate.IsValidExchangeRate()) throw ExchangeRateArgumentOutOfRangeException(exchangeRate);

        if (!IsValidCustomNameParam(customName)) throw CustomNameArgumentOutOfRangeException(customName);

        if (CustomNameCollection.Remove(measureUnit, out string? removedCustomName))
        {
            decimal removedExchangeRate = GetExchangeRate(measureUnit);

            if (ExchangeRates.RemoveExchangeRate(measureUnit))
            {
                if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

                ExchangeRates.SetExchangeRate(measureUnit, removedExchangeRate);
            }

            SetCustomName(measureUnit, removedCustomName);
        }

        throw new InvalidOperationException(null);
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
    public bool TrySetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return TryGetCustomMeasurement(measureUnit, exchangeRate, customName, out ICustomMeasurement? customMeasurement) && customMeasurement != null;
    }

    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

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

        return TrySetCustomMeasureUnit(this, measureUnit, exchangeRate, customName);
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

    public void ValidateExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

        throw ExchangeRateArgumentOutOfRangeException(exchangeRate);
    }

    public void ValidateExchangeRate(decimal exchangeRate)
    {
        ValidateExchangeRate(exchangeRate, GetMeasureUnit());
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other
            && Equals(other);
    }

    public override IMeasurement GetDefault()
    {
        return GetFactory().CreateDefault(MeasureUnitTypeCode);
    }

    public override IMeasurementFactory GetFactory()
    {
        return (IMeasurementFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
    }

    public override Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }
    #endregion
    #endregion

    #region Private methods
    private IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode? measureUnitTypeCode)
    {
        IEnumerable<object> validMeasureUnits;
        IDictionary<object, string> customNameCollection;

        if (measureUnitTypeCode.HasValue)
        {
            validMeasureUnits = GetValidMeasureUnits(measureUnitTypeCode.Value);
            customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode.Value);
        }
        else
        {
            validMeasureUnits = ExchangeRates.GetValidMeasureUnits();
            customNameCollection = CustomNameCollection;
        }

        IDictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary(x => MeasureUnitTypes.GetDefaultName((Enum)x), x => x);

        foreach (KeyValuePair<object, string> item in customNameCollection)
        {
            measureUnitCollection.Add(item.Value, item.Key);
        }

        return measureUnitCollection;
    }

    private IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode)
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
            if (measureUnitTypeCode.HasValue) return getCustomMeasureUnitTypeCodesShortList();

            return GetCustomMeasureUnitTypeCodes();
        }

        IEnumerable<MeasureUnitTypeCode> getCustomMeasureUnitTypeCodesShortList()
        {
            yield return measureUnitTypeCode.Value;
        }
        #endregion
    }

    #region Static methods
    private static decimal? GetExchangeRateOrNull(Enum? measureUnit)
    {
        if (measureUnit == null) return null;

        decimal exchangeRate = ExchangeRates.GetExchangeRateCollection().FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate == default) return null;

        return exchangeRate;
    }

    private static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
    {
        return measureUnitBasedCollection
            .Where(x => x.Key.GetType().Equals(measureUnitTypeCode.GetMeasureUnitType()))
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
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

    private static bool TrySetCustomMeasureUnit(IMeasurement measurement, Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!ExchangeRates.TrySetExchangeRate(measureUnit, exchangeRate)) return false;

        if (measurement.TrySetCustomName(measureUnit, customName)) return true;

        if (ExchangeRates.RemoveExchangeRate(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }
    #endregion
    #endregion
}
