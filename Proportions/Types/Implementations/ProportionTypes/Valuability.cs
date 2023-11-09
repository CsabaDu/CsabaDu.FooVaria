namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Valuability : Proportion<IValuability, Currency, WeightUnit>, IValuability
{
    #region Constructors
    public Valuability(IProportionFactory<IValuability, Currency, WeightUnit> factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Valuability(IProportionFactory<IValuability, Currency, WeightUnit> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    {
    }
    #endregion
}