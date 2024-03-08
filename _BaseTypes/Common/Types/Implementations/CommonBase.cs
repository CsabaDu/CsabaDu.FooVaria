namespace CsabaDu.FooVaria.BaseTypes.Common.Types.Implementations;

public abstract class CommonBase : ICommonBase
{
    #region Constructors
    protected CommonBase(IRootObject rootObject, string paramName)
    {
        _ = NullChecked(rootObject, paramName);
    }
    #endregion

    #region Public methods
    #region Abstract methods
    public abstract IFactory GetFactory();
    #endregion
    #endregion
}
