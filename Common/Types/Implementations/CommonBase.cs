using CsabaDu.FooVaria.Common.Factories;
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
    //public abstract void Validate(ICommonBase? other);
    //public abstract void Validate(IFactory? factory);
    public virtual void Validate(IFooVariaObject? fooVariaObject)
    {
        Validate(this, fooVariaObject);
    }
    #endregion

    public static void Validate(IFactory factoryBase, IFactory? factory)
    {
        string name = nameof(factory);

        if (NullChecked(factory, name).GetType().IsInstanceOfType(factoryBase)) return;

        throw new ArgumentOutOfRangeException(name, factory!.GetType().Name, null);
    }

    #endregion

    #region Protected methods
    #region Static methods
    protected static void Validate<T>(T commonBase, ICommonBase? other, [NotNull] out T validCommonBase) where T : class, ICommonBase
    {
        if (NullChecked(other, nameof(other)).GetType() == commonBase.GetType())
        {
            validCommonBase = (T)other!;

            return;
        }

        throw TypeArgumentOutOfRangeException(nameof(other), other!);
    }

    protected static void Validate<T>(T commonBase, IFactory? factory) where T : class, ICommonBase // TEST!!!
    {
        string name = nameof(factory);

        if (NullChecked(factory, name).GetType() == commonBase.GetFactory().GetType()) return;

        throw new ArgumentOutOfRangeException(name, factory!.GetType().Name, null);

    }

    protected void Validate<T>(T commonBase, IFooVariaObject? fooVariaObject) where T : class, ICommonBase
    {
        if (fooVariaObject is IFactory factory)
        {
            var factoryBase = commonBase.GetFactory();

            Validate(factoryBase, factory);
        }
        else if (fooVariaObject is ICommonBase other)
        {
            Validate(commonBase, other, out T validCommonBase);
        }
        else
        {
            throw new InvalidOperationException(null!);
        }
    }
    #endregion
    #endregion
}
