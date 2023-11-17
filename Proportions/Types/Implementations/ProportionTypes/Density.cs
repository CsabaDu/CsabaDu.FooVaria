using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;
using CsabaDu.FooVaria.Measurements.Types;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Density : Proportion<IDensity, WeightUnit, VolumeUnit>, IDensity
{
    #region Constructors
    public Density(IDensity other) : base(other)
    {
    }

    public Density(IDensityFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Density(IDensityFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    {
    }

    public override IBaseRate GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator)
    {
        throw new NotImplementedException();
    }

    public IDensity GetProportion(IWeight numerator, IVolume denominator)
    {
        throw new NotImplementedException();
    }

    public IDensity GetProportion(IWeight numerator, IMeasurement denominatorMeasurement)
    {
        throw new NotImplementedException();
    }

    public IDensity GetProportion(IWeight numerator, IDenominator denominator)
    {
        throw new NotImplementedException();
    }
    #endregion
}