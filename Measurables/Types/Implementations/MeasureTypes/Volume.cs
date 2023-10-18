namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    #region Constructors
    internal Volume(IMeasureFactory factory, ValueType quantity, VolumeUnit volumeUnit) : base(factory, quantity, volumeUnit)
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
        return ConvertMeasure<IWeight, WeightUnit>(this, ConvertMode.Divide);
    }

    public IVolume GetDefaultRateComponent()
    {
        return GetDefault(this);
    }

    public double GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<double>();
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

    public IVolume GetMeasure(double quantity, IMeasurement measurement)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IVolume GetMeasure(IVolume other)
    {
        return GetMeasure(this as IVolume, other);
    }

    public IVolume GetMeasure(double quantity)
    {
        return GetMeasure(this, quantity);
    }

    public VolumeUnit GetMeasureUnit()
    {
        return GetMeasureUnit<VolumeUnit>(this);
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }
    #endregion
}
