namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public record TestData_Enum_returns_bool(string ParamsDescription, Enum Context, bool Expected) : TestData_returns_bool(ParamsDescription, Expected)
{
    public override object[] ToArgs(ArgsCode argsCode) => argsCode switch
    {
        ArgsCode.Properties => [TestCase, Context, Expected],
        _ => base.ToArgs(argsCode),
    };
}
