namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.CommonBase;

public sealed class CommonBaseChild(IRootObject rootObject, string paramName) : FooVaria.BaseTypes.Common.Types.Implementations.CommonBase(rootObject, paramName)
{
    #region Members

    // CommonBaseChild CommonBaseChild(IRootObject rootObject, string paramName)
    // IFactory ICommonBase.GetFactoryValue()

    #endregion

    public CommonBaseReturn Return { private get; set; }

    public override IFactory GetFactory() => Return.GetFactoryValue;
}
