using CsabaDu.FooVaria.BaseTypes.Common.Enums;

namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class ExceptionMethodsTests
{
    private class TestType : object;

    #region Private static fields
    private static readonly CommonTestsDynamicDataSource DynamicDataSource = new(ArgsCode.Instance);
    #endregion

    #region Private static properties
    public static IEnumerable<object[]> NullChecked_object_ArgumentException_ArgsList
    => DynamicDataSource.ExceptionMethods_NullChecked_object_ArgumentException_ArgsToList();

    public static IEnumerable<object[]> NullChecked_object_Returns_ArgsList
    => DynamicDataSource.ExceptionMethods_NullChecked_object_Returns_ArgsToList();

    public static IEnumerable<object[]> NullChecked_IEnumerable_ArgumentException_ArgsList
    => DynamicDataSource.ExceptionMethods_NullChecked_IEnumerable_ArgumentException_ArgsToList();

    public static IEnumerable<object[]> NullChecked_IEnumerable_Returns_ArgsList
    => DynamicDataSource.ExceptionMethods_NullChecked_IEnumerable_Returns_ArgsToList();

    public static IEnumerable<object[]> TypeChecked_ArgumentNullException_ArgsList
    => DynamicDataSource.ExceptionMethods_TypeChecked_ArgumentNullException_ArgsToList();
    #endregion

    #region NullChecked tests
    #region T NullChecked<T>(T?, string)
    [Fact]
    public void NullChecked_nullArg_object_arg_string_throwsArgumentNullException()
    {
        // Arrange
        object param = null;
        string paramName = ParamNames.param;

        // Act
        void attempt() => _ = NullChecked(param, paramName);

        // Assert
        _ = Assert.Throws<ArgumentNullException>(paramName, attempt);
    }

    [Theory, MemberData(nameof(NullChecked_object_ArgumentException_ArgsList))]
    public void NullChecked_invalidArg_object_arg_string_throwsArgumentException(TestDataThrows<ArgumentException, object> testData)
    {
        // Arrange
        object param = testData.Arg1;

        // Act
        void attempt() => _ = NullChecked(param, testData.ParamName);

        // Assert
        var exception = Assert.Throws<ArgumentException>(testData.ParamName, attempt);
        Assert.Equal(testData.Message, exception.Message);
    }

    [Theory, MemberData(nameof(NullChecked_object_Returns_ArgsList))]
    public void NullChecked_validArg_object_arg_string_returnsExpected(TestData<string, object> testData)
    {
        // Arrange
        object param = testData.Arg1;

        // Act
        var actual = NullChecked(param, null);

        // Assert
        Assert.Equal(param, actual);
    }
    #endregion

    #region TEnumerable NullChecked<TEnumerable>(TEnumerable?, string, bool) where TEnumerable : IEnumerable
    [Fact]
    public void NullChecked_nullArg_IEnumerable_arg_string_arg_bool_throwsArgumentNullException()
    {
        // Arrange
        IEnumerable enumerable = null;
        string paramName = ParamNames.enumerable;
        bool checkElements = false;

        // Act
        void attempt() => _ = NullChecked(enumerable, paramName, checkElements);

        // Assert
        _ = Assert.Throws<ArgumentNullException>(paramName, attempt);
    }

    [Theory, MemberData(nameof(NullChecked_IEnumerable_ArgumentException_ArgsList))]
    public void NullChecked_invalidArg_IEnumerable_arg_string_arg_bool_throwsArgumentException(TestDataThrows<ArgumentException, IEnumerable, bool> testData)
    {
        // Arrange
        IEnumerable enumerable = testData.Arg1;
        bool checkElements = testData.Arg2;

        // Act
        void attempt() => _ = NullChecked(enumerable, testData.ParamName, checkElements);

        // Assert
        var exception = Assert.Throws<ArgumentException>(testData.ParamName, attempt);
        Assert.Equal(testData.Message, exception.Message);
    }

    [Theory, MemberData(nameof(NullChecked_IEnumerable_Returns_ArgsList))]
    public void NullChecked_validArg_IEnumerable_arg_string_arg_bool_returnsExpected(TestData<string, IEnumerable, bool> testData)
    {
        // Arrange
        IEnumerable enumerable = testData.Arg1;
        bool checkElements = testData.Arg2;

        // Act
        var actual = NullChecked(enumerable, null, checkElements);

        // Assert
        Assert.Equal(enumerable, actual);
    }
    #endregion
    #endregion

    #region TypeChecked tests
    #region static T TypeChecked<T>(T?, string, Type)
    [Theory, MemberData(nameof(TypeChecked_ArgumentNullException_ArgsList))]
    public void TypeChecked_nullArg_object_arg_string_nullArg_Type_throwsArgumentNullException(TestDataThrows<ArgumentNullException, object, Type> testData)
    {
        // Arrange
        object param = testData.Arg1;
        Type validType = testData.Arg2;

        // Act
        void attempt() => _ = TypeChecked(param, testData.ParamName, validType);

        // Assert
        _ = Assert.Throws<ArgumentNullException>(testData.ParamName, attempt);
    }

    #endregion

    #region static T TypeChecked<T>(object?, string)
    [Fact]
    public void TypeChecked_invalidArg_object_arg_string_throwsArgumentOutOfRangeException()
    {
        // Arrange
        object param = new();
        string paramName = ParamNames.param;

        // Act
        void attempt() => _ = TypeChecked<TestType>(param, paramName);

        // Assert
       _ = Assert.Throws<ArgumentOutOfRangeException>(paramName, attempt);
    }
    #endregion

    //[Fact]
    //public void TypeChecked_InvalidType_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    string paramName = "param";
    //    object param = new object();
    //    Type expectedType = typeof(string);

    //    // Act
    //    void act() => TypeChecked(param, paramName, expectedType);

    //    // Assert
    //    _ = Assert.Throws<ArgumentOutOfRangeException>(paramName, act);
    //}

    [Fact]
    public void TypeChecked_ValidType_ReturnsParam()
    {
        // Arrange
        string paramName = "param";
        string param = "test";
        Type expectedType = typeof(string);

        // Act
        var result = TypeChecked(param, paramName, expectedType);

        // Assert
        Assert.Equal(param, result);
    }

    #endregion

    #region Defined Tests
    //[Fact]
    //public void Defined_InvalidEnum_ThrowsInvalidEnumArgumentException()
    //{
    //    // Arrange
    //    string paramName = "param";
    //    SideCode invalidEnum = (SideCode)999;

    //    // Act
    //    void act() => Defined(invalidEnum, paramName);

    //    // Assert
    //    var exception = Assert.Throws<InvalidEnumArgumentException>(paramName, act);
    //    Assert.Equal($"The {paramName} argument's type is invalid in this context.", exception.Message);
    //}

    [Fact]
    public void Defined_ValidEnum_ReturnsEnum()
    {
        // Arrange
        string paramName = "param";
        SideCode validEnum = SideCode.Inner;

        // Act
        var result = Defined(validEnum, paramName);

        // Assert
        Assert.Equal(validEnum, result);
    }
    #endregion
}
