namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public class CommonTestsDynamicDataSource(ArgsCode argsCode) : DynamicDataSource(argsCode)
{
    private bool _expected;
    private string _paramsDescription = null;

    private class NullEnumeratorEnumerable() : IEnumerable
    {
        public IEnumerator GetEnumerator() => null;
    }

    private void SetParamName(string paramName) => ParamName = paramName;

    private string GetEnumerableExceptionMessageContent(string messageEnd)
    {
        return $"The {ParamName} enumerable{messageEnd}.";
    }

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
    public IEnumerable<object[]> ExceptionMethods_NullChecked_object_ArgumentException_ArgsToList()
    {
        SetParamName(ParamNames.param);

        ParamsDescription = "Empty string";
        object param = string.Empty;
        MessageContent = "The value cannot be an empty string.";
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable.GetEnumerator() returns null";
        param = new NullEnumeratorEnumerable();
        MessageContent = GetEnumerableExceptionMessageContent("'s GetEnumerator() method returns null");
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable does not contain any element";
        param = new List<object>();
        MessageContent = GetEnumerableExceptionMessageContent(" does not contain any element");
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
        param = new List<object>() { null };
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataReturnsToArgs(null, param);
    }

    public IEnumerable<object[]> ExceptionMethods_NullChecked_IEnumerable_ArgumentException_ArgsToList()
    {
        SetParamName(ParamNames.enumerable);

        bool checkElements = false;
        string checkelementState = $" ({nameof(checkElements)}: {checkElements})";
        ParamsDescription = "IEnumerable.GetEnumerator() returns null";
        IEnumerable enumerable = new NullEnumeratorEnumerable();
        MessageContent = GetEnumerableExceptionMessageContent("'s GetEnumerator() method returns null");
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable does not contain any element";
        enumerable = new List<object>();
        MessageContent = GetEnumerableExceptionMessageContent(" does not contain any element");
        yield return testDataToArgs();

        checkElements = true;
        ParamsDescription = "IEnumerable contains null elements only" + checkelementState;
        enumerable = new List<object>() { null, };
        MessageContent = GetEnumerableExceptionMessageContent(" contains null value elements");
        yield return testDataToArgs();

        ParamsDescription = "IEnumerable contains null and notnull elements" + checkelementState;
        enumerable = new List<object>() { new(), null };
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataThrowsToArgs<ArgumentException, IEnumerable, bool>(enumerable, checkElements);
    }

    #endregion

    #region TypeChecked
    public IEnumerable<object[]> ExceptionMethods_TypeChecked_ArgumentNullException_ArgsToList()
    {
        SetParamName(ParamNames.param);
        object param = null;
        Type validType = null;
        yield return testDataToArgs();

        SetParamName(ParamNames.validType);
        param = new();
        yield return testDataToArgs();  

        object[] testDataToArgs() => TestDataThrowsToArgs<ArgumentNullException, object, Type>(param, validType);
    }

    #endregion
    #endregion
}
