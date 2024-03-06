namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;

public abstract class BaseMeasurement : Measurable, IBaseMeasurement
{
    #region Structs
    public readonly struct MeasurementElements(Enum context, string paramName)
    {
        public MeasureUnitElements MeasureUnitElements => new(context, paramName);
        public decimal ExchangeRate => GetExchangeRate(context, paramName);

        public Enum GetMeasureUnit()
        {
            return MeasureUnitElements.MeasureUnit;
        }

        public MeasureUnitCode GetMeasureUnitCode()
        {
            return MeasureUnitElements.MeasureUnitCode;
        }
    }
    #endregion

    #region Static fields
    private static readonly decimal[] AreaExchangeRates = [100, 10000, 1000000];
    private static readonly decimal[] DistanceExchangeRates = [1000];
    private static readonly decimal[] ExtentExchangeRates = [10, 100, 1000];
    private static readonly decimal[] TimePeriodExchangeRates = [60, 1440, 10080, 14400];
    private static readonly decimal[] VolumeExchangeRates = [1000, 1000000, 1000000000];
    private static readonly decimal[] WeightExchangeRates = [1000, 1000000];
    #endregion

    #region Constructors
    #region Static constructor
    static BaseMeasurement()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ExchangeRateCollection = new(ConstantExchangeRateCollection);
    }
    #endregion

    protected BaseMeasurement(IBaseMeasurementFactory factory) : base(factory)
    {
    }
    #endregion

    #region Properties
    #region Static properties
    public static Dictionary<object, decimal> ConstantExchangeRateCollection { get; }
    public static Dictionary<object, decimal> ExchangeRateCollection { get; protected set; }
    #endregion
    #endregion

    #region Public methods
    public int CompareTo(IBaseMeasurement? other)
    {
        if (other == null) return 1;

        other.ValidateMeasureUnitCode(GetMeasureUnitCode(), nameof(other));

        return GetExchangeRate().CompareTo(other.GetExchangeRate());
    }

    public bool Equals(IBaseMeasurement? other)
    {
        return base.Equals(other)
            && other.GetExchangeRate() == GetExchangeRate();
    }

    public IBaseMeasurement? GetBaseMeasurement(Enum context)
    {
        return GetFactory().CreateBaseMeasurement(context);
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
        string paramName = nameof(other);

        MeasureUnitCode measureUnitCode = NullChecked(other, paramName).GetMeasureUnitCode();

        if (HasMeasureUnitCode(measureUnitCode)) return GetExchangeRate() / other!.GetExchangeRate();

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }

    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        ValidateExchangeRate(exchangeRate, paramName, GetBaseMeasureUnit());
    }

    #region Override methods
    public override IBaseMeasurementFactory GetFactory()
    {
        return (IBaseMeasurementFactory)Factory;
    }

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

    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        if (ExchangeRateCollection.ContainsKey(NullChecked(measureUnit, paramName))) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract string GetName();
    #endregion

    #region Static methods
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
        if (NullChecked(measureUnit, paramName) is MeasureUnitCode measureUnitCode)
        {
            measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        }

        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate != default) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }

    public static Dictionary<object, decimal> GetExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitCode);
    }

    public static IEnumerable<object> GetValidMeasureUnits()
    {
        foreach (object item in ExchangeRateCollection.Keys)
        {
            yield return item;
        }
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
        return measureUnit != null
            && GetValidMeasureUnits().Contains(measureUnit);
    }

    public static bool NameEquals(string name, string otherName)
    {
        return string.Equals(name, otherName, StringComparison.OrdinalIgnoreCase);
    }

    public static void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit)
    {
        if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

        throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
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
    #endregion
    #endregion

    #region Private methods
    #region Static methods
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
