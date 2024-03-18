namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal sealed class QuantifiableFactoryObject : IQuantifiableFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity) => new QuantifiableChild(new RootObject(), null)
    {
        Return = new()
        {
            GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
            GetDefaultQuantity = defaultQuantity,
        }
    };
}
