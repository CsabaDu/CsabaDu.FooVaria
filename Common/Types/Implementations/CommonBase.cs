namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class CommonBase : ICommonBase
{
    private protected CommonBase(IFactory factory)
    {
        Factory = NullChecked(factory, nameof(factory));
    }

    private protected CommonBase(ICommonBase other)
    {
        Factory = NullChecked(other, nameof(other)).Factory;
    }

    public IFactory Factory { get; init; }

    public abstract IFactory GetFactory();
}
