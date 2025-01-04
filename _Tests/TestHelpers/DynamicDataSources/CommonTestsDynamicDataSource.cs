namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public class CommonTestsDynamicDataSource : DynamicDataSourcesBase
{
    public IEnumerable<object[]> Extensions_IsValidExchangeRate_ArgsToList(ArgsCode argsCode)
    {
        _expectedIsTrue = true;
        decimalQuantity = 0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = 1m;
        yield return testDataToArgs();

        decimalQuantity = 1000000000m;
        yield return testDataToArgs();

        _expectedIsTrue = false;
        decimalQuantity = 0m;
        yield return testDataToArgs();

        decimalQuantity = -0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = -1m;
        yield return testDataToArgs();

        decimalQuantity = -1000000000m;
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataToArgs(decimalQuantity, argsCode);
    }

    public IEnumerable<object[]> Extensions_IsDefined_ArgsToList(ArgsCode argsCode)
    {
        _expectedIsTrue = true;
        string paramsDescription = null;
        context = TestEnum.ValidMinValue;
        yield return testDataToArgs();

        context = TestEnum.ValidMaxValue;
        yield return testDataToArgs();

        _expectedIsTrue = false;
        paramsDescription = "Invalid value";
        context = (TestEnum)2;
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataToArgs(context, argsCode, paramsDescription);
    }
}
