namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.Fakes
{
    internal sealed class CommonBaseChild(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName)
    {
        #region Members

        // CommonBaseChild CommonBaseChild(IRootObject rootObject, string paramName)
        // IFactory ICommonBase.GetFactory()

        #endregion

        public CommonBaseReturn Return { private get; set; }

        public override IFactory GetFactory() => Return.GetFactory;
    }
}
