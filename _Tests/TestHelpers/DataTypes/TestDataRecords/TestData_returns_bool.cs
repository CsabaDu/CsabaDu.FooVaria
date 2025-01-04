namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords
{
    public abstract record TestData_returns_bool(string ParamsDescription, bool Expected) : TestData_returns<bool>(ParamsDescription, Expected);
}
