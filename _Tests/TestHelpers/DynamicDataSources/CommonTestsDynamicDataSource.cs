namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public class CommonTestsDynamicDataSource(ArgsCode argsCode) : DynamicDataSource(argsCode)
{
    private bool _expected;
    private string _paramsDescription = null;

    private void SetParamName() => ParamName = ParamNames.param;

    private class NullEnumeratorEnumerable() : IEnumerable
    {
        public IEnumerator GetEnumerator() => null;
    }

    private class TestType : object;

    private object[] TestDataToArgs<TStruct>(TStruct arg) where TStruct : struct
    {
        ParamsDescription = _paramsDescription ?? arg.ToString();
        return TestDataReturnsToArgs(_expected, arg);
    }

    #region Extensions
    public IEnumerable<object[]> Extensions_IsValidExchangeRate_ArgsToList()
    {
        #region returns True
        _expected = true;
        decimal decimalQuantity = 0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = 1m;
        yield return testDataToArgs();

        decimalQuantity = 1000000000m;
        yield return testDataToArgs();
        #endregion

        #region returns False
        _expected = false;
        decimalQuantity = 0m;
        yield return testDataToArgs();

        decimalQuantity = -0.0000000001m;
        yield return testDataToArgs();

        decimalQuantity = -1m;
        yield return testDataToArgs();

        decimalQuantity = -1000000000m;
        yield return testDataToArgs();
        #endregion

        object[] testDataToArgs() => TestDataToArgs(decimalQuantity);
    }

    public IEnumerable<object[]> Extensions_IsDefined_ArgsToList()
    {
        #region returns True
        _expected = true;
        TestEnum testEnum = TestEnum.MinValue;
        yield return testDataToArgs();

        testEnum = TestEnum.MidValue;
        yield return testDataToArgs();

        testEnum = TestEnum.MaxValue;
        yield return testDataToArgs();
        #endregion

        #region returns False
        _expected = false;
        int invalidValue = Enum.GetNames<TestEnum>().Length;
        testEnum = (TestEnum)invalidValue;
        _paramsDescription = nameof(invalidValue);
        yield return testDataToArgs();
        #endregion

        object[] testDataToArgs() => TestDataToArgs(testEnum);
    }
    #endregion

    #region ExceptionMethods
    #region NullChecked
    public IEnumerable<object[]> ExceptionMethods_NullChecked_ArgumentException_ArgsToList()
    {
        SetParamName();

        ParamsDescription = "Empty string";
        object param = string.Empty;
        MessageContent = "The value cannot be an empty string.";
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable.GetEnumerator() returns null";
        param = new NullEnumeratorEnumerable();
        MessageContent = $"GetEnumerator() method of the {ParamName} enumerable returns null.";
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable does not contain any element";
        param = new List<object>();
        MessageContent = $"The {ParamName} enumerable does not contain any element.";
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataThrowsToArgs<ArgumentException, object>(param);
    }

    public IEnumerable<object[]> ExceptionMethods_NullChecked_Returns_ArgsToList()
    {
        ParamsDescription = "object";
        object param = new();
        yield return testDataToArgs();

        ParamsDescription = "Not empty string";
        param = ParamNames.param;
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable contains elements";
        param = new List<object>() { new() };
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataReturnsToArgs(null, param);
    }

    public IEnumerable<object[]> ExceptionMethods_TypeChecked_3params_ArgumentNullException_ArgsToList()
    {
        SetParamName();



    }

    #endregion
    #endregion
}
