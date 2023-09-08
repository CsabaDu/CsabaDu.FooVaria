namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    #region Constructors
    internal Weight(IWeight weight) : base(weight)
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
        return ConvertMeasure<IVolume, VolumeUnit>(this, ConvertMode.Multiply);
    }

    public override IWeight GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IWeight GetMeasure(double quantity, WeightUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IWeight GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IWeight GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IWeight GetMeasure(IWeight? other = null)
    {
        return GetMeasure(this, other as Weight);
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }
    #endregion
}
