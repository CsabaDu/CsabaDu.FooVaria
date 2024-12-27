namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents a cash measure.
/// </summary>
/// <param name="factory">The measure factory.</param>
/// <param name="currency">The currency of the cash.</param>
/// <param name="quantity">The quantity of the cash.</param>
internal sealed class Cash(IMeasureFactory factory, Currency currency, decimal quantity)
    : Measure<ICash, decimal, Currency>(factory, currency, quantity), ICash
{
    #region Public methods

    /// <summary>
    /// Gets a custom cash measure with the specified currency, exchange rate, quantity, and custom name.
    /// </summary>
    /// <param name="currency">The currency of the custom measure.</param>
    /// <param name="exchangeRate">The exchange rate for the custom measure.</param>
    /// <param name="quantity">The quantity of the custom measure.</param>
    /// <param name="customName">The custom name for the measure.</param>
    /// <returns>The custom cash measure, or null if not available.</returns>
    public ICash? GetCustomMeasure(Currency currency, decimal exchangeRate, decimal quantity, string customName)
    {
        return (ICash?)GetBaseMeasure(currency, exchangeRate, quantity, customName);
    }

    /// <summary>
    /// Gets the next custom cash measure with the specified custom name, exchange rate, and quantity.
    /// </summary>
    /// <param name="customName">The custom name for the measure.</param>
    /// <param name="exchangeRate">The exchange rate for the custom measure.</param>
    /// <param name="quantity">The quantity of the custom measure.</param>
    /// <returns>The next custom cash measure, or null if not available.</returns>
    public ICash? GetNextCustomMeasure(string customName, decimal exchangeRate, decimal quantity)
    {
        return (ICash?)GetBaseMeasure(customName, GetMeasureUnitCode(), exchangeRate, quantity);
    }

    #endregion
}
