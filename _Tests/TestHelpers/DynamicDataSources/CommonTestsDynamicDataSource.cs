namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public class CommonTestsDynamicDataSource : DynamicDataSource
{
    public IEnumerable<object[]> Extensions_IsValidExchangeRate_ArgsToList(ArgsCode argsCode)
    {
        bool expected = true;
        decimalQuantity = 0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = 1m;
        yield return testDataToArgs();

        decimalQuantity = 1000000000m;
        yield return testDataToArgs();

        expected = false;
        decimalQuantity = 0m;
        yield return testDataToArgs();

        decimalQuantity = -0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = -1m;
        yield return testDataToArgs();

        decimalQuantity = -1000000000m;
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataReturnsToArgs(decimalQuantity, expected, argsCode);
    }

    public IEnumerable<object[]> Extensions_IsDefined_ArgsToList(ArgsCode argsCode)
    {
        bool expected = true;
        string paramsDescription = null;
        TestEnum testEnum = TestEnum.MinValue;
        yield return testDataToArgs();

        testEnum = TestEnum.MidValue;
        yield return testDataToArgs();

        testEnum = TestEnum.MaxValue;
        yield return testDataToArgs();

        expected = false;
        int invalidValue = Enum.GetNames<TestEnum>().Length;
        paramsDescription = nameof(invalidValue);
        testEnum = (TestEnum)invalidValue;
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataReturnsToArgs(testEnum, expected, argsCode, paramsDescription);
    }
}
