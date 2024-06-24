namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.BaseMeasures;

public sealed class SpreadMeasureBaseMeasureObject(IRootObject rootObject, string paramName) : BaseMeasureChild(rootObject, paramName), ISpreadMeasure
{
    public static SpreadMeasureBaseMeasureObject GetSpreadMeasureBaseMeasureObject(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null)
    {
        if (measureUnit is null || !GetMeasureUnitCode(measureUnit).IsSpreadMeasureUnitCode()) return null;

        

        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasurementReturnValue = BaseMeasurementFactory.CreateBaseMeasurement(measureUnit),
                GetBaseQuantityReturnValue = (double)quantity.ToQuantity(TypeCode.Double),
                GetFactoryReturnValue = GetBaseMeasureFactoryObject(rateComponentCode ?? RateComponentCode.Numerator),
            }
        };
    }

    public static SpreadMeasureBaseMeasureObject GetSpreadMeasureBaseMeasureObject(DataFields fields, RateComponentCode? rateComponentCode = null)
    {
        return GetSpreadMeasureBaseMeasureObject(fields.measureUnit, fields.quantity, rateComponentCode);
    }

    public double GetQuantity() => (double)ReturnValues.GetBaseQuantityReturnValue.ToQuantity(TypeCode.Double);

    public ISpreadMeasure GetSpreadMeasure() => this;

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        if (spreadMeasure == null) throw new ArgumentNullException(paramName);

        if (spreadMeasure is not IBaseMeasure) throw new ArgumentOutOfRangeException(paramName);
    }
}
