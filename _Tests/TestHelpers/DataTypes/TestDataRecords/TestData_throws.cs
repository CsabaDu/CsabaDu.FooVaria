namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;

public abstract record TestData_throws<TException>(string ParamsDescription, TException Exception)
    : TestData<TException>(ParamsDescription, ResultCode.throws)
    where TException : Exception
{
    protected override string Result => typeof(TException).Name;
}

public abstract record TestData_throws(string ParamsDescription, Exception Exception)
    : TestData_throws<Exception>(ParamsDescription, Exception)
{
    protected override string Result => Exception.GetType().Name;
}
