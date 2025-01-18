namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class ExtensionsTests
{
    #region Private static fields
    private static readonly CommonTestsDynamicDataSource DynamicDataSource = new(ArgsCode.Instance);
    #endregion

    #region Private static properties
    public static IEnumerable<object[]> IsValidExchangeRate_ArgsList
    => DynamicDataSource.Extensions_IsValidExchangeRate_ArgsToList();
    public static IEnumerable<object[]> IsDefined_ArgsList
    => DynamicDataSource.Extensions_IsDefined_ArgsToList();
    #endregion

    #region Test methods
    #region IsValidExchangeRate tests
    #region static bool IsValidExchangeRate(this decimal)
    [Theory, MemberData(nameof(IsValidExchangeRate_ArgsList))]
    public void IsValidExchangeRate_returnsExpected(TestData_returns<bool, decimal> testData)
    {
        // Arrange
        decimal exchangeRate = testData.Arg1;

        // Act
        var actual = exchangeRate.IsValidExchangeRate();

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion
    #endregion

    #region IsDefined tests
    #region static bool IsDefined<T>(this T) where T : struct, Enum
    [Theory, MemberData(nameof(IsDefined_ArgsList))]
    public void IsDefined_returnsExpected(TestData_returns<bool, TestEnum> testData)
    {
        // Arrange
        TestEnum testEnum = testData.Arg1;

        // Act
        var actual = testEnum.IsDefined();

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion
    #endregion
    #endregion
}
