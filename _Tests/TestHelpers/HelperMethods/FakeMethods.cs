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
}
