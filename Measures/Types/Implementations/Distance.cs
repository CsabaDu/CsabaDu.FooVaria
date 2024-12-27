namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents a distance measure.
/// </summary>
internal sealed class Distance(IMeasureFactory factory, DistanceUnit distanceUnit, double quantity)
    : Measure<IDistance, double, DistanceUnit>(factory, distanceUnit, quantity), IDistance
{
    #region Public methods

    /// <summary>
    /// Compares the current distance to another extent.
    /// </summary>
    /// <param name="other">The extent to compare to.</param>
    /// <returns>An integer that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(IExtent? other)
    {
        return ConvertMeasure().CompareTo(other);
    }

    /// <summary>
    /// Converts the specified extent to a distance.
    /// </summary>
    /// <param name="extent">The extent to convert.</param>
    /// <returns>The converted distance.</returns>
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    /// <summary>
    /// Converts the current distance to an extent.
    /// </summary>
    /// <returns>The converted extent.</returns>
    public IExtent ConvertMeasure()
    {
        return ConvertMeasure<IExtent>(MeasureOperationMode.Multiply);
    }

    /// <summary>
    /// Determines whether the current distance is equal to another extent.
    /// </summary>
    /// <param name="other">The extent to compare to.</param>
    /// <returns>True if the current distance is equal to the other extent; otherwise, false.</returns>
    public bool Equals(IExtent? other)
    {
        return ConvertMeasure().Equals(other);
    }

    /// <summary>
    /// Exchanges the current distance to the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit to exchange to.</param>
    /// <returns>The exchanged extent, or null if not exchangeable.</returns>
    public IExtent? ExchangeTo(ExtentUnit extentUnit)
    {
        return ConvertMeasure().ExchangeTo(extentUnit);
    }

    /// <summary>
    /// Calculates the proportion of the current distance to another extent.
    /// </summary>
    /// <param name="other">The extent to compare to.</param>
    /// <returns>The proportion of the current distance to the other extent.</returns>
    public decimal ProportionalTo(IExtent? other)
    {
        return ConvertMeasure().ProportionalTo(other);
    }

    #endregion
}
