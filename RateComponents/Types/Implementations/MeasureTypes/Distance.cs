namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Distance : Measure<IDistance, double, DistanceUnit>, IDistance
{
    #region Constructors
    internal Distance(IMeasureFactory factory, DistanceUnit distanceUnit, ValueType quantity) : base(factory, distanceUnit, quantity)
    {
    }
    #endregion

    #region Public methods
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        return ConvertMeasure<IExtent>(ConvertMode.Multiply);
    }
    #endregion
}
