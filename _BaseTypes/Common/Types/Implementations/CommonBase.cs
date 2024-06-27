namespace CsabaDu.FooVaria.BaseTypes.Common.Types.Implementations;

public abstract class CommonBase(IRootObject rootObject, string paramName) : ICommonBase
{
    #region Properties
    public IFactory Factory { get; init; } = NullChecked(rootObject, paramName) switch
    {
        CommonBase commonBase => commonBase.Factory,
        IFactory factory => factory,

        _ => throw new InvalidOperationException(null),
    };
    #endregion
}
