namespace CsabaDu.FooVaria.Measurables.Statics;

internal static class ExchangeRates
{
    public const decimal DefaultExchangeRate = decimal.One;

    private static readonly IDictionary<object, decimal> ConstantExchangeRateCollection = new Dictionary<object, decimal>()
    {
        // AreaUnits
        { AreaUnit.mmSquare, DefaultExchangeRate },
        { AreaUnit.cmSquare, 100m },
        { AreaUnit.dmSquare, 10000m },
        { AreaUnit.meterSquare, 1000000m },

        // Currency
        { Currency.Default, DefaultExchangeRate },

        // DistanceUnits
        { DistanceUnit.meter, DefaultExchangeRate },
        { DistanceUnit.km, 1000m },

        // ExtentUnits
        { ExtentUnit.mm, DefaultExchangeRate },
        { ExtentUnit.cm, 10m },
        { ExtentUnit.dm, 100m },
        { ExtentUnit.meter, 1000m },

        // Pieces
        { Pieces.Default, DefaultExchangeRate },

        // TimePeriodUnits
        { TimePeriodUnit.minute, DefaultExchangeRate },
        { TimePeriodUnit.hour, 60m },
        { TimePeriodUnit.day, 1440m },
        { TimePeriodUnit.week, 10080m },
        { TimePeriodUnit.decade, 14400m },

        // VolumeUnits
        { VolumeUnit.mmCubic, DefaultExchangeRate },
        { VolumeUnit.cmCubic, 1000m },
        { VolumeUnit.dmCubic, 1000000m },
        { VolumeUnit.meterCubic, 1000000000m },

        // WeightUnits
        { WeightUnit.g, DefaultExchangeRate },
        { WeightUnit.kg, 1000m },
        { WeightUnit.ton, 1000000m },
    };

    private static IDictionary<object, decimal> ExchangeRateCollection { get; set; } = new Dictionary<object, decimal>(ConstantExchangeRateCollection);

    internal static void SetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (TrySetExchangeRate(measureUnit, exchangeRate)) return;
        
        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    internal static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (exchangeRate <= 0) throw ExchangeRateArgumentOutOfRangeException(exchangeRate);

        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit) && ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
    }

    internal static bool RemoveExchangeRate(Enum measureUnit)
    {
        return ExchangeRateCollection.Remove(measureUnit);
    }

    internal static void RestoreConstantExchangeRates()
    {
        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
    }

    internal static IEnumerable<object> GetValidMeasureUnits()
    {
        foreach (object item in ExchangeRateCollection.Keys)
        {
            yield return item;
        }
    }

    internal static IDictionary<Enum, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(ExchangeRateCollection);
    }

    internal static IDictionary<Enum, decimal> GetConstantExchangeRateCollection()
    {
        return GetExchangeRateCollection(ConstantExchangeRateCollection);
    }

    private static IDictionary<Enum, decimal> GetExchangeRateCollection(IDictionary<object, decimal> collection)
    {
        Dictionary<Enum, decimal> exchangeRateCollection = new();

        for (int i = 0; i < collection.Count; i++)
        {
            Enum measureUnit = (Enum)collection.Keys.ElementAt(i);
            decimal exchangeRate = collection.Values.ElementAt(i);

            exchangeRateCollection.Add(measureUnit, exchangeRate);
        }

        return exchangeRateCollection;
    }
}
