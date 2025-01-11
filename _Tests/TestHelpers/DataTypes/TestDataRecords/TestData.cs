namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData<TResult>(string ParamsDescription, ResultCode ResultCode) : ITestData where TResult : notnull
{
    protected abstract string Result { get; }
    public string TestCase => $"{ParamsDescription} => {ResultCode} {Result}";

    public override sealed string ToString() => TestCase;
    public virtual object[] ToArgs(ArgsCode argsCode) => argsCode == ArgsCode.Instance ? [this] : null;
}
