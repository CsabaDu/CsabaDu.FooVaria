namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasures;

public sealed class SpreadMeasureBaseMeasureObject(IRootObject rootObject, string paramName) : BaseMeasureChild(rootObject, paramName), ISpreadMeasure
{
    public static SpreadMeasureBaseMeasureObject GetSpreadMeasureBaseMeasureObject(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null)
    {
        if (measureUnit is null || !GetMeasureUnitCode(measureUnit).IsSpreadMeasureUnitCode()) return null;

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = BaseMeasureFactoryObject.BaseMeasurementFactory.CreateBaseMeasurement(measureUnit),
                GetBaseQuantity = quantity is double ? quantity : (ValueType)quantity.ToQuantity(TypeCode.Double),
                GetFactory = BaseMeasureFactoryObject.GetBaseMeasureFactoryObject(rateComponentCode ?? RateComponentCode.Numerator),
            }
        };
    }

    public double GetQuantity() => (double)GetBaseQuantity();

    public ISpreadMeasure GetSpreadMeasure() => this;

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        if (spreadMeasure == null) throw new ArgumentNullException(paramName);

        if (spreadMeasure is not IBaseMeasure) throw new ArgumentOutOfRangeException(paramName);
    }
}
