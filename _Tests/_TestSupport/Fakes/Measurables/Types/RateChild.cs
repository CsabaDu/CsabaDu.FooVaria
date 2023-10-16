namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Types;

internal sealed class RateChild : Rate
{
    public RateChild(IRate other) : base(other)
    {
    }

    public RateChild(IRateFactory factory, IMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, numerator, measureUnitTypeCode)
    {
    }

    public RateChild(IRateFactory factory, IMeasure numerator, IMeasurement measurement) : base(factory, numerator, measurement)
    {
    }

    public RateChild(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, numerator, denominator)
    {
    }

    public override IMeasurable GetMeasurable(IMeasurable other)
    {
        throw new NotImplementedException();
    }

    public override IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit limit)
    {
        throw new NotImplementedException();
    }
}
