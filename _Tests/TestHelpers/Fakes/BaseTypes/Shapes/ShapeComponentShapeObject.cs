namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Shapes;

public sealed class ShapeComponentShapeObject(IRootObject rootObject, string paramName) : ShapeChild(rootObject, paramName), IShapeComponent
{
    public static ShapeComponentShapeObject GetShapeComponentShapeObject(ISpreadMeasure spreadMeasure, IShapeFactory factory = null)
    {
        ShapeComponentSpreadMeasureObject shapeComponentSpreadMeasure = GetShapeComponentSpreadMeasureObject(spreadMeasure);

        return GetShapeComponentShapeObject(shapeComponentSpreadMeasure);
    }

    private static ShapeComponentShapeObject GetShapeComponentShapeObject(ShapeComponentSpreadMeasureObject shapeComponentSpreadMeasure, IShapeFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetFactoryValue = factory,
                GetShapeComponentsValue = [shapeComponentSpreadMeasure],
            },
            SpreadMeasure = shapeComponentSpreadMeasure.GetSpreadMeasure(),
        };
    }

    public static ShapeComponentShapeObject GetShapeComponentShapeObject(Enum measureUnit, decimal defaultQuantity, IShapeFactory factory = null)
    {
        ShapeComponentSpreadMeasureObject shapeComponentSpreadMeasure = GetShapeComponentSpreadMeasureObject(measureUnit, defaultQuantity);

        return GetShapeComponentShapeObject(shapeComponentSpreadMeasure, factory);
    }

    public static ShapeComponentShapeObject GetShapeComponentShapeObject(DataFields fields, IShapeFactory factory = null)
    {
        return GetShapeComponentShapeObject(fields.measureUnit, fields.defaultQuantity, factory);
    }

    public bool Equals(IShapeComponent x, IShapeComponent y)
    {
        return FakeMethods.Equals(x, y);
    }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return FakeMethods.GetHashCode(shapeComponent);
    }
}
