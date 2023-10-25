using CsabaDu.FooVaria.Proportions.Types;
using CsabaDu.FooVaria.Proportions.Types.Implementations;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class DensityFactory : ProportionFactory<IDensity, WeightUnit, VolumeUnit>, IDensityFactory
{
    #region Public methods
    #region Override methods
    public override IDensity Create(IBaseRate baseRate)
    {
        if (baseRate is IDensity other) return Create(other);

        return new Density(this, baseRate);
    }

    public override IDensity Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        return new Density(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
    }

    public override IDensity Create(IDensity other)
    {
        return new Density(other);
    }
    #endregion
    #endregion
}
