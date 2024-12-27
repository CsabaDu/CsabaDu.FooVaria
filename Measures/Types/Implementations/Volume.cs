namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents a volume measure.
/// </summary>
internal sealed class Volume(IMeasureFactory factory, VolumeUnit volumeUnit, double quantity)
    : Measure<IVolume, double, VolumeUnit>(factory, volumeUnit, quantity), IVolume
{
    #region Public methods

    /// <summary>
    /// Compares the current volume to another weight.
    /// </summary>
    /// <param name="other">The weight to compare to.</param>
    /// <returns>An integer that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(IWeight? other)
    {
        return ConvertMeasure().CompareTo(other);
    }

    /// <summary>
    /// Converts the specified weight to a volume.
    /// </summary>
    /// <param name="weight">The weight to convert.</param>
    /// <returns>The converted volume.</returns>
    public IVolume ConvertFrom(IWeight weight)
    {
        return NullChecked(weight, nameof(weight)).ConvertMeasure();
    }

    /// <summary>
    /// Converts the current volume to a weight.
    /// </summary>
    /// <returns>The converted weight.</returns>
    public IWeight ConvertMeasure()
    {
        return ConvertMeasure<IWeight>(MeasureOperationMode.Divide);
    }

    /// <summary>
    /// Determines whether the current volume is equal to another weight.
    /// </summary>
    /// <param name="other">The weight to compare to.</param>
    /// <returns>true if the current volume is equal to the other weight; otherwise, false.</returns>
    public bool Equals(IWeight? other)
    {
        return ConvertMeasure().Equals(other);
    }

    /// <summary>
    /// Gets the spread measure.
    /// </summary>
    /// <returns>The spread measure.</returns>
    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    /// <summary>
    /// Validates the specified spread measure.
    /// </summary>
    /// <param name="spreadMeasure">The spread measure to validate.</param>
    /// <param name="paramName">The name of the parameter associated with the spread measure.</param>
    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }

    /// <summary>
    /// Calculates the proportion of the current volume to another weight.
    /// </summary>
    /// <param name="other">The weight to compare to.</param>
    /// <returns>The proportion of the current volume to the other weight.</returns>
    public decimal ProportionalTo(IWeight? other)
    {
        return ConvertMeasure().ProportionalTo(other);
    }
    #endregion
}
