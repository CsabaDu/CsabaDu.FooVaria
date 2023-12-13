namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Area : Measure<IArea, double, AreaUnit>, IArea
{
    #region Constructors
    internal Area(IMeasureFactory factory, AreaUnit areaUnit, ValueType quantity) : base(factory, areaUnit, quantity)
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

    public override void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidateSpreadQuantity(quantity, paramName);
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }
    #endregion
}
