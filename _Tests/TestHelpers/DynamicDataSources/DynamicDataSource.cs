namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public abstract class DynamicDataSource : DataFields
{
    protected string _paramsDescription;

    protected object[] TestDataReturnsToArgs<T1, TStruct>(T1 arg1, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, TStruct> testData = new(paramsDescription, arg1, expected);
        return testData.ToArgs(argsCode);
    }

    protected object[] TestDataReturnsToArgs<T1, T2, TStruct>(T1 arg1, T2 arg2, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, T2, TStruct> testData = new(paramsDescription, arg1, arg2, expected);
        return testData.ToArgs(argsCode);
    }

    protected object[] TestDataReturnsToArgs<T1, T2, T3, TStruct>(T1 arg1, T2 arg2, T3 arg3, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, T2, T3, TStruct> testData = new(paramsDescription, arg1, arg2, arg3, expected);
        return testData.ToArgs(argsCode);
    }

    protected object[] TestDataReturnsToArgs<T1, T2, T3, T4, TStruct>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, T2, T3, T4, TStruct> testData = new(paramsDescription, arg1, arg2, arg3, arg4, expected);
        return testData.ToArgs(argsCode);
    }

    protected object[] TestDataReturnsToArgs<T1, T2, T3, T4, T5, TStruct>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, T2, T3, T4, T5, TStruct> testData = new(paramsDescription, arg1, arg2, arg3, arg4, arg5, expected);
        return testData.ToArgs(argsCode);
    }

    protected object[] TestDataReturnsToArgs<T1, T2, T3, T4, T5, T6, TStruct>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, T2, T3, T4, T5, T6, TStruct> testData = new(paramsDescription, arg1, arg2, arg3, arg4, arg5, arg6, expected);
        return testData.ToArgs(argsCode);
    }

    protected object[] TestDataReturnsToArgs<T1, T2, T3, T4, T5, T6, T7, TStruct>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, TStruct expected, ArgsCode argsCode, string paramsDescription = null)
        where TStruct : struct
    {
        TestData_returns<T1, T2, T3, T4, T5, T6, T7, TStruct> testData = new(paramsDescription, arg1, arg2, arg3, arg4, arg5, arg6, arg7, expected);
        return testData.ToArgs(argsCode);
    }
}
