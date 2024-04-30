namespace CsabaDu.FooVaria.Tests.TestHelpers.HelperMethods;

public static class FakeMethods
{
    public static bool TryExchange<T>(T quantifiable, Func<T> create, Enum context, out T? exchanged)
        where T : class, IQuantifiable
    {
        exchanged = null;

        if (quantifiable.IsExchangeableTo(context))
        {
            exchanged = create();
        }

        return exchanged is not null;
    }

    public static bool Equals(IBaseShapeComponent x, IBaseShapeComponent y)
    {
        if (x is null && y is null) return true;

        if (x is null || y is null) return false;

        if (x.GetMeasureUnitCode() != y.GetMeasureUnitCode()) return false;

        return x.GetBaseShapeComponents().SequenceEqual(y.GetBaseShapeComponents());
    }

    public static int GetHashCode([DisallowNull] IShapeComponent baseShapeComponent)
    {
        HashCode hashCode = new();
        hashCode.Add(baseShapeComponent.GetMeasureUnitCode());

        foreach (IBaseShapeComponent item in baseShapeComponent.GetBaseShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }
}
