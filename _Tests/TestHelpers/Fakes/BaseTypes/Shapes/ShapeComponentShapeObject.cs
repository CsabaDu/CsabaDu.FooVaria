
namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Shapes;

public sealed class ShapeComponentShapeObject(IRootObject rootObject, string paramName) : ShapeChild(rootObject, paramName), IShapeComponent
{
    public static ShapeComponentShapeObject GetShapeComponentShapeObject(ISpreadMeasure spreadMeasure, IShapeFactory factory = null)
    {
        ShapeComponentSpreadMeasureObject shapeComponent = GetShapeComponentSpreadMeasureObject(spreadMeasure);

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetFactory = factory,
                GetShapeComponents = [shapeComponent],
            },
            SpreadMeasure = spreadMeasure.GetSpreadMeasure(),
        };
    }

    public bool Equals(IShapeComponent x, IShapeComponent y)
    {
        return x is null
            && y is null
            || y is IShape xShape
            && y is IShape yShape
            && xShape.Equals(xShape, yShape);
   }

    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        return GetBaseShape().GetShapeComponents();
    }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        if (shapeComponent is IShape shape) return shape.GetHashCode(shape);

        if (shapeComponent is BaseQuantifiable baseQuantifiable) return baseQuantifiable.GetHashCode();

        return GetMeasureUnitCode().GetHashCode();
    }
}
