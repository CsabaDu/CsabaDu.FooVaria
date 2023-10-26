using CsabaDu.FooVaria.Measurables.Statics;
using CsabaDu.FooVaria.Proportions.Factories;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations;

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
    #endregion

    #region Public methods
    #region Override methods
    public override IDensityFactory GetFactory()
    {
        return (IDensityFactory)Factory;
    }

    public override void ValidateQuantity(ValueType? quantity)
    {
        base.ValidateQuantity(quantity);

        if ((decimal?)quantity!.ToQuantity(TypeCode.Decimal) > 0) return;

        throw QuantityArgumentOutOfRangeException(quantity);
    }
    #endregion
    #endregion
}
