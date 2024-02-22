namespace CsabaDu.FooVaria.ShapeLimits.Types.Implementations;

public sealed class ShapeLimit : SimpleShape, IShapeLimit
{
    internal ShapeLimit(IShapeLimit other) : base(other)
    {
        LimitMode = other.LimitMode;
        SimpleShape = other.SimpleShape;
    }

    internal ShapeLimit(IShapeLimitFactory factory, ISimpleShape simpleShape, LimitMode limitMode) : base(factory)
    {
        ValidateSimpleShape(simpleShape, nameof(simpleShape));

        LimitMode = Defined(limitMode, nameof(limitMode));
        SimpleShape = simpleShape;
    }

    public LimitMode LimitMode { get; init; }
    public ISimpleShape SimpleShape { get; init; }

    public override IExtent? this[ShapeExtentCode shapeExtentCode] => SimpleShape[shapeExtentCode];

    public bool Equals(IShapeLimit? x, IShapeLimit? y)
    {
        if (x == null && y == null) return true;

        return x != null
            && y != null
            && x.LimitMode == y.LimitMode
            && x.Equals(y);
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

    public bool? Includes(IShape? limitable)
    {
        return limitable?.FitsIn(this, LimitMode);
    }

    public ISimpleShapeFactory GetSimpleShapeFactory()
    {
        return (ISimpleShapeFactory)SimpleShape.Factory;
    }

    public void ValidateSimpleShape(ISimpleShape? simpleShape, string paramName)
    {
        IFactory factory = NullChecked(simpleShape, nameof(simpleShape)).GetFactory();

        if (GetSimpleShapeFactory().Equals(factory)) return;

        throw ArgumentTypeOutOfRangeException(paramName, simpleShape!);
    }

    public override IShapeLimitFactory GetFactory()
    {
        return (IShapeLimitFactory)Factory;
    }

    public override Enum GetMeasureUnit()
    {
        return SimpleShape.GetMeasureUnit();
    }

    public override IShapeComponent? GetValidShapeComponent(IBaseQuantifiable? shapeComponent)
    {
        return SimpleShape.GetValidShapeComponent(shapeComponent);
    }

    public override ISpreadMeasure GetSpreadMeasure()
    {
        return SimpleShape.GetSpreadMeasure();
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return SimpleShape.GetDiagonal(extentUnit);
    }

    public override IBulkSpreadFactory GetBulkSpreadFactory()
    {
        return SimpleShape.GetBulkSpreadFactory();
    }

    public override ISimpleShape GetShape()
    {
        return SimpleShape;
    }
    public override ITangentShapeFactory GetTangentShapeFactory()
    {
        return SimpleShape.GetTangentShapeFactory();
    }
}
