using CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;

internal sealed class Valuability : Proportion<IValuability, Currency, WeightUnit>, IValuability
{
    #region Constructors
    public Valuability(IValuability other) : base(other)
    {
    }

    public Valuability(IValuabilityFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public Valuability(IValuabilityFactory factory, decimal defaultQuantity) : base(factory, MeasureUnitTypeCode.Currency, defaultQuantity, MeasureUnitTypeCode.WeightUnit)
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

    public IValuability GetProportion(ICash numerator, IWeight denominator)
    {
        throw new NotImplementedException();
    }

    public IValuability GetProportion(ICash numerator, IMeasurement denominatorMeasurement)
    {
        throw new NotImplementedException();
    }

    public IValuability GetProportion(ICash numerator, IDenominator denominator)
    {
        throw new NotImplementedException();
    }

    public override IValuability GetProportion(IRateComponent numerator, WeightUnit denominatorMeasureUnit)
    {
        throw new NotImplementedException();
    }
    #endregion
}