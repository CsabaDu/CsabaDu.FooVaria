namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData_returns<TStruct>(string ParamsDescription, TStruct Expected)
    : TestData<TStruct>(ParamsDescription, ResultCode.returns)
    where TStruct : struct
{
    protected override string Result => Expected.ToString();
}

public record TestData_returns<T1, TStruct>(string ParamsDescription, T1 Arg1, TStruct Expected)
    : TestData_returns<TStruct>(ParamsDescription, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Expected] : base.ToArgs(argsCode);
}

public record TestData_returns<T1, T2, TStruct>(string ParamsDescription, T1 Arg1, T2 Arg2, TStruct Expected)
    : TestData_returns<T1, TStruct>(ParamsDescription, Arg1, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Arg2, Expected] : base.ToArgs(argsCode);
}

public record TestData_returns<T1, T2, T3, TStruct>(string ParamsDescription, T1 Arg1, T2 Arg2, T3 Arg3, TStruct Expected)
    : TestData_returns<T1, T2, TStruct>(ParamsDescription, Arg1, Arg2, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Arg2, Arg3, Expected] : base.ToArgs(argsCode);
}

public record TestData_returns<T1, T2, T3, T4, TStruct>(string ParamsDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, TStruct Expected)
    : TestData_returns<T1, T2, T3, TStruct>(ParamsDescription, Arg1, Arg2, Arg3, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Arg2, Arg3, Arg4, Expected] : base.ToArgs(argsCode);
}

public record TestData_returns<T1, T2, T3, T4, T5, TStruct>(string ParamsDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, TStruct Expected)
    : TestData_returns<T1, T2, T3, T4, TStruct>(ParamsDescription, Arg1, Arg2, Arg3, Arg4, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Arg2, Arg3, Arg4, Arg5, Expected] : base.ToArgs(argsCode);
}

public record TestData_returns<T1, T2, T3, T4, T5, T6, TStruct>(string ParamsDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, TStruct Expected)
    : TestData_returns<T1, T2, T3, T4, T5, TStruct>(ParamsDescription, Arg1, Arg2, Arg3, Arg4, Arg5, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Expected] : base.ToArgs(argsCode);
}

public record TestData_returns<T1, T2, T3, T4, T5, T6, T7, TStruct>(string ParamsDescription, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, TStruct Expected)
    : TestData_returns<T1, T2, T3, T4, T5, T6, TStruct>(ParamsDescription, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Expected)
    where TStruct : struct
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ? [TestCase, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Expected] : base.ToArgs(argsCode);
}

public abstract record TestData_returns(string ParamsDescription, string ExpectedDescription = "")
    : TestData<object>(ParamsDescription, ResultCode.returns)
{
    protected override string Result => ExpectedDescription;
}
