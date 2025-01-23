namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData<TResult>(string Definition) : ITestData where TResult : notnull
{
    private string ExitMode
    {
        get
        {
            string typeName = GetType().Name;
            int testDataNameLength = nameof(TestData<TResult>).Length;
            int apostropheIndex = typeName.IndexOf('`', testDataNameLength);
            return apostropheIndex > -1 ?
                typeName[testDataNameLength..apostropheIndex]
                : typeName[testDataNameLength..];
        }
    }

    protected abstract string Result { get; }

    public string TestCase => ExitMode == string.Empty ?
        $"{Definition} => {Result}"
        : $"{Definition} => {ExitMode} {Result}";

    public virtual object?[] ToArgs(ArgsCode argsCode)
    {
        if (argsCode == ArgsCode.Instance) return [this];

        throw new InvalidEnumArgumentException(nameof(argsCode), (int)(object)argsCode, typeof(ArgsCode));
    }

    public override sealed string ToString() => TestCase;
}

public record TestData<String, T1>(string ParamsDescription, string Outcome, T1? Arg1)
    : TestData<string>(ParamsDescription)
{
    protected override sealed string Result => Outcome ?? string.Empty;

    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2>(string ParamsDescription, string Outcome, T1? Arg1, T2? Arg2)
    : TestData<string, T1>(ParamsDescription, Outcome, Arg1)
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1, Arg2]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3>(string ParamsDescription, string Outcome, T1? Arg1, T2? Arg2, T3? Arg3)
    : TestData<string, T1, T2>(ParamsDescription, Outcome, Arg1, Arg2)
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1, Arg2, Arg3]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4>(string ParamsDescription, string Outcome, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4)
    : TestData<string, T1, T2, T3>(ParamsDescription, Outcome, Arg1, Arg2, Arg3)
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1, Arg2, Arg3, Arg4]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4, T5>(string ParamsDescription, string Outcome, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5)
    : TestData<string, T1, T2, T3, T4>(ParamsDescription, Outcome, Arg1, Arg2, Arg3, Arg4)
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1, Arg2, Arg3, Arg4, Arg5]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4, T5, T6>(string ParamsDescription, string Outcome, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6)
    : TestData<string, T1, T2, T3, T4, T5>(ParamsDescription, Outcome, Arg1, Arg2, Arg3, Arg4, Arg5)
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6]
        : base.ToArgs(argsCode);
}

public record TestData<String, T1, T2, T3, T4, T5, T6, T7>(string ParamsDescription, string Outcome, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7)
    : TestData<string, T1, T2, T3, T4, T5, T6>(ParamsDescription, Outcome, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7]
        : base.ToArgs(argsCode);
}
