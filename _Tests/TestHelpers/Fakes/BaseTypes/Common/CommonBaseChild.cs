namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Common;

public sealed class CommonBaseChild(IRootObject rootObject, string paramName) : FooVaria.BaseTypes.Common.Types.Implementations.CommonBase(rootObject, paramName)
{
    #region Members

    // CommonBaseChild CommonBaseChild(IRootObject rootObject, string paramName)
    // IFactory ICommonBase.GetFactoryValue()

    #endregion

    public CommonBaseReturnValues ReturnValues { private get; set; }

    public override IFactory GetFactory() => ReturnValues.GetFactoryValue;
}
