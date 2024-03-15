using CsabaDu.FooVaria.BaseTypes.Common;
using CsabaDu.FooVaria.BaseTypes.Common.Types.Implementations;
using CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Returns;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.Fakes
{
    internal sealed class CommonBaseChild(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName)
    {
        #region Members

        // CommonBaseChild CommonBaseChild(IRootObject rootObject, string paramName)
        // IFactory ICommonBase.GetFactory()

        #endregion

        public CommonBaseReturns Return { private get; set; }

        public override IFactory GetFactory() => Return.GetFactory;
    }
}
