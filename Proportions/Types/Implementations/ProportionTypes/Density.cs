using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Density : Proportion<IDensity, WeightUnit, VolumeUnit>, IDensity
{
    #region Constructors
    public Density(IProportionFactory<IDensity, WeightUnit, VolumeUnit> factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Density(IProportionFactory<IDensity, WeightUnit, VolumeUnit> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    {
    }

    public IDensity GetProportion(IWeight numerator, IVolume denominator)
    {
        throw new NotImplementedException();
    }
    #endregion
}