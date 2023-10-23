﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Weight : Measure<IWeight, double, WeightUnit>, IWeight
{
    #region Constructors
    internal Weight(IMeasureFactory factory, ValueType quantity, WeightUnit weightUnit) : base(factory, quantity, weightUnit)
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
        return ConvertMeasure<IVolume, VolumeUnit>(ConvertMode.Multiply);
    }
    #endregion
}


    //public IWeight GetDefaultRateComponent()
    //{
    //    return GetDefault(this);
    //}

    //public double GetDefaultRateComponentQuantity()
    //{
    //    return GetDefaultRateComponentQuantity<double>();
    //}

    //public override IWeight GetMeasure(IBaseMeasure baseMeasure)
    //{
    //    return GetMeasure(this, baseMeasure);
    //}

    //public IWeight GetMeasure(double quantity, WeightUnit measureUnit)
    //{
    //    return GetMeasure(this, quantity, measureUnit);
    //}

    //public IWeight GetMeasure(double quantity, string name)
    //{
    //    return GetMeasure(this, quantity, name);
    //}

    //public IWeight GetMeasure(double quantity, IMeasurement measurement)
    //{
    //    return GetMeasure(this, quantity, measurement);
    //}

    //public IWeight GetMeasure(IWeight other)
    //{
    //    return GetMeasure(this as IWeight, other);
    //}

    //public IWeight GetMeasure(double quantity)
    //{
    //    return GetMeasure(this, quantity);
    //}

    //public WeightUnit GetMeasureUnit()
    //{
    //    return GetMeasureUnit<WeightUnit>(this);
    //}