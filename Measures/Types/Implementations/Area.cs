namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents an area measure.
/// </summary>
internal sealed class Area(IMeasureFactory factory, AreaUnit areaUnit, double quantity)
    : Measure<IArea, double, AreaUnit>(factory, areaUnit, quantity), IArea
{
    #region Public methods

    /// <summary>
    /// Gets the spread measure.
    /// </summary>
    /// <returns>The spread measure.</returns>
    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    /// <summary>
    /// Validates the spread measure.
    /// </summary>
    /// <param name="spreadMeasure">The spread measure to validate.</param>
    /// <param name="paramName">The parameter name associated with the spread measure.</param>
    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }

    #endregion
}
