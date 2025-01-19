using CsabaDu.FooVaria.BaseTypes.Common.Enums;

namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class ExceptionMethodsTests
{
    #region Private fields
    #region Static fields
    private static readonly CommonTestsDynamicDataSource DynamicDataSource = new(ArgsCode.Instance);
    #endregion
    #endregion

    #region Private properties
    #region Static properties
    public static IEnumerable<object[]> NullChecked_ArgumentException_ArgsList
    => DynamicDataSource.ExceptionMethods_NullChecked_ArgumentException_ArgsToList();
    public static IEnumerable<object[]> NullChecked_Returns_ArgsList
    => DynamicDataSource.ExceptionMethods_NullChecked_Returns_ArgsToList();
    #endregion
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

    [Theory, MemberData(nameof(NullChecked_ArgumentException_ArgsList))]
    public void NullChecked_invalidArg_object_arg_string_throwsArgumentException(TestData_throws<ArgumentException, object> testData)
    {
        // Arrange
        object param = testData.Arg1;
        string paramName = ParamNames.param;

        // Act
        void attempt() => _ = NullChecked(param, paramName);

        // Assert
        var exception = Assert.Throws<ArgumentException>(paramName, attempt);
        Assert.Equal(testData.Message, exception.Message);
    }

    [Theory, MemberData(nameof(NullChecked_Returns_ArgsList))]
    public void NullChecked_validArg_object_arg_string_returnsExpected(TestData_returns testData)
    {
        // Arrange
        object param = testData.Args[0];
        string paramName = ParamNames.param;

        // Act
        var actual = NullChecked(param, paramName);

        // Assert
        Assert.Equal(param, actual);
    }
    #endregion
    #endregion

    #region TypeChecked tests
    #region static T TypeChecked<T>(T?, string, Type)

    #endregion

    #region static T TypeChecked<T>(object?, string)

    #endregion

    [Fact]
    public void TypeChecked_InvalidType_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        string paramName = "param";
        object param = new object();
        Type expectedType = typeof(string);

        // Act
        void act() => TypeChecked(param, paramName, expectedType);

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(paramName, act);
        Assert.Equal($"The {paramName} argument's type is invalid in this context.", exception.Message);
    }

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
    [Fact]
    public void Defined_InvalidEnum_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        string paramName = "param";
        SideCode invalidEnum = (SideCode)999;

        // Act
        void act() => Defined(invalidEnum, paramName);

        // Assert
        var exception = Assert.Throws<InvalidEnumArgumentException>(paramName, act);
        Assert.Equal($"The {paramName} argument's type is invalid in this context.", exception.Message);
    }

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
