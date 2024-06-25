namespace CsabaDu.FooVaria.BaseTypes.Common.Types.Implementations;

public abstract class CommonBase(IRootObject rootObject, string paramName) : ICommonBase
{
    #region Constructors
    //protected CommonBase(IRootObject rootObject, string paramName)
    //{
    //    _ = NullChecked(rootObject, paramName);

    //    Factory = getFactory();

    //    #region Local methods
    //    ICreateNew getFactory()
    //    {
    //        if (rootObject is ICreateNew factory)
    //        {
    //            return factory;
    //        }

    //        if (rootObject is IGetNew commonBase)
    //        {
    //            return commonBase.Factory;
    //        }
            
    //        throw new InvalidOperationException(null);
    //    }
    //    #endregion
    //}
    #endregion

    #region Properties
    public IFactory Factory { get; init; } = NullChecked(rootObject, paramName) switch
    {
        CommonBase commonBase => commonBase.Factory,
        IFactory factory => factory,

        _ => throw new InvalidOperationException(null),
    };
    #endregion
}
