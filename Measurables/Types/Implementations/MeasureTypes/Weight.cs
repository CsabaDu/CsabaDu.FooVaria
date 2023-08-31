namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    #region Constructors
    internal Weight(IWeight weight) : base(weight)
    {
    }

    internal Weight(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Weight(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public IWeight ConvertFrom(IVolume volume)
    {
        return NullChecked(volume, nameof(volume)).ConvertMeasure();
    }

    public IVolume ConvertMeasure()
    {
        decimal quantity = DefaultQuantity * CrossMeasureRatio;

        return (IVolume)GetMeasure(quantity, default(VolumeUnit));
    }

    public override IWeight GetMeasure(IBaseMeasure baseMeasure)
    {
        ValidateBaseMeasure(baseMeasure);

        return (IWeight)base.GetMeasure(baseMeasure);
    }

    public IWeight GetMeasure(double quantity, WeightUnit measureUnit)
    {
        return (IWeight)base.GetMeasure(quantity, measureUnit);
    }

    public IWeight GetMeasure(double quantity, string name)
    {
        return (IWeight)base.GetMeasure(quantity, name);
    }

    public IWeight GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (IWeight)base.GetMeasure(quantity, measurement);
    }

    public IWeight GetMeasure(IWeight? other = null)
    {
        return (IWeight)base.GetMeasure(other);
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }
    #endregion
}
