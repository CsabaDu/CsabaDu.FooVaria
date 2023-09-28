namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    #region Constructors
    internal Weight(IWeight other) : base(other)
    {
    }

    internal Weight(IMeasureFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
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

    public IWeight GetMeasure(double quantity, IMeasurement measurement)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IWeight GetMeasure(IWeight other)
    {
        return GetMeasure(this as IWeight, other);
    }

    public IWeight GetMeasure(double quantity)
    {
        return GetMeasure(this, quantity);
    }
    #endregion
}
