namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords
{
    public abstract record TestData_returns<TStruct>(string ParamsDescription, TStruct Expected) : TestData<TStruct>(ParamsDescription) where TStruct : struct
    {
        protected override string Result => $"returns {Expected}";
    }
}
