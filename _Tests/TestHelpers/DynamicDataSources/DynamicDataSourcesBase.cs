namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public abstract partial class DynamicDataSourcesBase : DataFields
{
    protected string _paramsDescription;
    protected bool _expectedIsTrue;
    protected Exception _exception;

    protected object[] TestDataToArgs(object arg, ArgsCode argsCode, string paramsDescription = null)
    {
        _paramsDescription = paramsDescription ?? arg.ToString();

        TestData_returns_bool testData = arg switch
        {
            decimal decimalQuantity => new TestData_decimal_returns_bool(_paramsDescription, decimalQuantity, _expectedIsTrue),
            Enum context => new TestData_Enum_returns_bool(_paramsDescription, context, _expectedIsTrue),
            Exception exception => new TestData_Exception_returns_bool(_paramsDescription, exception, _expectedIsTrue),
            _ => null,
        };

        return testData.ToArgs(argsCode);
    }
}
