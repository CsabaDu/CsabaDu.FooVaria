namespace CsabaDu.FooVaria.BaseTypes.Common.Types
{
    /// <summary>
    /// Represents a common base type interface that extends the IRootObject interface.
    /// </summary>
    public interface ICommonBase : IRootObject
    {
        /// <summary>
        /// Gets the factory associated with the current <see cref="ICommonBase"/> child type.
        /// </summary>
        /// <returns>An instance of IFactory.</returns>
        IFactory GetFactory();
    }

    /// <summary>
    /// Represents a generic common base type interface that extends the ICommonBase interface.
    /// </summary>
    /// <typeparam name="TSelf">The type that implements the <see cref="ICommonBase"/> interface.</typeparam>
    public interface ICommonBase<TSelf> : ICommonBase
        where TSelf : class, ICommonBase
    {
        /// <summary>
        /// Creates a new instance of the implementing type based on another instance.
        /// </summary>
        /// <param name="other">The instance to base the new instance on.</param>
        /// <returns>A new instance of the implementing type.</returns>
        TSelf GetNew(TSelf other);
    }
}
