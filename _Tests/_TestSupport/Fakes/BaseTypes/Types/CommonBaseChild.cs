namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types
{
    internal sealed class CommonBaseChild : CommonBase
    {
        public CommonBaseChild(IFactory factory) : base(factory)
        {
        }

        public CommonBaseChild(ICommonBase other) : base(other)
        {
        }
    }
}
