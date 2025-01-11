namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public interface ITestData
{
    string TestCase { get; }

    object[] ToArgs(ArgsCode argsCode);
}
