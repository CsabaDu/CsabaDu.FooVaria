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

    public virtual void Validate(IRootObject? rootObject, string paramName)
    {
        ValidateCommonBaseAction = () => _ = GetValidCommonBase(this, rootObject!, paramName);

        Validate(this, rootObject, paramName);
    }
    #endregion
    #endregion

    #region Protected methods
    protected void Validate<T>(T commonBase, IRootObject? rootObject, string paramName) where T : class, ICommonBase
    {
        _ = NullChecked(rootObject, paramName);

        if (rootObject is IFactory factory)
        {
            ValidateInterfaces(commonBase.Factory, factory, paramName);
        }
        else if (rootObject is ICommonBase && ValidateCommonBaseAction != null)
        {
            ValidateCommonBaseAction();

            ValidateCommonBaseAction = null;
        }
        else
        {
            throw new InvalidOperationException(null!);
        }
    }

    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, IRootObject other, string paramName) where T : class, ICommonBase
    {
        ValidateInterfaces(commonBase, other, paramName);

        return (T)other!;
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static void ValidateInterfaces(IRootObject rootObject, IRootObject other, string paramName)
    {
        Type type = rootObject.GetType();
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
