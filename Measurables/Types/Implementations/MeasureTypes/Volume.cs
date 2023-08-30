﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    internal Volume(IVolume volume) : base(volume)
    {
    }

    internal Volume(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Volume(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }

    public IVolume ConvertFrom(IWeight weight)
    {
        return NullChecked(weight, nameof(weight)).ConvertMeasure();
    }

    public IWeight ConvertMeasure()
    {
        decimal quantity = DefaultQuantity / CrossMeasureRatio;

        return (IWeight)GetMeasure(quantity, default(WeightUnit));
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

    public IVolume GetVolume(IBaseMeasure baseMeasure)
    {
        return (IVolume)GetMeasure(baseMeasure);
    }

    //public IWeight ToVolumetricWeight(decimal ratio, WeightUnit weightUnit = WeightUnit.g)
    //{
    //    throw new NotImplementedException();
    //}

    //public IVolume GetVolume(double quantity, VolumeUnit volumeUnit)
    //{
    //    throw new NotImplementedException();
    //}

    //public IVolume GetVolume(ValueType quantity, string name)
    //{
    //    throw new NotImplementedException();
    //}

    //public IVolume GetVolume(ValueType quantity, IMeasurement? measurement = null)
    //{
    //    throw new NotImplementedException();
    //}

    //public IVolume GetVolume(IVolume? other = null)
    //{
    //    throw new NotImplementedException();
    //}
}
