using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class CommonBase : ICommonBase
{
    #region Constructors
    protected CommonBase(IFactory factory)
    {
        Factory = NullChecked(factory, nameof(factory));
    }

    protected CommonBase(IFactory factory, ICommonBase commonBase)
    {
        Factory = NullChecked(factory, nameof(factory));

        _ = NullChecked(commonBase, nameof(commonBase));
    }

    protected CommonBase(ICommonBase other)
    {
        Factory = NullChecked(other, nameof(other)).Factory;
    }
    #endregion

    #region Properties
    public IFactory Factory { get; init; }
    #endregion

    #region Public methods
    #region Abstract methods
    public abstract IFactory GetFactory();
    public abstract void Validate(ICommonBase? other);
    public abstract void Validate(IFactory? factory);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static void Validate<T, U>(T commonBase, object? arg, string name, [NotNull] out U validArg) where T : class, ICommonBase where U : class
    {
        if (NullChecked(arg, name) is U) validArg = (U)arg!;

        throw new ArgumentOutOfRangeException(name, arg!.GetType().Name, null);
    }

    protected static void Validate<T>(T commonBase, IFactory? factory) where T : class, ICommonBase
    {
        string name = nameof(factory);

        Type factoryType = NullChecked(factory, name).GetType();
        var commonBaseFactory = commonBase.GetFactory();

        if (factoryType != commonBaseFactory.GetType()) throw new ArgumentOutOfRangeException(name, factoryType.Name, null);
    }
    #endregion
    #endregion
}
