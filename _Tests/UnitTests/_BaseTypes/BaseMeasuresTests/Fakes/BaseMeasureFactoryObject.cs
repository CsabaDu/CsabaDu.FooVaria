namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;

internal sealed class BaseMeasureFactoryObject(RateComponentCode rateComponentCode) : IBaseMeasureFactory
{
    #region Test helpers
    private readonly DataFields Fields = new();
    internal static readonly BaseMeasurementFactoryObject BaseMeasurementFactory = new();

    public static BaseMeasureFactoryObject GetBaseMeasureFactoryObject()
    {
        DataFields fields = new();
        RateComponentCode rateComponentCode = fields.RandomParams.GetRandomRateComponentCode();

        return GetBaseMeasureFactoryObject(rateComponentCode);
    }

    public static BaseMeasureFactoryObject GetBaseMeasureFactoryObject(RateComponentCode rateComponentCode)
    {
        return new(rateComponentCode);
    }
    #endregion

    public RateComponentCode RateComponentCode => rateComponentCode;

    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        return new BaseMeasureChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = baseMeasurement,
                GetBaseQuantity = quantity,
                GetFactory = this,
            }
        };
    }

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnitCode);
        Fields.typeCode = measureUnitCode.GetQuantityTypeCode();
        Fields.quantity = (ValueType)defaultQuantity.ToQuantity(Fields.typeCode.Value);

        return CreateBaseMeasure(baseMeasurement, Fields.quantity);
    }
}
