namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData<TResult>(string ParamsDescription) : ITestData where TResult : notnull
{
    protected abstract string ResultMode { get; }
    protected abstract string ResultName { get; }
    public string TestCase => ResultName == null ?
        $"{ParamsDescription} => {ResultMode}"
        : $"{ParamsDescription} => {ResultMode} {ResultName}";

    protected string GetResultMode()
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
