namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class MeasurementBase : Measurable, IMeasurementBase
{
    #region Constructors
    #region Static constructor
    static MeasurementBase()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
    }
    #endregion

    private protected MeasurementBase(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }
    #endregion

    #region Properties
    #region Abstract properties
    public abstract decimal ExchangeRate { get; init; }
    #endregion

    #region Static properties
    public static IDictionary<object, decimal> ExchangeRateCollection { get; protected set; }
    public static IDictionary<object, decimal> ConstantExchangeRateCollection { get; }
    #endregion
    #endregion

    #region Public methods
    public int CompareTo(IMeasurementBase? other)
    {
        if (other == null) return 1;

        other.ValidateMeasureUnitTypeCode(MeasureUnitTypeCode, nameof(other));

        return ExchangeRate.CompareTo(other.ExchangeRate);
    }

    public bool Equals(IMeasurementBase? other)
    {
        return base.Equals(other)
            && other.ExchangeRate == ExchangeRate;
    }

    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return GetConstantExchangeRateCollection(MeasureUnitTypeCode);
    }

    public IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitTypeCode);
    }

    public decimal GetExchangeRate()
    {
        return ExchangeRate;
    }

    public decimal GetExchangeRate(Enum measureUnit)
    {
        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate != default) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitTypeCode);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(MeasureUnitTypeCode);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

        return IsValidMeasureUnit(context) && MeasureUnitTypes.HasMeasureUnitTypeCode(MeasureUnitTypeCode, context!);
    }

    public bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return exchangeRate == GetExchangeRate(measureUnit);
    }

    public decimal ProportionalTo(IMeasurementBase other)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(other, nameof(other)).MeasureUnitTypeCode;

        if (HasMeasureUnitTypeCode(measureUnitTypeCode)) return ExchangeRate / other.ExchangeRate;

        throw new ArgumentOutOfRangeException(nameof(other), measureUnitTypeCode, null);
    }

    public void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit)
    {
        if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

        throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
    }

    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        ValidateExchangeRate(exchangeRate, paramName, GetMeasureUnit());
    }

    #region Override methods
    #region Sealed methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IMeasurementBase other
            && Equals(other);
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        if (ExchangeRateCollection.ContainsKey(measureUnit)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract decimal GetExchangeRate(string name);
    public abstract Enum GetMeasureUnit();
    public abstract string GetName();
    public abstract void RestoreConstantExchangeRates();
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
    {
        MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(measureUnitTypeCode));

        return measureUnitBasedCollection
            .Where(x => x.Key.GetType().Name.Equals(Enum.GetName(measureUnitTypeCode)))
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static IDictionary<object, decimal> InitConstantExchangeRateCollection()
    {
        return initConstantExchangeRates<AreaUnit>(100, 10000, 1000000)
            .Union(initConstantExchangeRates<Currency>())
            .Union(initConstantExchangeRates<DistanceUnit>(1000))
            .Union(initConstantExchangeRates<ExtentUnit>(10, 100, 1000))
            .Union(initConstantExchangeRates<Pieces>())
            .Union(initConstantExchangeRates<TimePeriodUnit>(60, 1440, 10080, 14400))
            .Union(initConstantExchangeRates<VolumeUnit>(1000, 1000000, 1000000000))
            .Union(initConstantExchangeRates<WeightUnit>(1000, 1000000))
            .ToDictionary(x => x.Key, x => x.Value);

        #region Local methods
        static IEnumerable<KeyValuePair<object, decimal>> initConstantExchangeRates<T>(params decimal[] exchangeRates) where T : struct, Enum
        {
            yield return getMeasureUnitExchangeRatePair(default(T), decimal.One);

            int exchangeRateCount = exchangeRates?.Length ?? 0;

            if (exchangeRateCount > 0)
            {
                T[] measureUnits = Enum.GetValues<T>();
                int measureUnitCount = measureUnits.Length;

                if (measureUnitCount == 0 || measureUnitCount != exchangeRateCount + 1) throw new InvalidOperationException(null);

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