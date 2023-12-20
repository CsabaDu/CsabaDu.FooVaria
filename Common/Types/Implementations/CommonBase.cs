namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class CommonBase : ICommonBase
{
    #region Constructors
    protected CommonBase(IFactory factory)
    {
        Factory = NullChecked(factory, nameof(factory));
    }

    protected CommonBase(IFactory factory, params ICommonBase[] commonBases)
    {
        Factory = NullChecked(factory, nameof(factory));

        _ = NullChecked(commonBases, nameof(commonBases));
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

    public virtual void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateCommonBase, paramName);

        #region Local methods
        void validateCommonBase()
        {
            _ = GetValidCommonBase(this, rootObject!, paramName);
        }
        #endregion
    }
    #endregion
    #endregion

    #region Protected methods
    protected static void Validate<T>(T commonBase, IRootObject? rootObject, Action validateCommonBase, string paramName)
        where T : class, ICommonBase
    {
        _ = NullChecked(rootObject, paramName);

        if (rootObject is IFactory factory)
        {
            validateFactory();
        }
        else if (rootObject is ICommonBase)
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
        #endregion

    }

    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, IRootObject other, string paramName)
        where T : class, ICommonBase
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
        if (AreValidInterfaces(rootObject, other)) return;

        throw ArgumentTypeOutOfRangeException(paramName, other);

        //Type type = rootObject.GetType();
        //IEnumerable<Type> interfaces = type.GetInterfaces();
        //Type otherType = other.GetType();
        //IEnumerable<Type> otherInterfaces = otherType.GetInterfaces();

        //foreach (Type item in interfaces)
        //{
        //    if (!otherType.GetInterfaces().Contains(item))
        //    {
        //        throw ArgumentTypeOutOfRangeException(paramName, other);
        //    }
        //}
    }

    private static bool AreValidInterfaces(IRootObject rootObject, IRootObject other)
    {
        IEnumerable<Type> interfaces = rootObject.GetType().GetInterfaces();
        IEnumerable<Type> otherInterfaces = other.GetType().GetInterfaces();

        return otherInterfaces.Intersect(interfaces).SequenceEqual(otherInterfaces);
    }
    #endregion
    #endregion
}
