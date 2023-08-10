using System.Collections.Immutable;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal class Measurement : Measurable, IMeasurement
{
    #region Constants
    public const decimal DefaultExchangeRate = decimal.One;
    private const string DefaultCustomMeasureUnitName = "Default";
    #endregion

    #region Constructors
    static Measurement()
    {
        ExchangeRateCollection = GetConstantExchangeRates(new SortedList<Enum, decimal>());
        CustomNameCollection = new SortedList<Enum, string>();
    }

    internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit, decimal? exchangeRate, string? customName) : base(measurementFactory, measureUnit)
    {
        ExchangeRate = GetValidExchangeRate(measureUnit, exchangeRate);

        AddCustomName(measureUnit, customName);
        _ = ExchangeRateCollection.TryAdd(measureUnit, ExchangeRate);

        MeasureUnit = measureUnit;
    }

    internal Measurement(IMeasurementFactory measurementFactory, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName) : base(measurementFactory, measureUnitTypeCode)
    {
        ValidateExchangeRate(exchangeRate);

        Enum measureUnit = GetNextInvalidCustomMeasureUnit(measureUnitTypeCode);

        ExchangeRate = exchangeRate;

        AddCustomName(measureUnit, customName);
        ExchangeRateCollection.Add(measureUnit, exchangeRate);

        MeasureUnit = measureUnit;
    }
    #endregion

    #region Properties
    public object MeasureUnit { get; init; }
    public decimal ExchangeRate { get; init; }

    private IMeasurementFactory MeasurementFactory => (IMeasurementFactory)MeasurableFactory;
    private static IDictionary<Enum, decimal> ExchangeRateCollection { get; }
    private static IDictionary<Enum, string> CustomNameCollection { get; }
    #endregion

    #region Public methods
    public void AddOrReplaceCustomName(string customName)
    {
        ValidateCustomName(customName);

        Enum measureUnit = GetMeasureUnit();

        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

        if (CustomNameCollection.Remove(measureUnit) && CustomNameCollection.TryAdd(measureUnit, customName)) return;

        throw new InvalidOperationException(null);
    }

    public int CompareTo(IMeasurement? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitTypeCode(other.MeasureUnitTypeCode);

        return ExchangeRate.CompareTo(other.ExchangeRate);
    }

    public bool Equals(IMeasurement? other)
    {
        return other?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) == true
            && CompareTo(other) == 0;
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
            Enum defaultMeasureUnit = GetDefaultMeasureUnit(item);

            if (GetDefaultName(defaultMeasureUnit) == DefaultCustomMeasureUnitName)
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
        if (measureUnitTypeCode is MeasureUnitTypeCode notNullMeasureUnitTypeCode) return GetMeasureUnitBasedCollection(CustomNameCollection, notNullMeasureUnitTypeCode);

        return new SortedList<Enum, string>(CustomNameCollection);
    }

    public IDictionary<Enum, decimal> GetExchangeRateCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode is MeasureUnitTypeCode notNullMeasureUnitTypeCode) return GetMeasureUnitBasedCollection(ExchangeRateCollection, notNullMeasureUnitTypeCode);

        return new SortedList<Enum, decimal>(ExchangeRateCollection);
    }

    public decimal GetExchangeRate(Enum measureUnit)
    {
        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key == measureUnit).Value;

        if (exchangeRate > 0) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
    }

    public IEnumerable<T> GetInvalidCustomMeasureUnits<T>() where T : struct, Enum
    {
        T[] measureUnits = Enum.GetValues<T>();
        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnits.First());

        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        return GetInvalidCustomMeasureUnits<T>(measureUnits, measureUnitTypeCode);
    }

    public IEnumerable<Enum> GetInvalidCustomMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        IEnumerable<MeasureUnitTypeCode> customMeasureUnitTypeCodes;

        if (measureUnitTypeCode == null)
        {
            customMeasureUnitTypeCodes = GetCustomMeasureUnitTypeCodes();
        }
        else
        {
            MeasureUnitTypeCode customMeasureUnitTypeCode = measureUnitTypeCode.Value;

            ValidateCustomMeasureUnitTypeCode(customMeasureUnitTypeCode);

            customMeasureUnitTypeCodes = new List<MeasureUnitTypeCode>()
            {
                customMeasureUnitTypeCode
            };
        }

        IEnumerable<Enum> invalidCustomMeasureUnits = new List<Enum>();

        foreach (MeasureUnitTypeCode customMeasureUnitTypeCode in customMeasureUnitTypeCodes)
        {
            invalidCustomMeasureUnits = invalidCustomMeasureUnits.Union(getInvalidCustomMeasureUnits(customMeasureUnitTypeCode));
        }

        return invalidCustomMeasureUnits;

        #region Local methods
        IEnumerable<Enum> getInvalidCustomMeasureUnits(MeasureUnitTypeCode customMeasureUnitTypeCode)
        {
            Type measureUnitType = GetMeasureUnitType(customMeasureUnitTypeCode);
            Array measureUnits = Enum.GetValues(measureUnitType);

            return GetInvalidCustomMeasureUnits<Enum>(measureUnits, customMeasureUnitTypeCode);
        }
        #endregion
    }

    public override IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
    {
        if (measurableFactory is IMeasurementFactory measurementFactory && measurable is IMeasurement measurement) return measurementFactory.Create(measurement);

        return base.GetMeasurable(measurableFactory, measurable);
    }

    public IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null, string? customName = null)
    {
        if (exchangeRate == null) return MeasurementFactory.Create(measureUnit);

        return MeasurementFactory.Create(measureUnit, exchangeRate!.Value);
    }

    public IMeasurement GetMeasurement(IMeasurement? other = null)
    {
        return MeasurementFactory.Create(other ??  this);
    }

    public IMeasurement GetMeasurement(IBaseMeasure baseMeasure)
    {
        return baseMeasure?.Measurement ?? throw new ArgumentNullException(nameof(baseMeasure));
    }

    public IMeasurement? GetMeasurement(string measureUnitName)
    {
        Enum? measureUnit = GetMeasureUnit(this, measureUnitName);

        return measureUnit == null ? null : GetMeasurement(measureUnit);
    }

    public override Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public string GetName(Enum? measureUnit = null)
    {
        measureUnit ??= GetMeasureUnit();

        return GetCustomName(measureUnit) ?? GetDefaultName(measureUnit);
    }

    public Enum? GetMeasureUnit(string customName)
    {
        if (customName == null) return null;

        return CustomNameCollection.FirstOrDefault(x => x.Value == customName).Key;
    }

    public ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null)
    {
        ValidateExchangeRate(exchangeRate);

        return GetMeasurement(GetNextInvalidCustomMeasureUnit(measureUnitTypeCode), exchangeRate);
    }

    public IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        return MeasurementFactory.GetRateComponent(rate, rateComponentCode);
    }

    public void InitiateCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, params decimal[] exchangeRates)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        int count = exchangeRates?.Length ?? 0;

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(exchangeRates), count, null);

        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);
        string[] measureUnitNames = GetDefaultNames(measureUnitTypeCode);

        AddExchangeRates(ExchangeRateCollection, measureUnitType, measureUnitNames, exchangeRates!);
    }

    public bool IsCustomMeasureUnit(Enum measureUnit)
    {
        if (measureUnit == null || !IsDefinedMeasureUnit(measureUnit)) return false;

        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
        string[] measureUnitNames = GetDefaultNames(measureUnitTypeCode);

        return measureUnitNames.First() == DefaultCustomMeasureUnitName;
    }

    public bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode == null)
        {
            measureUnitTypeCode = MeasureUnitTypeCode;
        }
        else if (!Enum.IsDefined(typeof(MeasureUnitTypeCode), measureUnitTypeCode))
        {
            return false;
        }

        return GetDefaultNames(measureUnitTypeCode).First() == DefaultCustomMeasureUnitName;
    }

    public bool IsExchangeableTo(Enum measureUnit)
    {
        return IsExchangeableTo(measureUnit, MeasureUnitTypeCode);
    }

    public bool IsValidMeasureUnit(Enum measureUnit)
    {
        return ExchangeRateCollection.ContainsKey(measureUnit);
    }

    public decimal ProportionalTo(IMeasurement measurement)
    {
        Enum measureUnit = measurement?.GetMeasureUnit() ?? throw new ArgumentNullException(nameof(measurement));

        if (IsExchangeableTo(measureUnit)) return ExchangeRate / GetExchangeRate(measureUnit);

        throw new ArgumentOutOfRangeException(nameof(measurement), measureUnit, null);
    }

    public void RestoreConstantMeasureUnits()
    {
        IDictionary<Enum, decimal> constantExchangeRateCollection = GetConstantExchangeRates(new SortedList<Enum, decimal>());

        foreach (KeyValuePair<Enum, decimal> item in ExchangeRateCollection)
        {
            if (!constantExchangeRateCollection.Contains(item))
            {
                _ = ExchangeRateCollection.Remove(item);
            }
        }
    }

    public bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        ValidateExchangeRate(exchangeRate);

        return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate)
            && TryAddCustomName(measureUnit, customName);
    }

    public bool TryAddCustomName(Enum measureUnit, string? customName) // Check!
    {
        return customName == null
            || IsValidMeasureUnit(measureUnit)
            && IsValidCustomNameOrNull(customName)
            && CustomNameCollection.TryAdd(measureUnit, customName!);
    }

    public bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement)
    {
        customMeasurement = null;

        if (!IsCustomMeasureUnit(measureUnit)) return false;

        if (IsValidMeasureUnit(measureUnit) && GetExchangeRate(measureUnit) == exchangeRate)
        {
            return tryGetCustomMeasurement(out customMeasurement);
        }

        if (TryAddCustomMeasureUnit(measureUnit, exchangeRate))
        {
            return tryGetCustomMeasurement(out customMeasurement);
        }

        return false;

        #region Local methods
        bool tryGetCustomMeasurement(out ICustomMeasurement? customMeasurement)
        {
            customMeasurement = null;

            if (TryAddCustomName(measureUnit, customName))
            {
                customMeasurement = GetMeasurement(measureUnit);
            }

            return customMeasurement != null;
        }
        #endregion
    }

    public bool TryGetMeasureUnit(string customName, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(customName);

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (IsCustomMeasureUnitTypeCode(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    public void ValidateCustomName(string? customName)
    {
        _ = NullChecked(customName, nameof(customName));

        if (!IsValidCustomNameOrNull(customName)) throw new ArgumentOutOfRangeException(nameof(customName), customName, null);
    }

    public void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null)
    {
        _ = NullChecked(exchangeRate, nameof(exchangeRate));

        if (exchangeRate > 0)
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
    private static void AddConstantExchangeRates<T>(ref IDictionary<Enum, decimal> exchangeRateCollection, params decimal[] exchangeRates) where T : struct, Enum
    {
        Type measureUnitType = typeof(T);
        string[] measureUnitNames = Enum.GetNames(measureUnitType);
        int namesCount = measureUnitNames.Length;
        int exchangeRatesCount = exchangeRates?.Length ?? 0;

        if (exchangeRatesCount != 0 && exchangeRatesCount != namesCount - 1) throw new InvalidOperationException(null);

        ExchangeRateCollection.Add(default(T), DefaultExchangeRate);

        if (exchangeRatesCount > 0)
        {
            AddExchangeRates(exchangeRateCollection, measureUnitType, measureUnitNames, exchangeRates!);
        }
    }

    private void AddCustomName(Enum measureUnit, string? customName)
    {
        if (TryAddCustomName(measureUnit, customName)) return;

        throw new ArgumentOutOfRangeException(nameof(customName), customName, null);
    }

    private static void AddExchangeRates(IDictionary<Enum, decimal> exchangeRateCollection, Type measureUnitType, string[] measureUnitNames, decimal[] exchangeRates)
    {
        for (int i = 0; i < exchangeRates.Length; i++)
        {
            Enum measureUnit = (Enum)Enum.Parse(measureUnitType, measureUnitNames.ElementAt(i + 1));
            exchangeRateCollection.Add(measureUnit, exchangeRates[i]);
        }
    }

    private IEnumerable<T> GetInvalidCustomMeasureUnits<T>(Array measureUnits, MeasureUnitTypeCode measureUnitTypeCode) where T : Enum
    {
        foreach (T measureUnit in measureUnits)
        {
            foreach (string item in GetDefaultNames(measureUnitTypeCode))
            {
                string defaultName = GetDefaultName(measureUnit);

                if (defaultName.ToLower() != item.ToLower())
                {
                    yield return measureUnit;
                }
            }
        }
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

    private Enum GetNextInvalidCustomMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetInvalidCustomMeasureUnits(measureUnitTypeCode).First();
    }

    private decimal GetValidExchangeRate(Enum measureUnit, decimal? exchangeRate)
    {
        if (IsValidMeasureUnit(measureUnit))
        {
            exchangeRate ??= GetExchangeRate(measureUnit);

            ValidateExchangeRate(exchangeRate, measureUnit);
        }
        else
        {
            ValidateExchangeRate(exchangeRate);
        }

        return (decimal)exchangeRate!;
    }

    private static IDictionary<Enum, decimal> GetConstantExchangeRates(IDictionary<Enum, decimal> exchangeRateCollection)
    {
        exchangeRateCollection.Clear();

        AddConstantExchangeRates<AreaUnit>(ref exchangeRateCollection, 100, 10000, 1000000);
        AddConstantExchangeRates<Currency>(ref exchangeRateCollection);
        AddConstantExchangeRates<DistanceUnit>(ref exchangeRateCollection, 1000);
        AddConstantExchangeRates<Pieces>(ref exchangeRateCollection);
        AddConstantExchangeRates<ExtentUnit>(ref exchangeRateCollection, 10, 100, 1000);
        AddConstantExchangeRates<TimePeriodUnit>(ref exchangeRateCollection, 60, 1440, 10080, 14400);
        AddConstantExchangeRates<VolumeUnit>(ref exchangeRateCollection, 1000, 1000000, 1000000000);
        AddConstantExchangeRates<WeightUnit>(ref exchangeRateCollection, 1000, 1000000);

        return exchangeRateCollection;
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
            && !CustomNameCollection.Values.Contains(customName)
            && !GetDefaultNames().Contains(customName);
    }
    #endregion
}
