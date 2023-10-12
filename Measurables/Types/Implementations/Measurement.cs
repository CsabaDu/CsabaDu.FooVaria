namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Measurement : BaseMeasurement, IMeasurement
{
    #region Constants
    private const string DefaultCustomMeasureUnitName = "Default";
    #endregion

    #region Constructors
    public Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
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
        return MeasureUnitTypeCode.Equals(other?.MeasureUnitTypeCode)
            && other.ExchangeRate == ExchangeRate;
    }

    public ICustomMeasurement GetCustomMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetFactory().Create(measureUnitTypeCode, exchangeRate, customName);
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

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        Type customMeasureUnitType = GetMeasureUnitType(measureUnitTypeCode);
        Array customMeasureUnits = Enum.GetValues(customMeasureUnitType);

        foreach (Enum item in customMeasureUnits)
        {
            if (!GetExchangeRateCollection().ContainsKey(item))
            {
                yield return item;
            }
        }
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        IEnumerable<MeasureUnitTypeCode> customMeasureUnitTypeCodes = GetCustomMeasureUnitTypeCodes();
        IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(customMeasureUnitTypeCodes.First());

        for (int i = 1; i < customMeasureUnitTypeCodes.Count(); i++)
        {
            IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(customMeasureUnitTypeCodes.ElementAt(i));
            notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        }

        return notUsedCustomMeasureUnits;
    }

    public IMeasurement? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        return NullChecked(rate, nameof(rate)).GetRateComponent(rateComponentCode)?.Measurement;
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
                SetExchangeRate(measureUnit, exchangeRate);
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

    public decimal ProportionalTo(IMeasurement measurement)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurement, nameof(measurement)).MeasureUnitTypeCode;

        if (IsExchangeableTo(measureUnitTypeCode)) return ExchangeRate / measurement.ExchangeRate;

        throw new ArgumentOutOfRangeException(nameof(measurement), measureUnitTypeCode, null);
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

    public void SetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        SetCustomMeasureUnit(measureUnit, exchangeRate, customName);
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

            if (RemoveExchangeRate(measureUnit))
            {
                if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

                SetExchangeRate(measureUnit, removedExchangeRate);
            }

            SetCustomName(measureUnit, removedCustomName);
        }

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

    public bool TrySetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        if (!IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return false;

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        return TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other
            && Equals(other);
    }

    public override IMeasurement GetDefault()
    {
        return GetDefault(this);
    }

    public override IMeasurementFactory GetFactory()
    {
        return (IMeasurementFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
    }

    public override IMeasurement GetMeasurable(IMeasurable other)
    {
        return (IMeasurement)GetFactory().Create(other);
    }

    public override Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static bool RemoveExchangeRate(Enum measureUnit)
    {
        return ExchangeRateCollection.Remove(measureUnit);
    }

    private static bool TrySetCustomMeasureUnit(IMeasurement measurement, Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!TrySetExchangeRate(measureUnit, exchangeRate)) return false;

        if (measurement.TrySetCustomName(measureUnit, customName)) return true;

        if (RemoveExchangeRate(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }

    private static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        exchangeRate.ValidateExchangeRate();

        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit) && tryAddExchangeRate();

        #region Local methods
        static bool isDefinedExchangeRate(decimal exchangeRate, Enum measureUnit)
        {
            return ExchangeRateCollection.ContainsKey(measureUnit) && exchangeRate == ExchangeRateCollection[measureUnit];
        }

        bool tryAddExchangeRate()
        {
            return isDefinedExchangeRate(exchangeRate, measureUnit) || ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
        }
        #endregion
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

    private static void SetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (TrySetExchangeRate(measureUnit, exchangeRate)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }
    #endregion
    #endregion
}
