using CsabaDu.FooVaria.Common.Factories;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.AccessControl;

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

    protected Action ValidateCommonBase { private get; set; }

    public virtual void Validate(IFooVariaObject? fooVariaObject)
    {
        ValidateCommonBase = () => _ = GetValidCommonBase(this, (ICommonBase)fooVariaObject!);

        Validate(this, fooVariaObject);
    }
    #endregion
    #endregion

    #region Protected methods
    protected void Validate<T>(T commonBase, IFooVariaObject? fooVariaObject) where T : class, ICommonBase
    {
        _ = NullChecked(fooVariaObject, nameof(fooVariaObject));

        if (fooVariaObject is IFactory factory)
        {
            ValidateFactory(commonBase, factory);
        }
        else if (fooVariaObject is ICommonBase)
        {
            ValidateCommonBase.Invoke();
        }
        else
        {
            throw new InvalidOperationException(null!);
        }
    }

    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, IFooVariaObject other) where T : class, ICommonBase
    {
        string name = nameof(other);
        Type commonBaseType = commonBase.GetType();
        Type otherType = other.GetType();

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
