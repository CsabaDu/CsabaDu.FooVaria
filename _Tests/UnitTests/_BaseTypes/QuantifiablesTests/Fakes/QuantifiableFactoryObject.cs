namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal sealed class QuantifiableFactoryObject : IQuantifiableFactory
{
    DataFields Fields = new();

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity) => new QuantifiableChild(Fields.RootObject, Fields.paramName)
    {
        Return = new()
        {
            GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
            GetDefaultQuantity = defaultQuantity,
        }
    };
}
