namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

internal sealed class BaseRateChild : BaseRate
{
    public BaseRateChild(IBaseRate other) : base(other)
    {
    }

    public BaseRateChild(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    public BaseRateChild(IBaseRateFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, measureUnitTypeCode)
    {
    }

    public override IBaseRate ExchangeTo(IBaseMeasurable context)
    {
        throw new NotImplementedException();
    }

    public override IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode)
    {
        throw new NotImplementedException();
    }
}
