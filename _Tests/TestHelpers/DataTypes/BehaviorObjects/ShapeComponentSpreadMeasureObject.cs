namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;

public sealed class ShapeComponentSpreadMeasureObject : SpreadMeasureObject, IShapeComponent
{
    public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(Enum measureUnit, ValueType quantity)
    {
        return new()
        {
            MeasureUnit = measureUnit,
            Quantity = (double)quantity.ToQuantity(TypeCode.Double),
        };
    }

    public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(ISpreadMeasure spreadMeasure)
    {
        Enum measureUnit = spreadMeasure.GetBaseMeasureUnit();
        double quantity = spreadMeasure.GetQuantity();

        return GetShapeComponentSpreadMeasureObject(measureUnit, quantity);
    }

    //public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    //{
    //    Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    double quantity = (double)defaultQuantity.ToQuantity(TypeCode.Double);

    //    return GetShapeComponentSpreadMeasureObject(measureUnit, quantity);
    //}
}
