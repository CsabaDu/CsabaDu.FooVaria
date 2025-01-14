namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData_returns<TStruct>(string ParamsDescription, TStruct Expected)
    : TestData<TStruct>(ParamsDescription)
    where TStruct : struct
{
    protected override sealed string ResultType => GetResultType();
    protected override sealed string Result => Expected.ToString();
}

public record TestData_returns<TStruct, T1>(string ParamsDescription, TStruct Expected, T1 Arg1)
    : TestData_returns<TStruct>(ParamsDescription, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1] : base.ToArgs(argsCode);
}

public record TestData_returns<TStruct, T1, T2>(string ParamsDescription, TStruct Expected, T1 Arg1, T2 Arg2)
    : TestData_returns<TStruct, T1>(ParamsDescription, Expected, Arg1)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1, Arg2] : base.ToArgs(argsCode);
}

public record TestData_returns<TStruct, T1, T2, T3>(string ParamsDescription, TStruct Expected, T1 Arg1, T2 Arg2, T3 Arg3)
    : TestData_returns<TStruct, T1, T2>(ParamsDescription, Expected, Arg1, Arg2)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1, Arg2, Arg3] : base.ToArgs(argsCode);
}

public record TestData_returns<TStruct, T1, T2, T3, T4>(string ParamsDescription, TStruct Expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4)
    : TestData_returns<TStruct, T1, T2, T3>(ParamsDescription, Expected, Arg1, Arg2, Arg3)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1, Arg2, Arg3, Arg4] : base.ToArgs(argsCode);
}

public record TestData_returns<TStruct, T1, T2, T3, T4, T5>(string ParamsDescription, TStruct Expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5)
    : TestData_returns<TStruct, T1, T2, T3, T4>(ParamsDescription, Expected, Arg1, Arg2, Arg3, Arg4)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1, Arg2, Arg3, Arg4, Arg5] : base.ToArgs(argsCode);
}

public record TestData_returns<TStruct, T1, T2, T3, T4, T5, T6>(string ParamsDescription, TStruct Expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6)
    : TestData_returns<TStruct, T1, T2, T3, T4, T5>(ParamsDescription, Expected, Arg1, Arg2, Arg3, Arg4, Arg5)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6] : base.ToArgs(argsCode);
}

public record TestData_returns<TStruct, T1, T2, T3, T4, T5, T6, T7>(string ParamsDescription, TStruct Expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7)
    : TestData_returns<TStruct, T1, T2, T3, T4, T5, T6>(ParamsDescription, Expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7] : base.ToArgs(argsCode);
}

public abstract record TestData_returns(string ParamsDescription, string ResultDescription = "")
    : TestData<object>(ParamsDescription)
{
    protected override sealed string ResultType => GetResultType();

    protected override string Result => ResultDescription;
}
