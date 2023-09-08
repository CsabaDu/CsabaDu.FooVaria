namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Extent : Measure, IExtent
{
    #region Constructors
    internal Extent(IExtent extent) : base(extent)
    {
    }

    internal Extent(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
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
        decimal quantity = DefaultQuantity / ConvertMeasureRatio;

        return (IDistance)GetMeasure(quantity, default(DistanceUnit));
    }

    public override IExtent GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IExtent GetMeasure(double quantity, ExtentUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IExtent GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IExtent GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IExtent GetMeasure(IExtent? other = null)
    {
        return GetMeasure(this, other as Extent);
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }
    #endregion
}
