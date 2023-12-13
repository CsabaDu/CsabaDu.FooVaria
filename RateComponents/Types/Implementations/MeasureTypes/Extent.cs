namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Extent : Measure<IExtent, double,ExtentUnit>, IExtent
{
    #region Constructors
    internal Extent(IMeasureFactory factory, ExtentUnit extentUnit, ValueType quantity) : base(factory, extentUnit, quantity)
    {
    }
    #endregion

    #region Public methos
    public IExtent ConvertFrom(IDistance distance)
    {
        return NullChecked(distance, nameof(distance)).ConvertMeasure();
    }

    public IDistance ConvertMeasure()
    {
        return ConvertMeasure<IDistance, DistanceUnit>(ConvertMode.Divide);
    }
    #endregion
}
