namespace CsabaDu.FooVaria.Measurables.Statics;

internal static class ExchangeRates
{
    #region Constructor
    static ExchangeRates()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ExchangeRateCollection = new SortedList<object, decimal>(ConstantExchangeRateCollection);
    }
    #endregion

    #region Properties
    private static IDictionary<object, decimal> ConstantExchangeRateCollection { get; }
    //{
    //    // AreaUnits
    //    { AreaUnit.mmSquare, DefaultExchangeRate },
    //    { AreaUnit.cmSquare, 100m },
    //    { AreaUnit.dmSquare, 10000m },
    //    { AreaUnit.meterSquare, 1000000m },

    //    // Currency
    //    { Currency.Default, DefaultExchangeRate },

    //    // DistanceUnits
    //    { DistanceUnit.meter, DefaultExchangeRate },
    //    { DistanceUnit.km, 1000m },

    //    // ExtentUnits
    //    { ExtentUnit.mm, DefaultExchangeRate },
    //    { ExtentUnit.cm, 10m },
    //    { ExtentUnit.dm, 100m },
    //    { ExtentUnit.meter, 1000m },

    //    // Pieces
    //    { Pieces.Default, DefaultExchangeRate },

    //    // TimePeriodUnits
    //    { TimePeriodUnit.minute, DefaultExchangeRate },
    //    { TimePeriodUnit.hour, 60m },
    //    { TimePeriodUnit.day, 1440m },
    //    { TimePeriodUnit.week, 10080m },
    //    { TimePeriodUnit.decade, 14400m },

    //    // VolumeUnits
    //    { VolumeUnit.mmCubic, DefaultExchangeRate },
    //    { VolumeUnit.cmCubic, 1000m },
    //    { VolumeUnit.dmCubic, 1000000m },
    //    { VolumeUnit.meterCubic, 1000000000m },

    //    // WeightUnits
    //    { WeightUnit.g, DefaultExchangeRate },
    //    { WeightUnit.kg, 1000m },
    //    { WeightUnit.ton, 1000000m },
    //};

    private static IDictionary<object, decimal> ExchangeRateCollection { get; set; }
    #endregion

    #region Internal mehods
    internal static IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return ConstantExchangeRateCollection;
    }

    internal static IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return ExchangeRateCollection;
    }

    internal static IEnumerable<object> GetValidMeasureUnits()
    {
        foreach (object item in ExchangeRateCollection.Keys)
        {
            yield return item;
        }
    }

    internal static bool RemoveExchangeRate(Enum measureUnit)
    {
        return ExchangeRateCollection.Remove(measureUnit);
    }

    internal static void RestoreConstantExchangeRates()
    {
        ExchangeRateCollection = new SortedList<object, decimal>(ConstantExchangeRateCollection);
    }

    internal static void SetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (TrySetExchangeRate(measureUnit, exchangeRate)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    internal static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        exchangeRate.ValidateExchangeRate();

        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit) && IsDefinedExchangeRate(exchangeRate, measureUnit) || ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
    }

    internal static bool IsDefinedExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return ExchangeRateCollection.ContainsKey(measureUnit) && exchangeRate == ExchangeRateCollection[measureUnit];
    }

    internal static bool IsValidMeasureUnit(Enum? measureUnit)
    {
        if (measureUnit == null) return false;

        return GetValidMeasureUnits().Contains(measureUnit);
    }
    #endregion

    #region Private methods
    //private static IDictionary<object, decimal> GetExchangeRateCollection(IDictionary<object, decimal> collection)
    //{
    //    Dictionary<object, decimal> exchangeRateCollection = new();

    //    for (int i = 0; i < collection.Count; i++)
    //    {
    //        object measureUnit = collection.Keys.ElementAt(i);
    //        decimal exchangeRate = collection.Values.ElementAt(i);

    //        exchangeRateCollection.Add(measureUnit, exchangeRate);
    //    }

    //    return exchangeRateCollection;
    //}

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

                if (exchangeRateCount != measureUnitCount - 1) throw new ArgumentOutOfRangeException(nameof(exchangeRates), exchangeRateCount, null);

                int i = 0;

                foreach (decimal item in exchangeRates!)
                {
                    yield return new KeyValuePair<object, decimal>(measureUnits[++i], item);
                }
            }
        }
        #endregion
    }
    #endregion
}
