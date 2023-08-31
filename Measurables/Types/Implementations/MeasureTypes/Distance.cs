namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Distance : Measure, IDistance
{
    #region Constructors
    internal Distance(IDistance distance) : base(distance)
    {
    }

    internal Distance(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Distance(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        decimal quantity = DefaultQuantity * ConvertMeasureRatio;

        return (IExtent)GetMeasure(quantity, default(ExtentUnit));

    }

    public override IDistance GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IDistance GetMeasure(double quantity, DistanceUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IDistance GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IDistance GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IDistance GetMeasure(IDistance? other = null)
    {
        return GetMeasure(this, other as Distance);
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }
    #endregion
}
