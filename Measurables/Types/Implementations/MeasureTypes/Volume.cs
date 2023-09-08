namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    #region Constructors
    internal Volume(IVolume volume) : base(volume)
    {
    }

    //internal Volume(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    //{
    //}

    internal Volume(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }

    //public Volume(IMeasureFactory measureFactory, IBaseMeasure baseMeasure) : base(measureFactory, baseMeasure)
    //{
    //}
    #endregion

    #region Public methods
    public IVolume ConvertFrom(IWeight weight)
    {
        return NullChecked(weight, nameof(weight)).ConvertMeasure();
    }

    public IWeight ConvertMeasure()
    {
        decimal quantity = DefaultQuantity / ConvertMeasureRatio;

        return (IWeight)GetMeasure(quantity, default(WeightUnit));
    }

    public override IVolume GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IVolume GetMeasure(double quantity, VolumeUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IVolume GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IVolume GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IVolume GetMeasure(IVolume? other = null)
    {
        return GetMeasure(this, other as Volume);
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
