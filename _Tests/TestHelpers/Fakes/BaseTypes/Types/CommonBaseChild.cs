namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Types
{
    public sealed class CommonBaseChild(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName)
    {
        #region Members

        // CommonBaseChild CommonBaseChild(IRootObject rootObject, string paramName)
        // IFactory ICommonBase.GetFactory()

        #endregion

        public CommonBaseReturns Return { private get; set; }

        public override IFactory GetFactory() => Return.GetFactory;
    }
}
