﻿//using CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;
//using CsabaDu.FooVaria.Measurements.Types;

//namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

//internal sealed class Density : Proportion<IDensity, WeightUnit, VolumeUnit>, IDensity
//{
//    #region Constructors
//    public Density(IDensity other) : base(other)
//    {
//    }

//    public Density(IDensityFactory factory, IBaseRate baseRate) : base(factory, baseRate)
//    {
//    }

//    public Density(IDensityFactory factory, decimal defaultQuantity) : base(factory, MeasureUnitTypeCode.WeightUnit, defaultQuantity, MeasureUnitTypeCode.VolumeUnit)
//    {
//    }

//    public override IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
//    {
//        throw new NotImplementedException();
//    }

//    public override IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominatorMeasureUnit)
//    {
//        throw new NotImplementedException();
//    }

//    public IDensity GetProportion(IWeight numerator, IVolume denominator)
//    {
//        throw new NotImplementedException();
//    }

//    public IDensity GetProportion(IWeight numerator, IMeasurement denominatorMeasurement)
//    {
//        throw new NotImplementedException();
//    }

//    public IDensity GetProportion(IWeight numerator, IDenominator denominator)
//    {
//        throw new NotImplementedException();
//    }

//    public override IDensity GetProportion(IRateComponent numerator, VolumeUnit denominatorMeasureUnit)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//}