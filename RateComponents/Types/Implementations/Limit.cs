namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

/// <summary>
/// Represents a limit component of a rate with a specific quantity and limit mode.
/// </summary>
/// <param name="factory">The factory used to create limit components.</param>
/// <param name="measurement">The measurement associated with the limit component.</param>
/// <param name="quantity">The quantity of the limit component.</param>
/// <param name="limitMode">The mode of the limit component.</param>
internal sealed class Limit(ILimitFactory factory, IMeasurement measurement, ulong quantity, LimitMode limitMode) : RateComponent<ILimit>(factory, measurement), ILimit
{
    #region Properties
    /// <summary>
    /// Gets the mode of the limit component.
    /// </summary>
    public LimitMode LimitMode { get; init; } = Defined(limitMode, nameof(limitMode));

    /// <summary>
    /// Gets the quantity of the limit component.
    /// </summary>
    public ulong Quantity { get; init; } = quantity;

    /// <summary>
    /// Gets the factory used to create limit components.
    /// </summary>
    public ILimitFactory Factory { get; init; } = factory;
    #endregion

    #region Public methods
    /// <summary>
    /// Determines whether the specified limits are equal.
    /// </summary>
    /// <param name="x">The first limit to compare.</param>
    /// <param name="y">The second limit to compare.</param>
    /// <returns>True if the specified limits are equal; otherwise, false.</returns>
    public bool Equals(ILimit? x, ILimit? y)
    {
        return base.Equals(x, y);
    }

    /// <summary>
    /// Gets the base measure with the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity of the base measure.</param>
    /// <returns>The base measure with the specified quantity.</returns>
    public ILimit GetBaseMeasure(ulong quantity)
    {
        return GetRateComponent(quantity);
    }

    /// <summary>
    /// Gets the hash code for the specified limit.
    /// </summary>
    /// <param name="limit">The limit to get the hash code for.</param>
    /// <returns>The hash code for the specified limit.</returns>
    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return base.GetHashCode(limit);
    }

    /// <summary>
    /// Gets a limit with the specified parameters.
    /// </summary>
    /// <param name="measureUnit">The measure unit of the limit.</param>
    /// <param name="exchangeRate">The exchange rate of the limit.</param>
    /// <param name="quantity">The quantity of the limit.</param>
    /// <param name="customName">The custom name of the limit.</param>
    /// <param name="limitMode">The mode of the limit.</param>
    /// <returns>The limit with the specified parameters.</returns>
    public ILimit? GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        return Factory.Create(measureUnit, exchangeRate, quantity, customName, limitMode);
    }

    /// <summary>
    /// Gets a limit with the specified measurement, quantity, and limit mode.
    /// </summary>
    /// <param name="measurement">The measurement of the limit.</param>
    /// <param name="quantity">The quantity of the limit.</param>
    /// <param name="limitMode">The mode of the limit.</param>
    /// <returns>The limit with the specified measurement, quantity, and limit mode.</returns>
    public ILimit GetLimit(IMeasurement measurement, ulong quantity, LimitMode limitMode)
    {
        return Factory.Create(measurement, quantity, limitMode);
    }

    /// <summary>
    /// Gets a limit with the specified base measure and limit mode.
    /// </summary>
    /// <param name="baseMeasure">The base measure of the limit.</param>
    /// <param name="limitMode">The mode of the limit.</param>
    /// <returns>The limit with the specified base measure and limit mode.</returns>
    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        return Factory.Create(baseMeasure, limitMode);
    }

    /// <summary>
    /// Gets a limit with the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity of the limit.</param>
    /// <returns>The limit with the specified quantity.</returns>
    public ILimit GetLimit(ulong quantity)
    {
        return Factory.Create(this, quantity);
    }

    /// <summary>
    /// Gets a limit with the specified name, quantity, and limit mode.
    /// </summary>
    /// <param name="name">The name of the limit.</param>
    /// <param name="quantity">The quantity of the limit.</param>
    /// <param name="limitMode">The mode of the limit.</param>
    /// <returns>The limit with the specified name, quantity, and limit mode.</returns>
    public ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode)
    {
        return Factory.Create(name, quantity, limitMode);
    }

    /// <summary>
    /// Gets a limit with the specified measure unit, quantity, and limit mode.
    /// </summary>
    /// <param name="measureUnit">The measure unit of the limit.</param>
    /// <param name="quantity">The quantity of the limit.</param>
    /// <param name="limitMode">The mode of the limit.</param>
    /// <returns>The limit with the specified measure unit, quantity, and limit mode.</returns>
    public ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        return Factory.Create(measureUnit, quantity, limitMode);
    }

    /// <summary>
    /// Gets a limit with the specified custom name, measure unit code, exchange rate, quantity, and limit mode.
    /// </summary>
    /// <param name="customName">The custom name of the limit.</param>
    /// <param name="measureUnitCode">The measure unit code of the limit.</param>
    /// <param name="exchangeRate">The exchange rate of the limit.</param>
    /// <param name="quantity">The quantity of the limit.</param>
    /// <param name="limitMode">The mode of the limit.</param>
    /// <returns>The limit with the specified custom name, measure unit code, exchange rate, quantity, and limit mode.</returns>
    public ILimit? GetLimit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        return Factory.Create(customName, measureUnitCode, exchangeRate, quantity, limitMode);
    }

    /// <summary>
    /// Gets a new limit based on the specified other limit.
    /// </summary>
    /// <param name="other">The other limit to base the new limit on.</param>
    /// <returns>The new limit based on the specified other limit.</returns>
    public ILimit GetNew(ILimit other)
    {
        return Factory.CreateNew(other);
    }

    /// <summary>
    /// Gets the quantity of the limit component.
    /// </summary>
    /// <returns>The quantity of the limit component.</returns>
    public ulong GetQuantity()
    {
        return Quantity;
    }

    /// <summary>
    /// Determines whether the specified limitable object is included in the limit.
    /// </summary>
    /// <param name="limitable">The limitable object to check.</param>
    /// <returns>True if the specified limitable object is included in the limit; otherwise, false.</returns>
    public bool? Includes(IBaseMeasure? limitable)
    {
        return limitable is null ?
            false
            : limitable?.FitsIn(this);
    }

    #region Override methods
    /// <summary>
    /// Gets the base quantity of the limit component.
    /// </summary>
    /// <returns>The base quantity of the limit component.</returns>
    public override ValueType GetBaseQuantity()
    {
        return Quantity;
    }

    /// <summary>
    /// Gets the factory used to create limit components.
    /// </summary>
    /// <returns>The factory used to create limit components.</returns>
    public override ILimitFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets the mode of the limit component.
    /// </summary>
    /// <returns>The mode of the limit component.</returns>
    public override LimitMode? GetLimitMode()
    {
        return LimitMode;
    }
    #endregion
    #endregion
}
