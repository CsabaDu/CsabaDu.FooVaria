namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class ExtensionsTests
{
    #region Private fields
    #region Static fields
    private static readonly DynamicData<CommonTestsDynamicDataSource> DynamicData
        = new(new CommonTestsDynamicDataSource(), ArgsCode.Instance);
    #endregion
    #endregion

    #region Private properties
    #region Static properties
    public static IEnumerable<object[]> IsValidExchangeRate_ArgsList
    => DynamicData.Source.Extensions_IsValidExchangeRate_ArgsToList(DynamicData.ArgsCode);

    public static IEnumerable<object[]> IsDefined_ArgsList
    => DynamicData.Source.Extensions_IsDefined_ArgsToList(DynamicData.ArgsCode);
    #endregion
    #endregion

    #region Test methods
    #region IsValidExchangeRate
    [Theory, MemberData(nameof(IsValidExchangeRate_ArgsList))]
    public void IsValidExchangeRate_returnsExpected(TestData_returns<decimal, bool> testData)
    {
        // Arrange
        // Act
        var actual = testData.Arg1.IsValidExchangeRate();

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion

    #region IsDefined
    [Theory, MemberData(nameof(IsDefined_ArgsList))]
    public void IsDefined_returnsExpected(TestData_returns<TestEnum, bool> testData)
    {
        // Arrange
        // Act
        var actual = testData.Arg1.IsDefined();

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion
    #endregion
}
