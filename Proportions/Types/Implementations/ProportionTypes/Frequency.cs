﻿namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Frequency : Proportion<IFrequency, Pieces, TimePeriodUnit>, IFrequency
{
    #region Constructors
    public Frequency(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Frequency(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    {
    }
    #endregion
}