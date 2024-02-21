namespace CsabaDu.FooVaria.ShapeLimits.Factories.Implementations;

public sealed class ShapeLimitFactory(IBulkSpreadFactory bulkSpreadFactory, ITangentShapeFactory tangentShapeFactory) : SimpleShapeFactory(bulkSpreadFactory, tangentShapeFactory), IShapeLimitFactory
{
    public IShapeLimit Create(ISimpleShape simpleShape, LimitMode limitMode)
    {
        return new ShapeLimit(this, simpleShape, limitMode);
    }

    public IShapeLimit CreateNew(IShapeLimit other)
    {
        return new ShapeLimit(other);
    }

    public override IShape? CreateShape(params IShapeComponent[] shapeComponents)
    {
        int count = NullChecked(shapeComponents, nameof(shapeComponents)).Length;

        if (count == 1 && shapeComponents[0] is ISimpleShape simpleShape) return Create(simpleShape, default);

        return null;
    }
}
