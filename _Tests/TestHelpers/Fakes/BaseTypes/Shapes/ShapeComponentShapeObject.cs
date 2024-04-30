
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
                GetFactory = factory,
                GetShapeComponents = [shapeComponentSpreadMeasure],
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
        return x is null
            && y is null
            || y is IShape xShape
            && y is IShape yShape
            && xShape.Equals(xShape, yShape);
   }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        if (shapeComponent is IShape shape) return shape.GetHashCode(shape);

        if (shapeComponent is BaseQuantifiable baseQuantifiable) return baseQuantifiable.GetHashCode();

        return GetMeasureUnitCode().GetHashCode();
    }
}
