namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData<TResult>(string ParamsDescription) : ITestData where TResult : notnull
{
    protected abstract string Result { get; }
    protected abstract string ResultType { get; }
    public string TestCase => $"{ParamsDescription} => {ResultType} {Result}";

    protected string GetResultType()
    {
        string typeName = GetType().Name;
        int lastUnderscoreIndexPlus = typeName.LastIndexOf('_') + 1;
        int apostropheIndex = typeName.IndexOf('`', lastUnderscoreIndexPlus);
        return apostropheIndex == -1 ?
            typeName[lastUnderscoreIndexPlus..]
            : typeName[lastUnderscoreIndexPlus..apostropheIndex];
    }

    public virtual object[] ToArgs(ArgsCode argsCode) => argsCode == ArgsCode.Instance ? [this] : null;
    public override sealed string ToString() => TestCase;
}
