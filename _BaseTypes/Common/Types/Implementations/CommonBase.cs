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
    /// <param name="rootObject">The root object to be checked for null.</param>
    /// <param name="paramName">The name of the parameter to be checked for null.</param>
    /// <exception cref="ArgumentNullException">Thrown when the rootObject is null.</exception>
    protected CommonBase(IRootObject rootObject, string paramName)
    {
        _ = NullChecked(rootObject, paramName);
    }
    #endregion

    #region Public methods
    #region Abstract methods
    /// <summary>
    /// Gets the factory associated with the current instance.
    /// </summary>
    /// <returns>An instance of <see cref="IFactory"/>.</returns>
    public abstract IFactory GetFactory();
    #endregion
    #endregion
}
