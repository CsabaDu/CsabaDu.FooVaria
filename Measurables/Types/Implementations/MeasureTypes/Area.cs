namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Area : Measure, IArea
{
    #region Constructors
    internal Area(IArea area) : base(area)
    {
    }

    internal Area(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Area(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }

    public Area(IMeasureFactory measureFactory, IBaseMeasure baseMeasure) : base(measureFactory, baseMeasure)
    {
    }
    #endregion

    #region Public methods
    public override IArea GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IArea GetMeasure(double quantity, AreaUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IArea GetMeasure(IArea? other = null)
    {
        return GetMeasure(this, other as Area);
    }

    public IArea GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IArea GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return GetMeasure();
    }
    #endregion
}
