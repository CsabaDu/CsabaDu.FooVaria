using CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Frequency : Proportion<IFrequency, Pieces, TimePeriodUnit>, IFrequency
{
    #region Constructors
    public Frequency(IFrequency other) : base(other)
    {
    }

    public Frequency(IFrequencyFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Frequency(IFrequencyFactory factory, decimal defaultQuantity) : base(factory, MeasureUnitTypeCode.Pieces, defaultQuantity, MeasureUnitTypeCode.TimePeriodUnit)
    {
    }

    public override IBaseRate GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator)
    {
        throw new NotImplementedException();
    }

    public override IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominatorMeasureUnit)
    {
        throw new NotImplementedException();
    }

    public IFrequency GetProportion(IPieceCount numerator, ITimePeriod denominator)
    {
        throw new NotImplementedException();
    }

    public IFrequency GetProportion(IPieceCount numerator, IMeasurement denominatorMeasurement)
    {
        throw new NotImplementedException();
    }

    public IFrequency GetProportion(IPieceCount numerator, IDenominator denominator)
    {
        throw new NotImplementedException();
    }

    public override IFrequency GetProportion(IRateComponent numerator, TimePeriodUnit denominatorMeasureUnit)
    {
        throw new NotImplementedException();
    }
    #endregion
}