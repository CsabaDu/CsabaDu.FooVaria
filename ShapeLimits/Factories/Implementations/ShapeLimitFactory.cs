namespace CsabaDu.FooVaria.ShapeLimits.Factories.Implementations;

public sealed class ShapeLimitFactory(ISimpleShapeFactory simpleShapeFactory) : IShapeLimitFactory
{
    public ISimpleShapeFactory SimpleShapeFactory { get; init; } = NullChecked(simpleShapeFactory, nameof(simpleShapeFactory));

    public IShapeLimit Create(ISimpleShape simpleShape, LimitMode limitMode)
    {
        return new ShapeLimit(this, simpleShape, limitMode);
    }

    public IShapeLimit? Create(LimitMode limitMode, params IShapeComponent[] shapeComponents)
    {
        if (!Enum.IsDefined(limitMode)) return null;

        IShape? shape = CreateShape(shapeComponents);

        if (shape?.GetBaseShape() is not ISimpleShape simpleShape) return null;

        return Create(simpleShape, limitMode);
    }

    public IShapeLimit CreateNew(IShapeLimit other)
    {
        return new ShapeLimit(other);
    }

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        return SimpleShapeFactory.CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    public IShape? CreateShape(params IShapeComponent[] shapeComponents)
    {
        return SimpleShapeFactory.CreateShape(shapeComponents);
    }

    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        return SimpleShapeFactory.CreateSpread(spreadMeasure);
    }

    public ISpreadMeasure? CreateSpreadMeasure(Enum measureUnit, double quantity)
    {
        return SimpleShapeFactory.CreateSpreadMeasure(measureUnit, quantity);
    }
}
