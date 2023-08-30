namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Distance : Measure, IDistance
{
    internal Distance(IDistance distance) : base(distance)
    {
    }

    internal Distance(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Distance(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        decimal quantity = DefaultQuantity * DistancePerExtent;

        return (IExtent)GetMeasure(quantity, default(ExtentUnit));

    }

    public IDistance GetDistance(IBaseMeasure baseMeasure)
    {
        return (IDistance)GetMeasure(baseMeasure);
    }

    public IDistance GetMeasure(double quantity, DistanceUnit measureUnit)
    {
        return (IDistance)base.GetMeasure(quantity, measureUnit);
    }

    public IDistance GetMeasure(double quantity, string name)
    {
        return (IDistance)base.GetMeasure(quantity, name);
    }

    public IDistance GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (IDistance)base.GetMeasure(quantity, measurement);
    }

    public IDistance GetMeasure(IDistance? other = null)
    {
        return (IDistance)base.GetMeasure(other);
    }

    public override Enum GetMeasureUnit()
    {
        return (DistanceUnit)Measurement.MeasureUnit;
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }

    //public IDistance GetDistance(double quantity, DistanceUnit distanceUnit)
    //{
    //    return (IDistance)GetMeasure(quantity, distanceUnit);
    //}

    //public IDistance GetDistance(ValueType quantity, string name)
    //{
    //    return (IDistance)GetMeasure(quantity, name);
    //}

    //public IDistance GetDistance(ValueType quantity, IMeasurement? measurement = null)
    //{
    //    return (IDistance)GetMeasure(quantity, measurement);
    //}

    //public IDistance GetDistance(IDistance? other = null)
    //{
    //    return (IDistance)GetMeasure(other);
    //}
}
