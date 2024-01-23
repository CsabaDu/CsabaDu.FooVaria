namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Extent : Measure<IExtent, double,ExtentUnit>, IExtent
{
    #region Constructors
    internal Extent(IMeasureFactory factory, ExtentUnit extentUnit, double quantity) : base(factory, extentUnit, quantity)
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
        return ConvertMeasure<IDistance>(MeasureOperationMode.Divide);
    }
    #endregion
}
