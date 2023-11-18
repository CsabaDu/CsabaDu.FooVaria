namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Area : Measure<IArea, double, AreaUnit>, IArea
{
    #region Constructors
    internal Area(IMeasureFactory factory, ValueType quantity, AreaUnit areaUnit) : base(factory, quantity, areaUnit)
    {
    }
    #endregion

    #region Public methods

    public MeasureUnitTypeCode GetMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
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
