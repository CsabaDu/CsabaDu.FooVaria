using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal sealed class QuantifiableFactoryObject : IQuantifiableFactory
{
    private readonly DataFields Fields = new();

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        return new QuantifiableChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
                GetDefaultQuantity = defaultQuantity,
            }
        };
    }
}
