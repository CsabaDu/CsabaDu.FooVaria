namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

internal sealed class CommonBaseChild : CommonBase
{
    public CommonBaseChild(IFactory factory) : base(factory)
    {
    }

    public CommonBaseChild(ICommonBase other) : base(other)
    {
    }

    public override IFactory GetFactory()
    {
        throw new NotImplementedException();
    }
}
