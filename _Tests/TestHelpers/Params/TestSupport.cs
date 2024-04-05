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

    public static bool DoesNotThrowException(Action validator)
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

    public static bool DoesSucceedAsExpected(bool success, object obj)
    {
        if (obj is not null) return success == true;

        return success == false;
    }
}
