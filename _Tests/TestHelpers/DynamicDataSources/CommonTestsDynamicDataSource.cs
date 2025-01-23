namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public class CommonTestsDynamicDataSource(ArgsCode argsCode) : DynamicDataSource(argsCode)
{
    private const string containsNullValueElements = " contains null value elements";
    private const string doesNotContainElement = " does not contain any element";
    private const string enumerableContainsNotNullElementsOnly = "Enumerable contains notnull elements only";
    private const string enumerableContainsNullAndNotNullElements = "Enumerable contains null and notnull elements";
    private const string enumerableContainsNullElementsOnly = "Enumerable contains null elements only";
    private const string enumerableDoesNotContainElement = "Enumerable does not contain any element";
    private const string enumerableGetEnumeratorReturnsNull = "Enumerable.GetEnumerator() returns null";
    private const string enumerableContainsElements = "Enumerable contains elements";
    private const string sGetEnumeratorReturnsNull = "'s GetEnumerator() method returns null";
    private const string theValueCannotBeAnEmptyString = "The value cannot be an empty string.";

    private bool _expected;
    private string _exitMode = null;

    private class NullEnumeratorEnumerable() : IEnumerable
    {
        public IEnumerator GetEnumerator() => null;
    }

    private string ReturnsParamName => $"Returns '{ParamName}'";

    private string GetCheckNullEnumerableParamsDescription(string paramsDescription, bool checkElements)
    => Definition = paramsDescription + GetCheckElementsState(checkElements);

    private static string GetCheckElementsState(bool checkElements)
    => $" when bool checkElements param is {checkElements}";

    private void SetParamName(string paramName)
    => ParamName = paramName;

    private string GetEnumerableExceptionMessageContent(string messageEnd)
    => $"The {ParamName} enumerable{messageEnd}.";

    private object[] TestDataToArgs<TStruct>(TStruct arg) where TStruct : struct
    {
        Definition = _exitMode ?? arg.ToString();
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
        _exitMode = nameof(invalidValue);
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

        Definition = "Empty string";
        object param = string.Empty;
        MessageContent = theValueCannotBeAnEmptyString;
        yield return testDataToArgs();

        Definition = enumerableGetEnumeratorReturnsNull;
        param = new NullEnumeratorEnumerable();
        MessageContent = GetEnumerableExceptionMessageContent(sGetEnumeratorReturnsNull);
        yield return testDataToArgs();

        Definition = enumerableDoesNotContainElement;
        param = new List<object>();
        MessageContent = GetEnumerableExceptionMessageContent(doesNotContainElement);
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataThrowsToArgs<ArgumentException, object>(param);
    }

    public IEnumerable<object[]> ExceptionMethods_NullChecked_object_Returns_ArgsToList()
    {
        Definition = "object";
        ParamName = ParamNames.param;
        object param = new();
        yield return testDataToArgs();

        Definition = "Not empty string";
        param = " ";
        yield return testDataToArgs();

        Definition = enumerableContainsElements;
        param = new List<object>() { null };
        yield return testDataToArgs();

        object[] testDataToArgs() => TestDataToArgs(ReturnsParamName, param);
    }

    public IEnumerable<object[]> ExceptionMethods_NullChecked_IEnumerable_ArgumentException_ArgsToList()
    {
        SetParamName(ParamNames.enumerable);

        #region checkElements = false
        bool checkElements = false;
        Definition = enumerableGetEnumeratorReturnsNull;
        IEnumerable enumerable = new NullEnumeratorEnumerable();
        MessageContent = GetEnumerableExceptionMessageContent(sGetEnumeratorReturnsNull);
        yield return testDataToArgs();

        Definition = enumerableDoesNotContainElement;
        enumerable = new List<object>();
        MessageContent = GetEnumerableExceptionMessageContent(doesNotContainElement);
        yield return testDataToArgs();
        #endregion

        #region checkElements = true
        checkElements = true;
        Definition = GetCheckNullEnumerableParamsDescription(enumerableContainsNullElementsOnly, checkElements);
        enumerable = new List<object>() { null };
        MessageContent = GetEnumerableExceptionMessageContent(containsNullValueElements);
        yield return testDataToArgs();

        Definition = GetCheckNullEnumerableParamsDescription(enumerableContainsNullAndNotNullElements, checkElements);
        enumerable = new List<object>() { new(), null };
        yield return testDataToArgs();
        #endregion

        object[] testDataToArgs() => TestDataThrowsToArgs<ArgumentException, IEnumerable, bool>(enumerable, checkElements);
    }
    public IEnumerable<object[]> ExceptionMethods_NullChecked_IEnumerable_Returns_ArgsToList()
    {
        #region checkElements = false
        bool checkElements = false;
        ParamName = ParamNames.enumerable;
        IEnumerable enumerable = new List<object>() { null };
        Definition = GetCheckNullEnumerableParamsDescription(enumerableContainsNullElementsOnly, checkElements);
        yield return testDataToArgs();

        Definition = GetCheckNullEnumerableParamsDescription(enumerableContainsNullAndNotNullElements, checkElements);
        enumerable = new List<object>() { new(), null };
        yield return testDataToArgs();
        #endregion

        #region checkElements = true
        checkElements = true;
        Definition = GetCheckNullEnumerableParamsDescription(enumerableContainsNotNullElementsOnly, checkElements);
        enumerable = new List<object>() { new() };
        yield return testDataToArgs();
        #endregion

        object[] testDataToArgs() => TestDataToArgs(ReturnsParamName, enumerable, checkElements);
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
