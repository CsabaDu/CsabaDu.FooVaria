namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public sealed class TestSupport
{
    public static void RestoreConstantExchangeRates()
    {
        if (ExchangeRateCollection.Count != ConstantExchangeRateCount)
        {
            BaseMeasurement.RestoreConstantExchangeRates();
        }
    }

    public static bool Returned(Action validator)
    {
        try
        {
            validator();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
