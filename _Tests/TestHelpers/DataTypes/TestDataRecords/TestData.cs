namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData<TResult>(string ParamsDescription) : ITestData where TResult : notnull
{
    public abstract string ResultMode { get; }

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

public record TestData<String, T1>(string ParamsDescription, string ResultDescription, T1 Arg1)
    : TestData<string>(ParamsDescription)
{
    protected override sealed string ResultName => null;

    public override sealed string ResultMode => ResultDescription;

    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2>(string ParamsDescription, string ResultDescription, T1 Arg1, T2 Arg2)
    : TestData<string, T1>(ParamsDescription, ResultDescription, Arg1)
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1, Arg2]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3>(string ParamsDescription, string ResultDescription, T1 Arg1, T2 Arg2, T3 Arg3)
    : TestData<string, T1, T2>(ParamsDescription, ResultDescription, Arg1, Arg2)
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1, Arg2, Arg3]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4>(string ParamsDescription, string ResultDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4)
    : TestData<string, T1, T2, T3>(ParamsDescription, ResultDescription, Arg1, Arg2, Arg3)
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1, Arg2, Arg3, Arg4]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4, T5>(string ParamsDescription, string ResultDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5)
    : TestData<string, T1, T2, T3, T4>(ParamsDescription, ResultDescription, Arg1, Arg2, Arg3, Arg4)
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1, Arg2, Arg3, Arg4, Arg5]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4, T5, T6>(string ParamsDescription, string ResultDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6)
    : TestData<string, T1, T2, T3, T4, T5>(ParamsDescription, ResultDescription, Arg1, Arg2, Arg3, Arg4, Arg5)
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4, T5, T6, T7>(string ParamsDescription, string ResultDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7)
    : TestData<string, T1, T2, T3, T4, T5, T6>(ParamsDescription, ResultDescription, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ResultDescription, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7]
        : base.ToArgs(argsCode);
}
