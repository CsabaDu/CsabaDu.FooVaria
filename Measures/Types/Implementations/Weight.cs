namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents a weight measure.
/// </summary>
internal sealed class Weight(IMeasureFactory factory, WeightUnit weightUnit, double quantity)
    : Measure<IWeight, double, WeightUnit>(factory, weightUnit, quantity), IWeight
{
    #region Public methods

    /// <summary>
    /// Compares the current weight to another volume.
    /// </summary>
    /// <param name="other">The volume to compare to.</param>
    /// <returns>An integer that indicates the relative order of the weights.</returns>
    public int CompareTo(IVolume? other)
    {
        return ConvertMeasure().CompareTo(other);
    }

    /// <summary>
    /// Converts the specified volume to a weight.
    /// </summary>
    /// <param name="volume">The volume to convert.</param>
    /// <returns>The converted weight.</returns>
    public IWeight ConvertFrom(IVolume volume)
    {
        return NullChecked(volume, nameof(volume)).ConvertMeasure();
    }

    /// <summary>
    /// Converts the current weight to a volume.
    /// </summary>
    /// <returns>The converted volume.</returns>
    public IVolume ConvertMeasure()
    {
        return ConvertMeasure<IVolume>(MeasureOperationMode.Multiply);
    }

    /// <summary>
    /// Determines whether the current weight is equal to another volume.
    /// </summary>
    /// <param name="other">The volume to compare to.</param>
    /// <returns>True if the weights are equal; otherwise, false.</returns>
    public bool Equals(IVolume? other)
    {
        return ConvertMeasure().Equals(other);
    }

    /// <summary>
    /// Calculates the proportional value of the current weight to another volume.
    /// </summary>
    /// <param name="other">The volume to compare to.</param>
    /// <returns>The proportional value as a decimal.</returns>
    public decimal ProportionalTo(IVolume? other)
    {
        return ConvertMeasure().ProportionalTo(other);
    }

    #endregion
}
