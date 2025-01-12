namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class ExtensionsTests
{
    #region Private fields
    #region Static fields
    private static readonly DynamicTestData<CommonTestsDynamicDataSource> DynamicTestData
        = new(new CommonTestsDynamicDataSource(ArgsCode.Instance));
    #endregion
    #endregion

    #region Private properties
    #region Static properties
    public static IEnumerable<object[]> IsValidExchangeRate_ArgsList
    => DynamicTestData.Source.Extensions_IsValidExchangeRate_ArgsToList();

    public static IEnumerable<object[]> IsDefined_ArgsList
    => DynamicTestData.Source.Extensions_IsDefined_ArgsToList();
    #endregion
    #endregion

    #region Test methods
    #region IsValidExchangeRate
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

    #region IsDefined
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
}
