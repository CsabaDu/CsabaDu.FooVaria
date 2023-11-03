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

    public virtual void Validate(IFooVariaObject? fooVariaObject, string paramName)
    {
        ValidateCommonBaseAction = () => _ = GetValidCommonBase(this, fooVariaObject!, paramName);

        Validate(this, fooVariaObject, paramName);
    }
    #endregion
    #endregion

    #region Protected methods
    protected void Validate<T>(T commonBase, IFooVariaObject? fooVariaObject, string paramName) where T : class, ICommonBase
    {
        _ = NullChecked(fooVariaObject, paramName);

        if (fooVariaObject is IFactory factory)
        {
            validateFactory();
        }
        else if (fooVariaObject is ICommonBase && ValidateCommonBaseAction != null)
        {
            validateCommonBase();
        }
        else
        {
            throw new InvalidOperationException(null!);
        }

        #region Local methods
        void validateFactory()
        {
            ValidateInterfaces(commonBase.Factory, factory, paramName);
        }

        void validateCommonBase()
        {
            ValidateCommonBaseAction.Invoke();
            ValidateCommonBaseAction = null;
        }
        #endregion
    }

    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, IFooVariaObject other, string paramName) where T : class, ICommonBase
    {
        ValidateInterfaces(commonBase, other, paramName);

        return (T)other!;
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static void ValidateInterfaces(IFooVariaObject fooVariaObject, IFooVariaObject other, string paramName)
    {
        Type type = fooVariaObject.GetType();
        IEnumerable<Type> interfaces = type.GetInterfaces();
        Type otherType = other.GetType();

        foreach (Type item in interfaces)
        {
            if (!otherType.GetInterfaces().Contains(item))
            {
                throw ArgumentTypeOutOfRangeException(paramName, other);
            }
        }
    }
    #endregion
    #endregion
}
