namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Distance(IMeasureFactory factory, DistanceUnit distanceUnit, double quantity)
    : Measure<IDistance, double, DistanceUnit>(factory, distanceUnit, quantity), IDistance
{
    #region Public methods
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        return ConvertMeasure<IExtent>(MeasureOperationMode.Multiply);
    }
    #endregion
}
