namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

/// <summary>
/// Represents a denominator component of a rate.
/// </summary>
/// <param name="factory">The factory used to create denominator components.</param>
/// <param name="measurement">The measurement associated with the denominator component.</param>
/// <param name="quantity">The quantity of the denominator component.</param>
internal sealed class Denominator(IDenominatorFactory factory, IMeasurement measurement, decimal quantity) : RateComponent<IDenominator>(factory, measurement), IDenominator
{
    #region Properties
    /// <summary>
    /// Gets the quantity of the denominator.
    /// </summary>
    public decimal Quantity { get; init; } = GetDenominatorQuantity(quantity);

    /// <summary>
    /// Gets the factory used to create denominators.
    /// </summary>
    public IDenominatorFactory Factory { get; init; } = factory;
    #endregion

    #region Public methods
    /// <summary>
    /// Gets the base measure for the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The base measure.</returns>
    public IDenominator GetBaseMeasure(decimal quantity)
    {
        return (IDenominator)GetBaseMeasure((ValueType)quantity);
    }

    /// <summary>
    /// Gets the denominator for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The denominator.</returns>
    public IDenominator GetDenominator(Enum measureUnit)
    {
        return Factory.Create(measureUnit);
    }

    /// <summary>
    /// Gets the denominator for the specified name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The denominator.</returns>
    public IDenominator GetDenominator(string name)
    {
        return Factory.Create(name);
    }

    /// <summary>
    /// Gets the denominator for the specified measurement.
    /// </summary>
    /// <param name="measurement">The measurement.</param>
    /// <returns>The denominator.</returns>
    public IDenominator GetDenominator(IMeasurement measurement)
    {
        return Factory.Create(measurement);
    }

    /// <summary>
    /// Gets the denominator for the specified base measure and quantity.
    /// </summary>
    /// <param name="baseMeasure">The base measure.</param>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The denominator.</returns>
    public IDenominator GetDenominator(IBaseMeasure baseMeasure, ValueType quantity)
    {
        return Factory.Create(baseMeasure, quantity);
    }

    /// <summary>
    /// Gets a new denominator based on another denominator.
    /// </summary>
    /// <param name="other">The other denominator.</param>
    /// <returns>The new denominator.</returns>
    public IDenominator GetNew(IDenominator other)
    {
        return Factory.CreateNew(other);
    }

    #region Override methods
    /// <summary>
    /// Gets the base quantity.
    /// </summary>
    /// <returns>The base quantity.</returns>
    public override ValueType GetBaseQuantity()
    {
        return Quantity;
    }

    /// <summary>
    /// Gets the factory used to create denominators.
    /// </summary>
    /// <returns>The factory.</returns>
    public override IDenominatorFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets the limit mode.
    /// </summary>
    /// <returns>The limit mode.</returns>
    public override LimitMode? GetLimitMode()
    {
        return default;
    }

    /// <summary>
    /// Validates the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity.</param>
    /// <param name="paramName">The parameter name.</param>
    public override void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidatePositiveQuantity(quantity, paramName);
    }

    /// <summary>
    /// Gets the denominator for the specified measurement and quantity.
    /// </summary>
    /// <param name="measurement">The measurement.</param>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The denominator.</returns>
    public IDenominator GetDenominator(IMeasurement measurement, decimal quantity)
    {
        return (IDenominator)GetBaseMeasure(measurement, quantity);
    }

    /// <summary>
    /// Gets the quantity of the denominator.
    /// </summary>
    /// <returns>The quantity.</returns>
    public decimal GetQuantity()
    {
        return Quantity;
    }

    /// <summary>
    /// Gets the base measure for the specified measure unit and quantity.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The base measure.</returns>
    public IDenominator GetBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        return Factory.Create(measureUnit, quantity);
    }

    /// <summary>
    /// Gets the base measure for the specified name and quantity.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The base measure.</returns>
    public IDenominator GetBaseMeasure(string name, ValueType quantity)
    {
        return Factory.Create(name, quantity);
    }

    /// <summary>
    /// Gets the base measure for the specified measure unit, exchange rate, quantity, and custom name.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="quantity">The quantity.</param>
    /// <param name="customName">The custom name.</param>
    /// <returns>The base measure.</returns>
    public IDenominator? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        return Factory.Create(measureUnit, exchangeRate, quantity, customName);
    }

    /// <summary>
    /// Gets the base measure for the specified custom name, measure unit code, exchange rate, and quantity.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The base measure.</returns>
    public IDenominator? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        return Factory.Create(customName, measureUnitCode, exchangeRate, quantity);
    }

    /// <summary>
    /// Gets the base measure for the specified base measure.
    /// </summary>
    /// <param name="baseMeasure">The base measure.</param>
    /// <returns>The base measure.</returns>
    public IDenominator GetBaseMeasure(IBaseMeasure baseMeasure)
    {
        return Factory.Create(baseMeasure);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    /// <summary>
    /// Gets the denominator quantity, ensuring it is positive.
    /// </summary>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The denominator quantity.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the quantity is not positive.</exception>
    private static decimal GetDenominatorQuantity(decimal quantity)
    {
        return quantity > 0 ?
            quantity
            : throw QuantityArgumentOutOfRangeException(quantity);
    }
    #endregion
    #endregion
}
