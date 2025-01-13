namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public abstract class DynamicDataSource(ArgsCode argsCode)
{
    #region Properties
    protected ArgsCode ArgsCode { get; } = argsCode;

    protected string ParamsDescription { private get; set; }
    #endregion

    #region Methods
    #region TestDataReturnsToArgs
    protected object[] TestDataReturnsToArgs<TStruct, T1>(TStruct expected, T1 arg1) where TStruct : struct
    => new TestData_returns<TStruct, T1>(ParamsDescription, expected, arg1).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2>(TStruct expected, T1 arg1, T2 arg2) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2>(ParamsDescription, expected, arg1, arg2).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3>(TStruct expected, T1 arg1, T2 arg2, T3 arg3) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3>(ParamsDescription, expected, arg1, arg2, arg3).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4>(TStruct expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4>(ParamsDescription, expected, Arg1, Arg2, Arg3, Arg4).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4, T5>(TStruct expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4, T5>(ParamsDescription, expected, Arg1, Arg2, Arg3, Arg4, Arg5).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4, T5, T6>(TStruct expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Args6) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4, T5, T6>(ParamsDescription, expected, Arg1, Arg2, Arg3, Arg4, Arg5, Args6).ToArgs(ArgsCode);

    protected object[] TestDataReturnsToArgs<TStruct, T1, T2, T3, T4, T5, T6, T7>(TStruct expected, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7) where TStruct : struct
    => new TestData_returns<TStruct, T1, T2, T3, T4, T5, T6, T7>(ParamsDescription, expected, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7).ToArgs(ArgsCode);
    #endregion

    #region TestDataThrowsToArgs
    protected object[] TestDataThrowsToArgs<TException, T1>(TException exception, T1 arg1) where TException : Exception
    => new TestData_throws<TException, T1>(ParamsDescription, exception, arg1).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2>(TException exception, T1 arg1, T2 arg2) where TException : Exception
    => new TestData_throws<TException, T1, T2>(ParamsDescription, exception, arg1, arg2).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3>(TException exception, T1 arg1, T2 arg2, T3 arg3) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3>(ParamsDescription, exception, arg1, arg2, arg3).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3, T4>(TException exception, T1 arg1, T2 arg2, T3 arg3, T4 arg4) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4>(ParamsDescription, exception, arg1, arg2, arg3, arg4).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3, T4, T5>(TException exception, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4, T5>(ParamsDescription, exception, arg1, arg2, arg3, arg4, arg5).ToArgs(ArgsCode);

    protected object[] TestDataThrowsToArgs<TException, T1, T2, T3, T4, T5, T6>(TException exception, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4, T5, T6>(ParamsDescription, exception, arg1, arg2, arg3, arg4, arg5, arg6).ToArgs(ArgsCode);

    protected object[]TestDataThrowsToArgs<TException, T1, T2, T3, T4, T5, T6, T7>(TException exception, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) where TException : Exception
    => new TestData_throws<TException, T1, T2, T3, T4, T5, T6, T7>(ParamsDescription, exception, arg1, arg2, arg3, arg4, arg5, arg6, arg7).ToArgs(ArgsCode);
    #endregion
    #endregion
}
