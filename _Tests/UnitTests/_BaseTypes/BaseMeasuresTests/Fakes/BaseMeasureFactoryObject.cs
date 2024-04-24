//namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;

//internal sealed class BaseMeasureFactoryObject(RateComponentCode rateComponentCode) : IBaseMeasureFactory
//{
//    #region Test helpers
//    internal static readonly TestHelpers.Fakes.BaseTypes.BaseMeasurements.BaseMeasurementFactoryObject BaseMeasurementFactory = new();

//    public static BaseMeasureFactoryObject GetBaseMeasureFactoryObject()
//    {
//        RateComponentCode rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();

//        return GetBaseMeasureFactoryObject(rateComponentCode);
//    }

//    public static BaseMeasureFactoryObject GetBaseMeasureFactoryObject(RateComponentCode rateComponentCode)
//    {
//        return new(rateComponentCode);
//    }
//    #endregion

//    public RateComponentCode RateComponentCode => rateComponentCode;

//    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
//    {
//        return new BaseMeasureChild(Fields.RootObject, Fields.paramName)
//        {
//            Return = new()
//            {
//                GetBaseMeasurement = baseMeasurement,
//                GetBaseQuantity = quantity,
//                GetFactory = this,
//            }
//        };
//    }

//    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
//    {
//        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnitCode);
//        Fields.typeCode = measureUnitCode.GetQuantityTypeCode();
//        Fields.quantity = (ValueType)defaultQuantity.ToQuantity(Fields.typeCode);

//        return CreateBaseMeasure(baseMeasurement, Fields.quantity);
//    }
//}
