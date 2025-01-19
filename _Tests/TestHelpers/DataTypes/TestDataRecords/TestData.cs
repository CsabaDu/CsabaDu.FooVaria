namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData<TResult>(string ParamsDescription) : ITestData where TResult : notnull
{
    protected abstract string ResultTypeName { get; }
    protected abstract string ResultName { get; }

    public string TestCase => ResultName == null ?
        $"{ParamsDescription} => {ResultTypeName}"
        : $"{ParamsDescription} => {ResultTypeName} {ResultName}";

    protected string GetResultTypeName()
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
