namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestDataThrows<TException>(string Definition, string ParamName, string MessageContent)
    : TestData<TException>(Definition, typeof(TException).Name), ITestData<TException>
    where TException : Exception
{
    public string Message { get; private set; } = MessageContent;

    public Type ExceptionType => typeof(TException);

    public TestDataThrows<TException> SetMessage(object? value)
    {
        string parameter = $" (Parameter '{ParamName}')";

        if (ExceptionType == typeof(ArgumentOutOfRangeException))
        {
            Message += parameter + getActualType();
        }
        else if (ExceptionType == typeof(ArgumentException))
        {
            Message += parameter;
        }

        return this;

        string getActualType() => $"\r\nActual value was {value?.GetType().Name ?? string.Empty}.";
    }
}

public record TestDataThrows<TException, T1>(string Definition, string ParamName, string MessageContent, T1? Arg1)
    : TestDataThrows<TException>(Definition, ParamName, MessageContent)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1]
        : base.ToArgs(argsCode);
}

public record TestDataThrows<TException, T1, T2>(string Definition, string ParamName, string MessageContent, T1? Arg1, T2? Arg2)
    : TestDataThrows<TException, T1>(Definition, ParamName, MessageContent, Arg1)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2]
        : base.ToArgs(argsCode);
}

public record TestDataThrows<TException, T1, T2, T3>(string Definition, string ParamName, string MessageContent, T1? Arg1, T2? Arg2, T3? Arg3)
    : TestDataThrows<TException, T1, T2>(Definition, ParamName, MessageContent, Arg1, Arg2)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3]
        : base.ToArgs(argsCode);
}

public record TestDataThrows<TException, T1, T2, T3, T4>(string Definition, string ParamName, string MessageContent, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4)
    : TestDataThrows<TException, T1, T2, T3>(Definition, ParamName, MessageContent, Arg1, Arg2, Arg3)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4]
        : base.ToArgs(argsCode);
}

public record TestDataThrows<TException, T1, T2, T3, T4, T5>(string Definition, string ParamName, string MessageContent, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5)
    : TestDataThrows<TException, T1, T2, T3, T4>(Definition, ParamName, MessageContent, Arg1, Arg2, Arg3, Arg4)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4, Arg5]
        : base.ToArgs(argsCode);
}

public record TestDataThrows<TException, T1, T2, T3, T4, T5, T6>(string Definition, string ParamName, string MessageContent, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6)
    : TestDataThrows<TException, T1, T2, T3, T4, T5>(Definition, ParamName, MessageContent, Arg1, Arg2, Arg3, Arg4, Arg5)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6]
        : base.ToArgs(argsCode);
}

public record TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7>(string Definition, string ParamName, string MessageContent, T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7)
    : TestDataThrows<TException, T1, T2, T3, T4, T5, T6>(Definition, ParamName, MessageContent, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
    where TException : Exception
{
    public override object?[] ToArgs(ArgsCode argsCode)
    => argsCode == ArgsCode.Properties ?
        [TestCase, ParamName, Message, ExceptionType, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7]
        : base.ToArgs(argsCode);
}
