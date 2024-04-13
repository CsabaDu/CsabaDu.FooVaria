namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests;

internal class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        measureUnitCode = RandomParams.GetRandomSpreadMeasureUnitCode();

        return GetIsExchangeableToArgs(RandomParams.GetRandomMeasureUnit(measureUnitCode));
    }
    #endregion
}
