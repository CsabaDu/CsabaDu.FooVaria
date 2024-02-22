namespace CsabaDu.FooVaria.ShapeLimits.Factories.Implementations;

public sealed class ShapeLimitFactory(ISimpleShapeFactory simpleShapeFactory) : SimpleShapeFactory, IShapeLimitFactory
{
    public ISimpleShapeFactory SimpleShapeFactory { get; init; } = NullChecked(simpleShapeFactory, nameof(simpleShapeFactory));

    public IShapeLimit Create(ISimpleShape simpleShape, LimitMode limitMode)
    {
        return new ShapeLimit(this, simpleShape, limitMode);
    }

    public IShapeLimit? Create(LimitMode limitMode, params IShapeComponent[] shapeComponents)
    {
        if (!Enum.IsDefined(limitMode)) return null;

        ISimpleShape? simpleShape = CreateShape(shapeComponents);

        if (simpleShape == null) return null;

        return Create(simpleShape, limitMode);
    }

    public IShapeLimit CreateNew(IShapeLimit other)
    {
        return new ShapeLimit(other);
    }

    public override ISimpleShape? CreateShape(params IShapeComponent[] shapeComponents)
    {
        return (ISimpleShape?)SimpleShapeFactory.CreateShape(shapeComponents);
    }

    public override IBulkSpreadFactory GetBulkSpreadFactory()
    {
        return SimpleShapeFactory.GetBulkSpreadFactory();
    }

    public override ITangentShapeFactory GetTangentShapeFactory()
    {
        return SimpleShapeFactory.GetTangentShapeFactory();
    }
}
