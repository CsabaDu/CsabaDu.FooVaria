namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestDataReturns<TStruct>(string Definition, TStruct Expected)
    : TestData<TStruct>(Definition, Expected.ToString()!)
    where TStruct : struct;

public record TestDataReturns<TStruct, T1>(string Definition, TStruct Expected, T1? Arg1)
    : TestDataReturns<TStruct>(Definition, Expected)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1]
        : base.ToArgs(argsCode);
}

public record TestDataReturns<TStruct, T1, T2>(string Definition, TStruct Expected, T1? Arg1, T2? Arg2)
    : TestDataReturns<TStruct, T1>(Definition, Expected, Arg1)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1, Arg2]
        : base.ToArgs(argsCode);
}

public record TestDataReturns<TStruct, T1, T2, T3>(string Definition, TStruct Expected, T1? Arg1, T2? Arg2, T3? Arg3)
    : TestDataReturns<TStruct, T1, T2>(Definition, Expected, Arg1, Arg2)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1, Arg2, Arg3]
        : base.ToArgs(argsCode);
}

public record TestDataReturns<TStruct, T1, T2, T3, T4>(string Definition, TStruct Expected, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4)
    : TestDataReturns<TStruct, T1, T2, T3>(Definition, Expected, Arg1, Arg2, Arg3)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1, Arg2, Arg3, Arg4]
        : base.ToArgs(argsCode);
}

public record TestDataReturns<TStruct, T1, T2, T3, T4, T5>(string Definition, TStruct Expected, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5)
    : TestDataReturns<TStruct, T1, T2, T3, T4>(Definition, Expected, Arg1, Arg2, Arg3, Arg4)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1, Arg2, Arg3, Arg4, Arg5]
        : base.ToArgs(argsCode);
}

public record TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6>(string Definition, TStruct Expected, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6)
    : TestDataReturns<TStruct, T1, T2, T3, T4, T5>(Definition, Expected, Arg1, Arg2, Arg3, Arg4, Arg5)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6]
        : base.ToArgs(argsCode);
}

public record TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6, T7>(string Definition, TStruct Expected, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7)
    : TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6>(Definition, Expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
    where TStruct : struct
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, Expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7]
        : base.ToArgs(argsCode);
}
