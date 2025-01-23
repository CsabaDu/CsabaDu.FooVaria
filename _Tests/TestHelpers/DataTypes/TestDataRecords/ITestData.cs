namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public interface ITestData
{
    string TestCase { get; }
    string Result { get; }

    object?[] ToArgs(ArgsCode argsCode);
}

public interface ITestData<String> : ITestData;
