namespace CsabaDu.FooVaria.Common.Statics;

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

