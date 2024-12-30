namespace CsabaDu.FooVaria.BaseTypes.Common.Types.Implementations;

/// <summary>
/// Represents an abstract base class that implements the <see cref="ICommonBase"/> interface.
/// </summary>
public abstract class CommonBase : ICommonBase
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="CommonBase"/> class.
    /// </summary>
    /// <param name="rootObject">A <see cref="IRootObject"/> child to be checked for null.
    /// The rootObject can be either an <see cref="IFactory"/> child instance associated with the current type,
    /// or an instance of the current <see cref="ICommonBase"/> child type in case of copy constructor.</param>
    /// <param name="paramName">The name of the <see cref="IRootObject"/> parameter to be checked.</param>
    /// <exception cref="ArgumentNullException">Thrown when the rootObject is null.</exception>
    protected CommonBase(IRootObject rootObject, string paramName)
    {
        _ = NullChecked(rootObject, paramName);
    }
    #endregion

    #region Public methods
    #region Abstract methods
    /// <summary>
    /// Gets the factory associated with the current <see cref="ICommonBase"/> child instance.
    /// </summary>
    /// <returns>An instance of <see cref="IFactory"/>.</returns> child instance.
    public abstract IFactory GetFactory();
    #endregion
    #endregion
}
