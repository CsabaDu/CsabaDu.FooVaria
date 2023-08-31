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
    #endregion

    #region Public methods
    public override IArea GetMeasure(IBaseMeasure baseMeasure)
    {
        ValidateBaseMeasure(baseMeasure);

        return (IArea)base.GetMeasure(baseMeasure);
    }

    public IArea GetMeasure(double quantity, AreaUnit measureUnit)
    {
        return (IArea)base.GetMeasure(quantity, measureUnit);
    }

    public IArea GetMeasure(IArea? other = null)
    {
        return (IArea)base.GetMeasure(other);
    }

    public IArea GetMeasure(double quantity, string name)
    {
        return(IArea)base.GetMeasure(quantity, name);
    }

    public IArea GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (IArea)base.GetMeasure(quantity, measurement);
    }

    public override Enum GetMeasureUnit()
    {
        return (AreaUnit)Measurement.MeasureUnit;
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
