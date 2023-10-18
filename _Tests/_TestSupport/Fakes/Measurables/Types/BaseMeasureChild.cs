namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Types;

internal sealed class BaseMeasureChild : BaseMeasure
{
    public BaseMeasureChild(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }

    public BaseMeasureChild(IBaseMeasureFactory factory, ValueType quantity, Enum measureUnit) : base(factory, quantity, measureUnit)
    {
    }

    public BaseMeasureChild(IBaseMeasureFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
    {
    }

    public override IBaseMeasure ExchangeTo(Enum measureUnit)
    {
        throw new NotImplementedException();
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
    {
        throw new NotImplementedException();
    }

    public override IMeasurable GetDefault()
    {
        throw new NotImplementedException();
    }

    public override LimitMode? GetLimitMode()
    {
        throw new NotImplementedException();
    }

    public override IMeasurable GetMeasurable(IMeasurable other)
    {
        throw new NotImplementedException();
    }
}

