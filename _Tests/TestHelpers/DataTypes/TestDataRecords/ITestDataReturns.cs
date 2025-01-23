namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public interface ITestDataReturns<TStruct> : ITestData<TStruct> where TStruct : struct
{
    TStruct Expected { get; }
}

