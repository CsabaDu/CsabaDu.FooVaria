namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData_throws<TException>(string ParamsDescription, string ParamName, string MessageContent)
    : TestData<TException>(ParamsDescription)
    where TException : Exception
{
    public string Message => typeof(TException) == typeof(ArgumentException) ? $"{MessageContent} (Parameter '{ParamName}')" : MessageContent;
    public Type ExceptionType => typeof(TException);
    protected override sealed string ResultType => GetResultType();
    protected override sealed string ResultName => ExceptionType.Name;
}

public record TestData_throws<TException, T1>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1)
    : TestData_throws<TException>(ParamsDescription, ParamName, MessageContent)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1]
        : base.ToArgs(argsCode);
}

public record TestData_throws<TException, T1, T2>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1, T2 Arg2)
    : TestData_throws<TException, T1>(ParamsDescription, ParamName, MessageContent, Arg1)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2]
        : base.ToArgs(argsCode);
}

public record TestData_throws<TException, T1, T2, T3>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1, T2 Arg2, T3 Arg3)
    : TestData_throws<TException, T1, T2>(ParamsDescription, ParamName, MessageContent, Arg1, Arg2)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3]
        : base.ToArgs(argsCode);
}

public record TestData_throws<TException, T1, T2, T3, T4>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4)
    : TestData_throws<TException, T1, T2, T3>(ParamsDescription, ParamName, MessageContent, Arg1, Arg2, Arg3)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4]
        : base.ToArgs(argsCode);
}

public record TestData_throws<TException, T1, T2, T3, T4, T5>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5)
    : TestData_throws<TException, T1, T2, T3, T4>(ParamsDescription, ParamName, MessageContent, Arg1, Arg2, Arg3, Arg4)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4, Arg5]
        : base.ToArgs(argsCode);
}

public record TestData_throws<TException, T1, T2, T3, T4, T5, T6>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6)
    : TestData_throws<TException, T1, T2, T3, T4, T5>(ParamsDescription, ParamName, MessageContent, Arg1, Arg2, Arg3, Arg4, Arg5)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6]
        : base.ToArgs(argsCode);
}

public record TestData_throws<TException, T1, T2, T3, T4, T5, T6, T7>(string ParamsDescription, string ParamName, string MessageContent, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7)
    : TestData_throws<TException, T1, T2, T3, T4, T5, T6>(ParamsDescription, ParamName, MessageContent, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
    where TException : Exception
{
    public override object[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7]
        : base.ToArgs(argsCode);
}
