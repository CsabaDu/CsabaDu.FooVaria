namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

/// <summary>
/// Represents a custom measurement that is derived from the <see cref="Measurement"> class and implements the <see cref="ICustomMeasurement"/> interface.
/// </summary>
/// <param name="factory">The factory used to create <see cref="IMeasurement"/> instances.</param>
/// <param name="measureUnit">The unit of measurement as an enumeration.</param>
internal sealed class CustomMeasurement(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit), ICustomMeasurement
{
    #region Public methods
    /// <summary>
    /// Gets a custom measurement for the specified measure unit, exchange rate, and custom name.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="customName">The custom name.</param>
    /// <returns>The custom measurement if found; otherwise, null.</returns>
    public ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return (ICustomMeasurement?)GetFactory().Create(measureUnit, exchangeRate, customName);
    }

    /// <summary>
    /// Gets the next custom measurement for the specified custom name, measure unit code, and exchange rate.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="customMeasureUnitCode">The custom measure unit code.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <returns>The next custom measurement if found; otherwise, null.</returns>
    public ICustomMeasurement? GetNextCustomMeasurement(string customName, MeasureUnitCode customMeasureUnitCode, decimal exchangeRate)
    {
        return (ICustomMeasurement?)GetFactory().Create(customName, customMeasureUnitCode, exchangeRate);
    }

    /// <summary>
    /// Gets the next custom measurement for the specified custom name and exchange rate.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <returns>The next custom measurement if found; otherwise, null.</returns>
    public ICustomMeasurement? GetNextCustomMeasurement(string customName, decimal exchangeRate)
    {
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode();

        return GetNextCustomMeasurement(customName, measureUnitCode, exchangeRate);
    }

    /// <summary>
    /// Gets the collection of measure units that are not used.
    /// </summary>
    /// <returns>A collection of measure units that are not used.</returns>
    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        return GetNotUsedCustomMeasureUnits(GetMeasureUnitCode());
    }
    #endregion
}
