namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Area(IMeasureFactory factory, AreaUnit areaUnit, double quantity)
    : Measure<IArea, double, AreaUnit>(factory, areaUnit, quantity), IArea
{
    #region Public methods
    public MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        return GetMeasureUnitCode();
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }
    #endregion
}
