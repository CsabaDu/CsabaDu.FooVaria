namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public interface ITestData
{
    string TestCase { get; }
    string ResultMode { get; }

    object[] ToArgs(ArgsCode argsCode);
}

public interface ITestData<TException> : ITestData where TException : Exception
{
    string ParamName { get; init; }
    string Message { get; }
    Type ExceptionType { get; }
}