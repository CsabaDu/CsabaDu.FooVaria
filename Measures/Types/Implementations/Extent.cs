namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Extent(IMeasureFactory factory, ExtentUnit extentUnit, double quantity)
    : Measure<IExtent, double, ExtentUnit>(factory, extentUnit, quantity), IExtent
{
    #region Public methos
    public IExtent ConvertFrom(IDistance distance)
    {
        return NullChecked(distance, nameof(distance)).ConvertMeasure();
    }

    public IDistance ConvertMeasure()
    {
        return ConvertMeasure<IDistance>(MeasureOperationMode.Divide);
    }
    #endregion
}
