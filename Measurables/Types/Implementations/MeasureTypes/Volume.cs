namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    #region Constructors
    internal Volume(IVolume volume) : base(volume)
    {
    }

    internal Volume(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Volume(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public IVolume ConvertFrom(IWeight weight)
    {
        return NullChecked(weight, nameof(weight)).ConvertMeasure();
    }

    public IWeight ConvertMeasure()
    {
        decimal quantity = DefaultQuantity / CrossMeasureRatio;

        return (IWeight)GetMeasure(quantity, default(WeightUnit));
    }

    public override IVolume GetMeasure(IBaseMeasure baseMeasure)
    {
        ValidateBaseMeasure(baseMeasure);

        return (IVolume)base.GetMeasure(baseMeasure);
    }

    public IVolume GetMeasure(double quantity, VolumeUnit measureUnit)
    {
        return (IVolume)base.GetMeasure(quantity, measureUnit);
    }

    public IVolume GetMeasure(double quantity, string name)
    {
        return (IVolume)base.GetMeasure(quantity, name);
    }

    public IVolume GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (IVolume)base.GetMeasure(quantity, measurement);
    }

    public IVolume GetMeasure(IVolume? other = null)
    {
        return (IVolume)base.GetMeasure(other);
    }

    public override Enum GetMeasureUnit()
    {
        return (VolumeUnit)Measurement.MeasureUnit;
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
