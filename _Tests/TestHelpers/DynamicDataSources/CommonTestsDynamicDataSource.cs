namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public class CommonTestsDynamicDataSource(ArgsCode argsCode) : DynamicDataSource(argsCode)
{
    private bool _expected;

    private object[] TestDataToArgs<T>(T arg, string paramsDescription = null) where T : struct
    {
        ParamsDescription = paramsDescription ?? arg.ToString();
        return TestDataReturnsToArgs(_expected, arg);
    }

    public IEnumerable<object[]> Extensions_IsValidExchangeRate_ArgsToList()
    {
        _expected = true;
        decimal decimalQuantity = 0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = 1m;
        yield return testDataToArgs();

        decimalQuantity = 1000000000m;
        yield return testDataToArgs();

        _expected = false;
        decimalQuantity = 0m;
        yield return testDataToArgs();

        decimalQuantity = -0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = -1m;
        yield return testDataToArgs();

        decimalQuantity = -1000000000m;
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataToArgs(decimalQuantity);
    }

    public IEnumerable<object[]> Extensions_IsDefined_ArgsToList()
    {
        string paramsDescription = null;

        _expected = true;
        TestEnum testEnum = TestEnum.MinValue;
        yield return testDataToArgs();

        testEnum = TestEnum.MidValue;
        yield return testDataToArgs();

        testEnum = TestEnum.MaxValue;
        yield return testDataToArgs();

        _expected = false;
        int invalidValue = Enum.GetNames<TestEnum>().Length;
        paramsDescription = nameof(invalidValue);
        testEnum = (TestEnum)invalidValue;
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataToArgs(testEnum, paramsDescription);
    }
}
