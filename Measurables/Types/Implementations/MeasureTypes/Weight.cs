using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    internal Weight(IWeight weight) : base(weight)
    {
    }

    internal Weight(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Weight(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    public IWeight ConvertFrom(IVolume volume)
    {
        return NullChecked(volume, nameof(volume)).ConvertMeasure();
    }

    public IVolume ConvertMeasure()
    {
        decimal quantity = DefaultQuantity * CrossMeasureRatio;

        return (IVolume)GetMeasure(quantity, default(VolumeUnit));
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

    public override Enum GetMeasureUnit()
    {
        return (WeightUnit)Measurement.MeasureUnit;
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }

    public IWeight GetWeight(IBaseMeasure baseMeasure)
    {
        return (IWeight)GetMeasure(baseMeasure);
    }

    //public IWeight GetVolumetricWeight(IVolume volume, decimal ratio, bool isGreater = true)
    //{
    //    throw new NotImplementedException();
    //}

    //public IWeight GetWeight(double quantity, WeightUnit weightUnit)
    //{
    //    throw new NotImplementedException();
    //}

    //public IWeight GetWeight(ValueType quantity, string name)
    //{
    //    throw new NotImplementedException();
    //}

    //public IWeight GetWeight(ValueType quantity, IMeasurement? measurement = null)
    //{
    //    throw new NotImplementedException();
    //}

    //public IWeight GetWeight(IWeight? other = null)
    //{
    //    throw new NotImplementedException();
    //}
}
