using System.Collections.Immutable;

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

        Enum measureUnit = GetNextNotUsedCustomMeasureUnit(measureUnitTypeCode);

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

        IEnumerable<Enum> getNotUsedCustomMeasureUnits(MeasureUnitTypeCode customMeasureUnitTypeCode)
        {
            Type measureUnitType = GetMeasureUnitType(customMeasureUnitTypeCode);
            Array measureUnits = Enum.GetValues(measureUnitType);

            return GetNotUsedCustomMeasureUnits<Enum>(measureUnits, customMeasureUnitTypeCode);
        }
        #endregion
    }

    public override IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
    {
        if (measurableFactory is IMeasurementFactory measurementFactory && measurable is IMeasurement measurement) return measurementFactory.Create(measurement);

        return base.GetMeasurable(measurableFactory, measurable);
    }

    public IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null)
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

    public IEnumerable<Enum> GetValidMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return ExchangeRateCollection.Keys;
    }

    public ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName = null)
    {
        ValidateExchangeRate(exchangeRate);

        return GetMeasurement(GetNextNotUsedCustomMeasureUnit(customMeasureUnitTypeCode), exchangeRate);
    }

    public IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        return MeasurementFactory.GetRateComponent(rate, rateComponentCode);
    }

    public void InitiateCustomExchangeRates(MeasureUnitTypeCode customMeasureUnitTypeCode, params decimal[] exchangeRates)
    {
        ValidateCustomMeasureUnitTypeCode(customMeasureUnitTypeCode);

        int count = exchangeRates?.Length ?? 0;

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(exchangeRates), count, null);

        Type measureUnitType = GetMeasureUnitType(customMeasureUnitTypeCode);
        string[] measureUnitNames = GetDefaultNames(customMeasureUnitTypeCode);

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

    public bool IsValidMeasureUnit(Enum measureUnit, decimal? exchangeRate = null)
    {
        if (!ExchangeRateCollection.ContainsKey(measureUnit)) return false;

        if (exchangeRate == null) return true;

        return ExchangeRateCollection[measureUnit] == exchangeRate.Value;
    }

    public decimal ProportionalTo(IMeasurement measurement)
    {
        Enum measureUnit = measurement?.GetMeasureUnit() ?? throw new ArgumentNullException(nameof(measurement));

        if (IsExchangeableTo(measureUnit)) return ExchangeRate / GetExchangeRate(measureUnit);

        throw new ArgumentOutOfRangeException(nameof(measurement), measureUnit, null);
    }

    public void RestoreConstantMeasureUnits()
    {
        ExchangeRateCollection = GetConstantExchangeRates(ExchangeRateCollection);
        CustomNameCollection.Clear();
    }

    public bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        ValidateExchangeRate(exchangeRate);

        if (!ExchangeRateCollection.TryAdd(measureUnit, exchangeRate)) return false;

        if (TryAddCustomName(measureUnit, customName)) return true;

        if (ExchangeRateCollection.Remove(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }

    public bool TryAddCustomName(Enum measureUnit, string? customName)
    {
        return customName == null
            || IsValidMeasureUnit(measureUnit)
            && IsValidCustomNameOrNull(customName)
            && CustomNameCollection.TryAdd(measureUnit, customName!);
    }

    public bool TryGetMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IMeasurement? measurement)
    {
        measurement = null;

        if (IsValidMeasureUnit(measureUnit, exchangeRate) && TryAddCustomName(measureUnit, customName))
        {
            measurement = GetMeasurement(measureUnit);
        }

        else if (TryAddCustomMeasureUnit(measureUnit, exchangeRate, customName))
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

    private decimal GetValidExchangeRate(Enum measureUnit, decimal? exchangeRate) // Check!
    {
        base.ValidateMeasureUnit(measureUnit);

        if (!IsValidMeasureUnit(measureUnit, exchangeRate))
        {
            ValidateExchangeRate(exchangeRate);
        }

        return exchangeRate!.Value;
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
