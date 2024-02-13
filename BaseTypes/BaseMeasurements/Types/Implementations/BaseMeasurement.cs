﻿namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;

public abstract class BaseMeasurement : Measurable, IBaseMeasurement
{
    #region Constructors
    #region Static constructor
    static BaseMeasurement()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
    }
    #endregion

    protected BaseMeasurement(IBaseMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
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

        other.ValidateMeasureUnitCode(MeasureUnitCode, nameof(other));

        return GetExchangeRate().CompareTo(other.GetExchangeRate());
    }

    public bool Equals(IBaseMeasurement? other)
    {
        return base.Equals(other)
            && other.GetExchangeRate() == GetExchangeRate();
    }

    public IBaseMeasurement? GetBaseMeasurement(object measureUnit)
    {
        return GetFactory().CreateBaseMeasurement(measureUnit);
    }

    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return GetConstantExchangeRateCollection(MeasureUnitCode);
    }

    public IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitCode);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitCode);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(MeasureUnitCode);
    }

    public Enum GetMeasureUnit(IMeasureUnit<Enum>? other)
    {
        return (other ?? this).GetMeasureUnit();
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return HasMeasureUnitCode(measureUnitCode);

        return IsValidMeasureUnit(context) && HasMeasureUnitCode(MeasureUnitCode, context!);
    }

    public bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return exchangeRate == GetExchangeRate(measureUnit);
    }

    public decimal ProportionalTo(IBaseMeasurement? other)
    {
        MeasureUnitCode measureUnitCode = NullChecked(other, nameof(other)).MeasureUnitCode;

        if (HasMeasureUnitCode(measureUnitCode)) return GetExchangeRate() / other!.GetExchangeRate();

        throw new ArgumentOutOfRangeException(nameof(other), measureUnitCode, null);
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
        return HashCode.Combine(MeasureUnitCode, GetExchangeRate());
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
    public abstract decimal GetExchangeRate();
    public abstract decimal GetExchangeRate(string name);
    public abstract string GetName();
    //public abstract void RestoreConstantExchangeRates();
    #endregion

    #region Static methods
    public static IEnumerable<object> GetConstantMeasureUnits()
    {
        foreach (object item in ConstantExchangeRateCollection.Keys)
        {
            yield return item;
        }

    }

    public static decimal GetExchangeRate(Enum measureUnit)
    {
        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate != default) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
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

    public static bool IsValidMeasureUnit(Enum? measureUnit)
    {
        if (measureUnit == null) return false;

        return GetValidMeasureUnits().Contains(measureUnit);
    }

    public static bool NameEquals(string name, string otherName)
    {
        return string.Equals(name, otherName, StringComparison.OrdinalIgnoreCase);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitCode measureUnitCode)
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
