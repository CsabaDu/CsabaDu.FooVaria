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
        ExchangeRateCollection = new SortedList<Enum, decimal>();

        InitiateConstantExchangeRates(ExchangeRateCollection);

        CustomNameCollection = new SortedList<Enum, string>();
    }

    internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit, decimal? exchangeRate, string? customName) : base(measurementFactory, measureUnit)
    {
        ExchangeRate = GetValidExchangeRate(measureUnit, exchangeRate);

        AddCustomName(measureUnit, customName);
        ExchangeRateCollection.Add(measureUnit, exchangeRate!.Value);

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
    public string? CustomName => CustomNameCollection[GetMeasureUnit()];

    private IMeasurementFactory MeasurementFactory => (IMeasurementFactory)MeasurableFactory;
    private static IDictionary<Enum, decimal> ExchangeRateCollection { get; }
    private static IDictionary<Enum, string> CustomNameCollection { get; }
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
            && CompareTo(other) == 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other && Equals(other);
    }

    public IDictionary<Enum, decimal> GetConstantExchangeRateCollection()
    {
        var constantExchangeRateCollection = new SortedList<Enum, decimal>();

        InitiateConstantExchangeRates(constantExchangeRateCollection);

        return constantExchangeRateCollection;
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

    public override Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
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
        string[] measureUnitNames = GetMeasureUnitNames(measureUnitTypeCode);

        AddExchangeRates(ExchangeRateCollection, measureUnitType, measureUnitNames, exchangeRates!);
    }

    public bool IsCustomMeasureUnit(Enum measureUnit)
    {
        if (measureUnit == null || !IsDefinedMeasureUnit(measureUnit)) return false;

        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
        string[] measureUnitNames = GetMeasureUnitNames(measureUnitTypeCode);

        return measureUnitNames.First() == DefaultCustomMeasureUnitName;
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
        ExchangeRateCollection.Clear();
        InitiateConstantExchangeRates(ExchangeRateCollection);
    }

    public bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        ValidateExchangeRate(exchangeRate);

        return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate)
            && TryAddCustomName(measureUnit, customName);

    }

    public bool TryAddCustomName(Enum measureUnit, string? customName)
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

    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null && TryAddCustomName(measureUnit, customName);
    }

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (GetMeasureUnitNames(measureUnitTypeCode).First() == DefaultCustomMeasureUnitName) return;

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
    private static void AddConstantExchangeRates<T>(IDictionary<Enum, decimal> exchangeRateCollection, params decimal[] exchangeRates) where T : struct, Enum
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
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        IDictionary<Enum, decimal> exchangeRateCollection = GetExchangeRateCollection(measureUnitTypeCode);
        int count = exchangeRateCollection.Count;

        for (int i = 0; i < count; i++)
        {
            Enum measureUnit = exchangeRateCollection.ElementAt(i).Key;
            int measureUnitValue = (int)(object)measureUnit;

            if (measureUnitValue > i) return measureUnit;
        }

        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);
        return (Enum)Enum.ToObject(measureUnitType, count);
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

    private static void InitiateConstantExchangeRates(IDictionary<Enum, decimal> exchangeRateCollection)
    {
        AddConstantExchangeRates<AreaUnit>(exchangeRateCollection, 100, 10000, 1000000);
        AddConstantExchangeRates<Currency>(exchangeRateCollection);
        AddConstantExchangeRates<DistanceUnit>(exchangeRateCollection, 1000);
        AddConstantExchangeRates<Pieces>(exchangeRateCollection);
        AddConstantExchangeRates<ExtentUnit>(exchangeRateCollection, 10, 100, 1000);
        AddConstantExchangeRates<TimePeriodUnit>(exchangeRateCollection, 60, 1440, 10080, 14400);
        AddConstantExchangeRates<VolumeUnit>(exchangeRateCollection, 1000, 1000000, 1000000000);
        AddConstantExchangeRates<WeightUnit>(exchangeRateCollection, 1000, 1000000);
    }

    private bool IsExchangeableTo(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
    {
        return IsValidMeasureUnit(measureUnit) && HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);
    }

    private static bool IsValidCustomNameOrNull(string? customName)
    {
        return customName == null
            || !CustomNameCollection.Values.Contains(customName)
            && customName != string.Empty
            && !string.IsNullOrWhiteSpace(customName);
    }
    #endregion
}
