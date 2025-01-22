namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public abstract class DynamicDataSource(ArgsCode argsCode)
{
    #region Properties
    protected ArgsCode ArgsCode { get; } = argsCode;
    protected string ParamsDescription { private get; set; } = null;
    protected string ParamName { get; set; } = null;
    protected string MessageContent { private get; set; } = null;
    #endregion

    #region Methods
    #region TestDataReturnsToArgs
    protected object[] TestDataReturnsToArgs<TStruct, T1>(TStruct expected, T1 arg1) where TStruct : struct
    => new TestData_returns<TStruct, T1>(ParamsDescription, expected, arg1).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2>(TStruct expected, T1 arg1, T2 arg2) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2>(ParamsDescription, expected, arg1, arg2).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3>(TStruct expected, T1 arg1, T2 arg2, T3 arg3) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3>(ParamsDescription, expected, arg1, arg2, arg3).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4>(TStruct expected, T1 arg1, T2 arg2, T3 arg3, T4 arg4) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4>(ParamsDescription, expected, arg1, arg2, arg3, arg4).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4, T5>(TStruct expected, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4, T5>(ParamsDescription, expected, arg1, arg2, arg3, arg4, arg5).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4, T5, T6>(TStruct expected, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 args6) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4, T5, T6>(ParamsDescription, expected, arg1, arg2, arg3, arg4, arg5, args6).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4, T5, T6, T7>(TStruct expected, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4, T5, T6, T7>(ParamsDescription, expected, arg1, arg2, arg3, arg4, arg5, arg6, arg7).ToArgs(ArgsCode);
    
    protected object[] TestDataReturnsToArgs(string resultDescription, params object[] args)
    => new TestData_returns(ParamsDescription, resultDescription, args).ToArgs(ArgsCode);
    #endregion

    #region TestDataThrowsToArgs
    protected object[] TestDataThrowsToArgs<TException, T1>(T1 arg1, object value = null) where TException : Exception
    => new TestData_throws<TException, T1>(ParamsDescription, ParamName, MessageContent, arg1).SetMessage(value).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2>(T1 arg1, T2 arg2, object value = null) where TException : Exception
    => new TestData_throws<TException, T1, T2>(ParamsDescription, ParamName, MessageContent, arg1, arg2).SetMessage(value).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, object value = null) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3>(ParamsDescription, ParamName, MessageContent, arg1, arg2, arg3).SetMessage(value).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, object value = null) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4>(ParamsDescription, ParamName, MessageContent, arg1, arg2, arg3, arg4).SetMessage(value).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, object value = null) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4, T5>(ParamsDescription, ParamName, MessageContent, arg1, arg2, arg3, arg4, arg5).SetMessage(value).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, object value = null) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4, T5, T6>(ParamsDescription, ParamName, MessageContent, arg1, arg2, arg3, arg4, arg5, arg6).SetMessage(value).ToArgs(ArgsCode);

    protected object[]TestDataThrowsToArgs<TException, T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, object value = null) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4, T5, T6, T7>(ParamsDescription, ParamName, MessageContent, arg1, arg2, arg3, arg4, arg5, arg6, arg7).SetMessage(value).ToArgs(ArgsCode);
    #endregion
    #endregion
}
