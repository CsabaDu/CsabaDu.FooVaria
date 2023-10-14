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

    #region Protected properties
    protected Action? ValidateCommonBaseAction { private get; set; }
    #endregion
    #endregion

    #region Public methods
    #region Virtual methods
    public virtual IFactory GetFactory()
    {
        return Factory;
    }

    public virtual void Validate(IFooVariaObject? fooVariaObject)
    {
        ValidateCommonBaseAction = () => _ = GetValidCommonBase(this, fooVariaObject!);

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
            validateFactory(commonBase, factory);
        }
        else if (fooVariaObject is ICommonBase)
        {
            ValidateCommonBaseAction!.Invoke();
        }
        else
        {
            throw new InvalidOperationException(null!);
        }

        #region Local methods
        static void validateFactory(T commonBase, IFactory factory)
        {
            Type commonBaseFactoryType = commonBase.Factory.GetType();

            ValidateInterfaces(commonBaseFactoryType.GetInterfaces(), factory, nameof(factory));
        }
        #endregion
    }

    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, IFooVariaObject other) where T : class, ICommonBase
    {
        Type commonBaseType = commonBase.GetType();

        ValidateInterfaces(commonBaseType.GetInterfaces(), other, nameof(other));

        return (T)other!;
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static void ValidateInterfaces(IEnumerable<Type> interfaces, IFooVariaObject fooVariaObject, string name)
    {
        Type type = fooVariaObject.GetType();

        foreach (Type item in interfaces)
        {
            if (!type.GetInterfaces().Contains(item))
            {
                throw ArgumentTypeOutOfRangeException(name, fooVariaObject);
            }
        }
    }
    #endregion
    #endregion
}
