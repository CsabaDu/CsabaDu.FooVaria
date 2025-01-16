namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Common;

public class TestCommonBase : CommonBase
{
    private readonly IFactory _factory;

    public TestCommonBase(IRootObject rootObject, string paramName, IFactory factory) : base(rootObject, paramName)
    {
        _factory = factory;
    }

    public override IFactory GetFactory() => _factory;
}
