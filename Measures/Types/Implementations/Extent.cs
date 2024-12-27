namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents an extent measure.
/// </summary>
internal sealed class Extent(IMeasureFactory factory, ExtentUnit extentUnit, double quantity)
    : Measure<IExtent, double, ExtentUnit>(factory, extentUnit, quantity), IExtent
{
    #region Public methods

    /// <summary>
    /// Compares the current extent to another distance.
    /// </summary>
    /// <param name="other">The distance to compare to.</param>
    /// <returns>An integer that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(IDistance? other)
    {
        return ConvertMeasure().CompareTo(other);
    }

    /// <summary>
    /// Converts the specified distance to an extent.
    /// </summary>
    /// <param name="distance">The distance to convert.</param>
    /// <returns>The converted extent.</returns>
    public IExtent ConvertFrom(IDistance distance)
    {
        return NullChecked(distance, nameof(distance)).ConvertMeasure();
    }

    /// <summary>
    /// Converts the current extent to a distance.
    /// </summary>
    /// <returns>The converted distance.</returns>
    public IDistance ConvertMeasure()
    {
        return ConvertMeasure<IDistance>(MeasureOperationMode.Divide);
    }

    /// <summary>
    /// Determines whether the current extent is equal to another distance.
    /// </summary>
    /// <param name="other">The distance to compare to.</param>
    /// <returns>True if the current extent is equal to the other distance; otherwise, false.</returns>
    public bool Equals(IDistance? other)
    {
        return ConvertMeasure().Equals(other);
    }

    /// <summary>
    /// Determines whether two shape components are equal.
    /// </summary>
    /// <param name="x">The first shape component to compare.</param>
    /// <param name="y">The second shape component to compare.</param>
    /// <returns>True if the shape components are equal; otherwise, false.</returns>
    public bool Equals(IShapeComponent? x, IShapeComponent? y)
    {
        return Equals<IShapeComponent>(x, y);
    }

    /// <summary>
    /// Exchanges the current extent to the specified distance unit.
    /// </summary>
    /// <param name="distanceUnit">The distance unit to exchange to.</param>
    /// <returns>The exchanged distance, or null if not exchangeable.</returns>
    public IDistance? ExchangeTo(DistanceUnit distanceUnit)
    {
        return ConvertMeasure().ExchangeTo(distanceUnit);
    }

    /// <summary>
    /// Gets the base shape components of the current extent.
    /// </summary>
    /// <returns>An enumerable of the base shape components.</returns>
    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        yield return this;
    }

    /// <summary>
    /// Gets the hash code for the specified shape component.
    /// </summary>
    /// <param name="shapeComponent">The shape component to get the hash code for.</param>
    /// <returns>The hash code for the shape component.</returns>
    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return GetHashCode<IShapeComponent>(shapeComponent);
    }

    /// <summary>
    /// Calculates the proportion of the current extent to another distance.
    /// </summary>
    /// <param name="other">The distance to compare to.</param>
    /// <returns>The proportion of the current extent to the other distance.</returns>
    public decimal ProportionalTo(IDistance? other)
    {
        return ConvertMeasure().ProportionalTo(other);
    }
    #endregion
}
