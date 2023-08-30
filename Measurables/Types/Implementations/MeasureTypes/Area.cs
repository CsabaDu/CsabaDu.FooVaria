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
    public IArea GetArea(IBaseMeasure baseMeasure)
    {
        return (IArea)GetMeasure(baseMeasure);
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

    //public IArea GetArea(ValueType quantity, string name)
    //{
    //    return (IArea)GetMeasure(quantity, name);
    //}
    //public IArea GetArea(ValueType quantity, IMeasurement? measurement = null)
    //{
    //    return (IArea)GetMeasure(quantity, measurement);
    //}
    //public IArea GetArea(double quantity, AreaUnit areaUnit)
    //{
    //    return (IArea)GetMeasure(quantity, areaUnit);
    //}
    //public IArea GetArea(IArea? other = null)
    //{
    //    return (IArea)GetMeasure(other);
    //}
