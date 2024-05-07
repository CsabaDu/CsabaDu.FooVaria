namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasures;

internal sealed class BaseMeasureFactoryObject(RateComponentCode rateComponentCode) : IBaseMeasureFactory
{
    #region Test helpers
    public static readonly BaseMeasurements.BaseMeasurementFactoryObject BaseMeasurementFactory = new();

    public static BaseMeasureFactoryObject GetBaseMeasureFactoryObject()
    {
        DataFields fields = DataFields.Fields;

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
        DataFields fields = DataFields.Fields;

        return new BaseMeasureChild(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurementValue = baseMeasurement,
                GetBaseQuantityValue = quantity,
                GetFactoryValue = this,
            }
        };
    }

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        DataFields fields = DataFields.Fields;

        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnitCode);
        fields.typeCode = measureUnitCode.GetQuantityTypeCode();
        fields.quantity = (ValueType)defaultQuantity.ToQuantity(fields.typeCode);

        return CreateBaseMeasure(baseMeasurement, fields.quantity);
    }
}
