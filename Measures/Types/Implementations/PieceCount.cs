namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents a measure of piece count.
/// </summary>
internal sealed class PieceCount(IMeasureFactory factory, Pieces pieces, long quantity)
    : Measure<IPieceCount, long, Pieces>(factory, pieces, quantity), IPieceCount
{
    #region Public methods

    /// <summary>
    /// Gets a custom measure with the specified pieces, exchange rate, quantity, and custom name.
    /// </summary>
    /// <param name="pieces">The pieces.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="quantity">The quantity.</param>
    /// <param name="customName">The custom name.</param>
    /// <returns>The custom measure.</returns>
    public IPieceCount? GetCustomMeasure(Pieces pieces, decimal exchangeRate, long quantity, string customName)
    {
        return (IPieceCount?)GetBaseMeasure(pieces, exchangeRate, quantity, customName);
    }

    /// <summary>
    /// Gets the next custom measure with the specified custom name, exchange rate, and quantity.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="quantity">The quantity.</param>
    /// <returns>The next custom measure.</returns>
    public IPieceCount? GetNextCustomMeasure(string customName, decimal exchangeRate, long quantity)
    {
        return (IPieceCount?)GetBaseMeasure(customName, GetMeasureUnitCode(), exchangeRate, quantity);
    }

    #endregion
}
