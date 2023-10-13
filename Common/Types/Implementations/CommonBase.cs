using CsabaDu.FooVaria.Common.Factories;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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
    #region Virtual methods
    public virtual IFactory GetFactory()
    {
        return Factory;
    }

    public virtual void Validate(IFooVariaObject? fooVariaObject)
    {
        validate(this, fooVariaObject);

        #region Local methods
        void validate<T>(T commonBase, IFooVariaObject? fooVariaObject) where T : class, ICommonBase
        {
            _ = NullChecked(fooVariaObject, nameof(fooVariaObject));

            if (fooVariaObject is IFactory factory)
            {
                ValidateFactory(commonBase, factory);
            }
            else if (fooVariaObject is ICommonBase other)
            {
                _ = GetValidCommonBase(commonBase, other);
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
    protected virtual void Validate<T>(T commonBase, IFooVariaObject? fooVariaObject) where T : class, ICommonBase
    {
        _ = NullChecked(fooVariaObject, nameof(fooVariaObject));

        if (fooVariaObject is IFactory factory)
        {
            ValidateFactory(commonBase, factory);
        }
        else if (fooVariaObject is ICommonBase other)
        {
            _ = GetValidCommonBase(commonBase, other);
        }
        else
        {
            throw new InvalidOperationException(null!);
        }
    }

    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, ICommonBase? other) where T : class, ICommonBase
    {
        string name = nameof(other);
        Type commonBaseType = commonBase.GetType();
        Type otherType = NullChecked(other, name).GetType();

        foreach (Type item in commonBaseType.GetInterfaces())
        {
            if (!otherType.GetInterfaces().Contains(item))
            {
                throw ArgumentTypeOutOfRangeException(name, other!);
            }
        }

        return (T)other!;
    }

    private static void ValidateFactory<T>(T commonBase, IFactory? factory) where T : class, ICommonBase
    {
        string name = nameof(factory);
        Type commonBaseFactoryType = commonBase.Factory.GetType();
        Type factoryType = NullChecked(factory, name).GetType();

        foreach (Type item in factoryType.GetInterfaces())
        {
            if (!commonBaseFactoryType.GetInterfaces().Contains(item))
            {
                throw ArgumentTypeOutOfRangeException(name, factory!);
            }
        }
    }
    #endregion
    #endregion
}
