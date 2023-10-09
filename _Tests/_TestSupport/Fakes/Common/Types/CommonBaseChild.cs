namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

internal sealed class CommonBaseChild : CommonBase
{
    public CommonBaseChild(IFactory factory) : base(factory)
    {
    }

    public CommonBaseChild(ICommonBase other) : base(other)
    {
    }

    public CommonBaseChild(IFactory factory, ICommonBase commonBase) : base(factory, commonBase)
    {
    }

    public override IFactory GetFactory()
    {
        throw new NotImplementedException();
    }

    public override void Validate(ICommonBase other)
    {
        throw new NotImplementedException();
    }

    public override void Validate(IFactory factory)
    {
        throw new NotImplementedException();
    }
}
