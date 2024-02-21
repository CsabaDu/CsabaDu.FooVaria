namespace CsabaDu.FooVaria.ShapeLimits.Types.Implementations;

public sealed class ShapeLimit : SimpleShape, IShapeLimit
{
    internal ShapeLimit(IShapeLimit other) : base(other)
    {
        SimpleShape = other.SimpleShape;
        LimitMode = other.LimitMode;
    }

    internal ShapeLimit(ISimpleShapeFactory factory, ISimpleShape simpleShape, LimitMode limitMode) : base(factory)
    {
        SimpleShape = NullChecked(simpleShape, nameof(simpleShape));
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    public override IExtent? this[ShapeExtentCode shapeExtentCode] => SimpleShape[shapeExtentCode];

    public LimitMode LimitMode { get; init; }
    public ISimpleShape SimpleShape { get; init; }

    public bool Equals(IShapeLimit? x, IShapeLimit? y)
    {
        if (x == null && y == null) return true;

        return x != null
            && y != null
            && x.LimitMode == y.LimitMode
            && x.Equals(y);
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return SimpleShape.GetDiagonal();
    }

    public int GetHashCode([DisallowNull] IShapeLimit shapeLimit)
    {
        return HashCode.Combine(shapeLimit.LimitMode, shapeLimit.GetHashCode());
    }

    public decimal GetLimiterDefaultQuantity()
    {
        return SimpleShape.GetDefaultQuantity();
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return SimpleShape.GetMeasureUnitCode();
    }

    public LimitMode? GetLimitMode()
    {
        return LimitMode;
    }

    public IShapeLimit GetNew(IShapeLimit other)
    {
        return GetFactory().CreateNew(other);
    }

    public IShapeLimit GetShapeLimit(ISimpleShape simpleShape, LimitMode limitMode)
    {
        return GetFactory().Create(simpleShape, limitMode);
    }

    public override ISpreadMeasure GetSpreadMeasure()
    {
        return SimpleShape.GetSpreadMeasure();
    }

    public override IShapeComponent? GetValidShapeComponent(IBaseQuantifiable? shapeComponent)
    {
        return SimpleShape.GetValidShapeComponent(shapeComponent);
    }

    public bool? Includes(IShape? limitable)
    {
        return limitable?.FitsIn(this, LimitMode);
    }

    public override IShapeLimitFactory GetFactory()
    {
        return (IShapeLimitFactory)Factory;
    }
}
