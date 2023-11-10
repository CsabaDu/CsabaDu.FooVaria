using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Frequency : Proportion<IFrequency, Pieces, TimePeriodUnit>, IFrequency
{
    #region Constructors
    public Frequency(IProportionFactory<IFrequency, Pieces, TimePeriodUnit> factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Frequency(IProportionFactory<IFrequency, Pieces, TimePeriodUnit> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    {
    }

    public IFrequency GetProportion(IPieceCount numerator, ITimePeriod denominator)
    {
        throw new NotImplementedException();
    }
    #endregion
}