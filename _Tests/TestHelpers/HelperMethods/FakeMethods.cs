namespace CsabaDu.FooVaria.Tests.TestHelpers.HelperMethods;

public static class FakeMethods
{
    public static bool TryExchange(IQuantifiable quantifiable, Func<IQuantifiable> create, Enum context, out IQuantifiable? exchanged)
    {
        if (quantifiable.IsExchangeableTo(context))
        {
            exchanged = create();
            return exchanged is not null;
        }

        exchanged = null;
        return false;
    }
}
