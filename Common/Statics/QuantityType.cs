namespace CsabaDu.FooVaria.Common.Statics;

public static class QuantityType
{
    private static HashSet<Type> QuantityTypes => new()
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
        foreach (Type item in QuantityTypes)
        {
            yield return Type.GetTypeCode(item);
        }
    }

    public static IEnumerable<Type> GetQuantityTypes()
    {
        return QuantityTypes;
    }
}

