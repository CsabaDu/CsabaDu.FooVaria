namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;

internal sealed class BaseMeasureFactoryObject : IBaseMeasureFactory
{
    #region Test helpers
    private readonly DataFields Fields = new();
    internal static readonly BaseMeasurementFactoryObject BaseMeasurementFactory = new();
    #endregion

    public RateComponentCode RateComponentCode => Fields.RandomParams.GetRandomRateComponentCode();

    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        return new BaseMeasureChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = baseMeasurement,
                GetBaseQuantity = quantity,
            }
        };
    }

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnitCode);
        TypeCode? quantityTypeCode = measureUnitCode.GetQuantityTypeCode();
        ValueType quantity = (ValueType)defaultQuantity.ToQuantity(quantityTypeCode.Value);

        return CreateBaseMeasure(baseMeasurement, quantity);
    }
}
