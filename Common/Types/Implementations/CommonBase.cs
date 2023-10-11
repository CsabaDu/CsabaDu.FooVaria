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
    #endregion

    #region Virtual methods
    public virtual void Validate(IFooVariaObject? fooVariaObject)
    {
        validate(this, fooVariaObject);

        #region Local methods
        static void validate<T>(T commonBase, IFooVariaObject? fooVariaObject) where T : class, ICommonBase
        {
            if (fooVariaObject is IFactory factory)
            {
                Validate(commonBase, factory);
            }
            else if (fooVariaObject is ICommonBase other)
            {
                Validate(commonBase, other, out T validArg);
            }
            else
            {
                throw new InvalidOperationException(null!);
            }
        }
        #endregion
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static void Validate<T>(T commonBase, ICommonBase? other, [NotNull] out T validArg) where T : class, ICommonBase
    {
        if (NullChecked(other, nameof(other)).GetType() == commonBase.GetType())
        {
            validArg = (T)other!;

            return;
        }

        throw ArgumentTypeOutOfRangeException(nameof(other), other!);
    }

    protected static void Validate<T>(T commonBase, IFactory? factory) where T : class, ICommonBase // TEST!!!
    {
        string name = nameof(factory);

        if (NullChecked(factory, name).GetType() == commonBase.GetFactory().GetType()) return;

        throw ArgumentTypeOutOfRangeException(nameof(factory), factory!);

    }
    #endregion
    #endregion
}
