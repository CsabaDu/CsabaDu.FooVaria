namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

internal sealed class BaseMeasurableChild : BaseMeasurable
{
    public BaseMeasurableChild(IFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }

    public BaseMeasurableChild(IFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    public BaseMeasurableChild(IFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
    {
    }

    public BaseMeasurableChild(IBaseMeasurable other) : base(other)
    {
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }

    public override IFactory GetFactory()
    {
        throw new NotImplementedException();
    }
}
