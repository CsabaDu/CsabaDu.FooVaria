namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public record TestData_decimal_returns_bool(string ParamsDescription, decimal DecimalQuantity, bool Expected) : TestData_returns_bool(ParamsDescription, Expected)
{
    public override object[] ToArgs(ArgsCode argsCode) => argsCode switch
    {
        ArgsCode.Properties => [TestCase, DecimalQuantity, Expected],
        _ => base.ToArgs(argsCode),
    };
}
