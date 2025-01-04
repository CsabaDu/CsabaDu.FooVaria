namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class ExtensionsTests
{
    #region Private fields
    #region Static fields
    private static readonly CommonTestsDynamicDataSource DynamicDataSource = new();
    private static readonly ArgsCode ArgsCode = ArgsCode.Instance;
    #endregion
    #endregion

    #region Private properties
    #region Static properties
    public static IEnumerable<object[]> IsValidExchangeRate_ArgsList
    => DynamicDataSource.Extensions_IsValidExchangeRate_ArgsToList(ArgsCode);

    public static IEnumerable<object[]> IsDefined_ArgsList
    => DynamicDataSource.Extensions_IsDefined_ArgsToList(ArgsCode);
    #endregion
    #endregion

    #region Test methods
    #region IsValidExchangeRate
    [Theory, MemberData(nameof(IsValidExchangeRate_ArgsList))]
    public void IsValidExchangeRate_returns_expected(TestData_decimal_returns_bool testData)
    {
        // Arrange & Act
        var actual = testData.DecimalQuantity.IsValidExchangeRate();

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion

    #region IsDefined
    [Theory, MemberData(nameof(IsDefined_ArgsList))]
    public void IsDefined_returns_expected(TestData_Enum_returns_bool testData)
    {
        // Arrange
        var testEnum = (TestEnum)testData.Context;

        // Act
        var actual = testEnum.IsDefined();

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion
    #endregion
}