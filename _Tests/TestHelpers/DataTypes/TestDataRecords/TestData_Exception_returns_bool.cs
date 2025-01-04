namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords
{
    public record TestData_Exception_returns_bool(string ParamsDescription, Exception Exception, bool Expected) : TestData_returns_bool(ParamsDescription, Expected)
    {
        public override object[] ToArgs(ArgsCode argsCode) => argsCode switch
        {
            ArgsCode.Properties => [TestCase, Exception, Expected],
            _ => base.ToArgs(argsCode),
        };
    }
}
