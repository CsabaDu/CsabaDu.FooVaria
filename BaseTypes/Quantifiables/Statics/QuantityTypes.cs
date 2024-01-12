namespace CsabaDu.FooVaria.Quantifiables.Statics;

public static class QuantityTypes
{
    private static HashSet<Type> QuantityTypeSet => new()
    {
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(double),
        typeof(decimal),
    };

    private const int QuantityRoundingDecimals = 8;

    public static decimal RoundQuantity(decimal quantity)
    {
        return decimal.Round(quantity, QuantityRoundingDecimals);
    }

    public static double RoundQuantity(double quantity)
    {
        return Math.Round(quantity, QuantityRoundingDecimals);
    }

    public static IEnumerable<TypeCode> GetQuantityTypeCodes()
    {
        foreach (Type item in QuantityTypeSet)
        {
            yield return Type.GetTypeCode(item);
        }
    }

    public static IEnumerable<Type> GetQuantityTypes()
    {
        return QuantityTypeSet;
    }
}

