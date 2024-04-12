namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal sealed class QuantifiableFactoryObject : IQuantifiableFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();

        return GetQuantifiableChild(defaultQuantity, measureUnit, this);
    }
}
