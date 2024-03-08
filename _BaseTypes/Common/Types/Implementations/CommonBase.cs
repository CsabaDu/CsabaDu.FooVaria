namespace CsabaDu.FooVaria.BaseTypes.Common.Types.Implementations;

public abstract class CommonBase : ICommonBase
{
    #region Constructors
    protected CommonBase(IFactory factory)
    {
        Factory = NullChecked(factory, nameof(factory));
    }

    protected CommonBase(ICommonBase other)
    {
        Factory = NullChecked(other, nameof(other)).Factory;
    }

    //protected CommonBase(IRootObject rootObject)
    //{
    //    _ = NullChecked(rootObject, nameof(rootObject));
    //}

    #endregion

    #region Properties
    public IFactory Factory { get; init; }
    #endregion

    #region Public methods
    #region Virtual methods
    public virtual IFactory GetFactory()
    {
        return Factory;
    }
    #endregion
    #endregion
}
