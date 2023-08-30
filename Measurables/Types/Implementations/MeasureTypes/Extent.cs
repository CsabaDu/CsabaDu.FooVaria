namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Extent : Measure, IExtent
{
    #region Constructors
    internal Extent(IExtent extent) : base(extent)
    {
    }

    internal Extent(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Extent(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
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
        decimal quantity = DefaultQuantity / CrossMeasureRatio;

        return (IDistance)GetMeasure(quantity, default(DistanceUnit));
    }

    public IExtent GetExtent(IBaseMeasure baseMeasure)
    {
        return (IExtent)GetMeasure(baseMeasure);
    }

    public IExtent GetMeasure(double quantity, ExtentUnit measureUnit)
    {
        return (IExtent)base.GetMeasure(quantity, measureUnit);
    }

    public IExtent GetMeasure(double quantity, string name)
    {
        return (IExtent)base.GetMeasure(quantity, name);
    }

    public IExtent GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (IExtent)base.GetMeasure(quantity, measurement);
    }

    public IExtent GetMeasure(IExtent? other = null)
    {
        return (IExtent)base.GetMeasure(other);
    }

    public override Enum GetMeasureUnit()
    {
        return (ExtentUnit)Measurement.MeasureUnit;
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }
    #endregion
}


//public IExtent GetExtent(double quantity, ExtentUnit extentUnit)
//{
//    return (IExtent)GetMeasure(quantity, extentUnit);
//}

//public IExtent GetExtent(ValueType quantity, string name)
//{
//    return (IExtent)GetMeasure(quantity, name);
//}

//public IExtent GetExtent(ValueType quantity, IMeasurement? measurement = null)
//{
//    return (IExtent)GetMeasure(quantity, measurement);
//}

//public IExtent GetExtent(IExtent? other = null)
//{
//    return (IExtent)GetMeasure(other);
//}