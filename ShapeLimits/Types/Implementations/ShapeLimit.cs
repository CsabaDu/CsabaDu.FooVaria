namespace CsabaDu.FooVaria.ShapeLimits.Types.Implementations;

public sealed class ShapeLimit : SimpleShape, IShapeLimit
{
    internal ShapeLimit(IShapeLimit other) : base(other)
    {
        SimpleShape = other.SimpleShape;
        LimitMode = other.LimitMode;
        Factory = other.Factory;
    }

    internal ShapeLimit(IShapeLimitFactory factory, ISimpleShape simpleShape, LimitMode limitMode) : base(factory)
    {
        ValidateSimpleShape(simpleShape, nameof(simpleShape));

        SimpleShape = simpleShape;
        LimitMode = Defined(limitMode, nameof(limitMode));
        Factory = factory;
    }

    public LimitMode LimitMode { get; init; }
    public ISimpleShape SimpleShape { get; init; }
    public IShapeLimitFactory Factory { get; init; }

    public override IExtent? this[ShapeExtentCode shapeExtentCode] => SimpleShape[shapeExtentCode];

    public bool Equals(IShapeLimit? x, IShapeLimit? y)
    {
        if (x is null && y is null) return true;

        return x is not null
            && y is not null
            && x.LimitMode == y.LimitMode
            && x.Equals(y);
    }

    public int GetHashCode([DisallowNull] IShapeLimit shapeLimit)
    {
        return HashCode.Combine(shapeLimit.LimitMode, shapeLimit.GetHashCode());
    }

    public LimitMode? GetLimitMode()
    {
        return LimitMode;
    }

    public IShapeLimit GetNew(IShapeLimit other)
    {
        return Factory.CreateNew(other);
    }

    public IShapeLimit GetShapeLimit(ISimpleShape simpleShape, LimitMode limitMode)
    {
        return Factory.Create(simpleShape, limitMode);
    }

    public IShapeLimit? GetShapeLimit(LimitMode limitMode, params IShapeComponent[] shapeComponents)
    {
        return Factory.Create(limitMode, shapeComponents);
    }
    public bool? Includes(IShape? limitable)
    {
        return limitable is null ?
            false
            : limitable?.FitsIn(this);
    }

    public ISimpleShapeFactory GetSimpleShapeFactory()
    {
        return (ISimpleShapeFactory)SimpleShape.GetFactory();
    }

    public void ValidateSimpleShape(ISimpleShape? simpleShape, string paramName)
    {
        IFactory factory = NullChecked(simpleShape, nameof(simpleShape)).GetFactory();

        if (GetSimpleShapeFactory().Equals(factory)) return;

        throw ArgumentTypeOutOfRangeException(paramName, simpleShape!);
    }

    public override IShapeLimitFactory GetFactory()
    {
        return Factory;
    }

    public override Enum GetBaseMeasureUnit()
    {
        return SimpleShape.GetBaseMeasureUnit();
    }

    public override IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable)
    {
        return SimpleShape.GetValidShapeComponent(quantifiable);
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

    public override ISimpleShape GetBaseShape()
    {
        return SimpleShape;
    }
    public override ITangentShapeFactory GetTangentShapeFactory()
    {
        return SimpleShape.GetTangentShapeFactory();
    }

    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
        exchanged = measureUnitElements.MeasureUnit switch
        {
            AreaUnit areaUnit => exchangeTo<IArea, AreaUnit>(areaUnit),
            ExtentUnit extentUnit => ExchangeTo(extentUnit),
            VolumeUnit volumeUnit => exchangeTo<IVolume, VolumeUnit>(volumeUnit),

            _ => null,
        };

        return exchanged is not null;

        #region Local methods
        IQuantifiable? exchangeTo<TSMeasure, TEnum>(TEnum measureUnit)
            where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
            where TEnum : struct, Enum
        {
            return GetSpreadMeasure() is TSMeasure spreadMeasure ? GetSpread(spreadMeasure.ExchangeTo(measureUnit)!) : null;
        }
        #endregion
    }
}
