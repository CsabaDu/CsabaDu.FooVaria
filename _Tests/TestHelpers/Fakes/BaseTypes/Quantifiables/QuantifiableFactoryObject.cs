namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Quantifiables;

public sealed class QuantifiableFactoryObject : IQuantifiableFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();

        return GetQuantifiableChild(defaultQuantity, measureUnit, this);
    }
}
