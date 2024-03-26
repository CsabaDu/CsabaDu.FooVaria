namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Distance(IMeasureFactory factory, DistanceUnit distanceUnit, double quantity)
    : Measure<IDistance, double, DistanceUnit>(factory, distanceUnit, quantity), IDistance
{
    #region Public methods
    public int CompareTo(IExtent? other)
    {
        return ConvertMeasure().CompareTo(other);
    }

    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        return ConvertMeasure<IExtent>(MeasureOperationMode.Multiply);
    }

    public bool Equals(IExtent? other)
    {
        return ConvertMeasure().Equals(other);
    }

    public decimal ProportionalTo(IExtent? other)
    {
        return ConvertMeasure().ProportionalTo(other);
    }
    #endregion
}
