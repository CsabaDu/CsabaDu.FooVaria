using CsabaDu.FooVaria.Common.Enums;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasurement : Measurable, IBaseMeasurement
{
    #region Constructors
    #region Static constructor
    static BaseMeasurement()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
        CustomNameCollection = new Dictionary<object, string>();
    }
    #endregion

    private protected BaseMeasurement(IBaseMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
        if (!ExchangeRateCollection.ContainsKey(measureUnit))
        {
            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }
    }
    #endregion

    #region Static properties
    #region Protected properties
    public static IDictionary<object, string> CustomNameCollection { get; protected set; }
    public static IDictionary<object, decimal> ExchangeRateCollection { get; protected set; }
    #endregion

    #region Private properties
    public static IDictionary<object, decimal> ConstantExchangeRateCollection { get; }
    #endregion
    #endregion

    #region Public methods
    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return GetConstantExchangeRateCollection(MeasureUnitTypeCode);
    }

    public IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitTypeCode);
    }


    public string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public string? GetCustomName()
    {
        return GetCustomName(GetMeasureUnit());
    }

    public IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);
    }

    public IDictionary<object, string> GetCustomNameCollection()
    {
        return CustomNameCollection;
    }

    public string GetDefaultName()
    {
        return GetDefaultName(GetMeasureUnit());
    }

    public string GetDefaultName(Enum measureUnit)
    {
        return MeasureUnitTypes.GetDefaultName(measureUnit);
    }

    public decimal GetExchangeRate(Enum measureUnit)
    {
        decimal? exchangeRate = GetExchangeRateOrNull(NullChecked(measureUnit, nameof(measureUnit)));

        if (exchangeRate != null) return exchangeRate.Value;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public decimal GetExchangeRate(string name)
    {
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, nameof(name)));

        if (measureUnit != null)
        {
            decimal? exchangeRate = GetExchangeRateOrNull(measureUnit);

            if (exchangeRate != null) return exchangeRate.Value;
        }

        throw NameArgumentOutOfRangeException(name);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(MeasureUnitTypeCode);
    }

    public IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitTypeCode);
    }

    public Enum? GetMeasureUnit(string name)
    {
        return (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == NullChecked(name, nameof(name))).Value;
    }

    public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitTypeCode);
        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    public IDictionary<string, object> GetMeasureUnitCollection()
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits();
        IDictionary<object, string> customNameCollection = CustomNameCollection;

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    public string GetName()
    {
        return GetCustomName() ?? GetDefaultName();
    }

    public IEnumerable<object> GetValidMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitTypeCode.GetMeasureUnitType()));
    }

    public bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return exchangeRate == GetExchangeRate(measureUnit);
    }

    public void RestoreConstantExchangeRates()
    {
        CustomNameCollection.Clear();
        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
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

    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
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
    public override IBaseMeasurementFactory GetFactory()
    {
        return (IBaseMeasurementFactory)Factory;
    }
    #endregion

    #region Static methods
    public static IEnumerable<object> GetConstantMeasureUnits()
    {
        foreach (object item in ConstantExchangeRateCollection.Keys)
        {
            yield return item;
        }

    }

    public static IEnumerable<object> GetValidMeasureUnits()
    {
        foreach (object item in ExchangeRateCollection.Keys)
        {
            yield return item;
        }
    }

    public static bool IsValidMeasureUnit(Enum? measureUnit)
    {
        if (measureUnit == null) return false;

        return GetValidMeasureUnits().Contains(measureUnit);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static decimal? GetExchangeRateOrNull(Enum? measureUnit)
    {
        if (measureUnit == null) return null;

        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate == default) return null;

        return exchangeRate;
    }

    private static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
    {
        _ = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));

        return measureUnitBasedCollection
            .Where(x => x.Key.GetType().Name.Equals(Enum.GetName(measureUnitTypeCode)))
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    private static IDictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
    {
        IDictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary
            (
                x => MeasureUnitTypes.GetDefaultName((Enum)x),
                x => x
            );

        foreach (KeyValuePair<object, string> item in customNameCollection)
        {
            measureUnitCollection.Add(item.Value, item.Key);
        }

        return measureUnitCollection;
    }

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
            yield return new KeyValuePair<object, decimal>(default(T), decimal.One);

            int exchangeRateCount = exchangeRates?.Length ?? 0;

            if (exchangeRateCount > 0)
            {
                T[] measureUnits = Enum.GetValues<T>();
                int measureUnitCount = measureUnits.Length;

                if (measureUnitCount == 0) throw new InvalidOperationException(null);

                if (exchangeRateCount != measureUnitCount - 1) throw new InvalidOperationException(null);

                int i = 0;

                foreach (decimal item in exchangeRates!)
                {
                    yield return new KeyValuePair<object, decimal>(measureUnits[++i], item);
                }
            }
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
    #endregion
    #endregion
}
