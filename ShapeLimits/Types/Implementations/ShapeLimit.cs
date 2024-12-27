namespace CsabaDu.FooVaria.ShapeLimits.Types.Implementations;

/// <summary>
/// Represents a shape limit with specific constraints and behaviors.
/// </summary>
public sealed class ShapeLimit : Shape, IShapeLimit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeLimit"/> class by copying another shape limit.
    /// </summary>
    /// <param name="other">The other shape limit to copy.</param>
    internal ShapeLimit(IShapeLimit other) : base(other, nameof(other))
    {
        SimpleShape = other.SimpleShape;
        LimitMode = other.LimitMode;
        Factory = other.Factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeLimit"/> class with the specified factory, simple shape, and limit mode.
    /// </summary>
    /// <param name="factory">The factory to create shape limits.</param>
    /// <param name="simpleShape">The simple shape associated with the shape limit.</param>
    /// <param name="limitMode">The limit mode for the shape limit.</param>
    internal ShapeLimit(IShapeLimitFactory factory, ISimpleShape simpleShape, LimitMode limitMode) : base(factory, nameof(factory))
    {
        ValidateSimpleShape(simpleShape, nameof(simpleShape));

        SimpleShape = simpleShape;
        LimitMode = Defined(limitMode, nameof(limitMode));
        Factory = factory;
    }

    /// <summary>
    /// Gets the limit mode of the shape limit.
    /// </summary>
    public LimitMode LimitMode { get; init; }

    /// <summary>
    /// Gets the simple shape associated with the shape limit.
    /// </summary>
    public ISimpleShape SimpleShape { get; init; }

    /// <summary>
    /// Gets the factory used to create shape limits.
    /// </summary>
    public IShapeLimitFactory Factory { get; init; }

    /// <summary>
    /// Gets the extent of the shape based on the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The extent of the shape.</returns>
    public IExtent? this[ShapeExtentCode shapeExtentCode] => SimpleShape[shapeExtentCode];

    /// <summary>
    /// Determines whether two shape limits are equal.
    /// </summary>
    /// <param name="x">The first shape limit to compare.</param>
    /// <param name="y">The second shape limit to compare.</param>
    /// <returns>True if the shape limits are equal, otherwise false.</returns>
    public bool Equals(IShapeLimit? x, IShapeLimit? y)
    {
        if (x is null && y is null) return true;

        return x is not null
            && y is not null
            && x.LimitMode == y.LimitMode
            && x.Equals(y);
    }

    /// <summary>
    /// Returns the hash code for the specified shape limit.
    /// </summary>
    /// <param name="shapeLimit">The shape limit to get the hash code for.</param>
    /// <returns>A hash code for the specified shape limit.</returns>
    public int GetHashCode([DisallowNull] IShapeLimit shapeLimit)
    {
        return HashCode.Combine(shapeLimit.LimitMode, shapeLimit.GetHashCode());
    }

    /// <summary>
    /// Gets the limit mode of the shape limit.
    /// </summary>
    /// <returns>The limit mode of the shape limit.</returns>
    public LimitMode? GetLimitMode()
    {
        return LimitMode;
    }

    /// <summary>
    /// Creates a new shape limit by copying another shape limit.
    /// </summary>
    /// <param name="other">The other shape limit to copy.</param>
    /// <returns>A new shape limit.</returns>
    public IShapeLimit GetNew(IShapeLimit other)
    {
        return Factory.CreateNew(other);
    }

    /// <summary>
    /// Creates a new shape limit with the specified simple shape and limit mode.
    /// </summary>
    /// <param name="simpleShape">The simple shape associated with the shape limit.</param>
    /// <param name="limitMode">The limit mode for the shape limit.</param>
    /// <returns>A new shape limit.</returns>
    public IShapeLimit GetShapeLimit(ISimpleShape simpleShape, LimitMode limitMode)
    {
        return Factory.Create(simpleShape, limitMode);
    }

    /// <summary>
    /// Creates a new shape limit with the specified limit mode and shape components.
    /// </summary>
    /// <param name="limitMode">The limit mode for the shape limit.</param>
    /// <param name="shapeComponents">The shape components associated with the shape limit.</param>
    /// <returns>A new shape limit.</returns>
    public IShapeLimit? GetShapeLimit(LimitMode limitMode, params IShapeComponent[] shapeComponents)
    {
        return Factory.Create(limitMode, shapeComponents);
    }

    /// <summary>
    /// Determines whether the shape limit includes the specified limitable shape.
    /// </summary>
    /// <param name="limitable">The limitable shape to check.</param>
    /// <returns>True if the shape limit includes the limitable shape, otherwise false.</returns>
    public bool? Includes(IShape? limitable)
    {
        return limitable is null ?
            false
            : limitable?.FitsIn(this);
    }

    /// <summary>
    /// Gets the factory used to create simple shapes.
    /// </summary>
    /// <returns>The factory used to create simple shapes.</returns>
    public ISimpleShapeFactory GetSimpleShapeFactory()
    {
        return (ISimpleShapeFactory)SimpleShape.GetFactory();
    }

    /// <summary>
    /// Validates the specified simple shape.
    /// </summary>
    /// <param name="simpleShape">The simple shape to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="ArgumentTypeOutOfRangeException">Thrown when the simple shape is not valid.</exception>
    public void ValidateSimpleShape(ISimpleShape? simpleShape, string paramName)
    {
        IFactory factory = NullChecked(simpleShape, nameof(simpleShape)).GetFactory();

        if (GetSimpleShapeFactory().Equals(factory)) return;

        throw ArgumentTypeOutOfRangeException(paramName, simpleShape!);
    }

    /// <summary>
    /// Gets the factory used to create shape limits.
    /// </summary>
    /// <returns>The factory used to create shape limits.</returns>
    public override IShapeLimitFactory GetFactory()
    {
        return Factory;
    }

    /// <summary>
    /// Gets the base measure unit of the shape limit.
    /// </summary>
    /// <returns>The base measure unit of the shape limit.</returns>
    public override Enum GetBaseMeasureUnit()
    {
        return SimpleShape.GetBaseMeasureUnit();
    }

    /// <summary>
    /// Gets a valid shape component for the specified quantifiable object.
    /// </summary>
    /// <param name="quantifiable">The quantifiable object.</param>
    /// <returns>A valid shape component.</returns>
    public override IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable)
    {
        return SimpleShape.GetValidShapeComponent(quantifiable);
    }

    /// <summary>
    /// Gets the spread measure of the shape limit.
    /// </summary>
    /// <returns>The spread measure of the shape limit.</returns>
    public override ISpreadMeasure GetSpreadMeasure()
    {
        return SimpleShape.GetSpreadMeasure();
    }

    /// <summary>
    /// Gets the base shape of the shape limit.
    /// </summary>
    /// <returns>The base shape of the shape limit.</returns>
    public override ISimpleShape GetBaseShape()
    {
        return SimpleShape;
    }

    /// <summary>
    /// Tries to exchange the shape limit to a specified context.
    /// </summary>
    /// <param name="context">The context to exchange to.</param>
    /// <param name="exchanged">The exchanged quantifiable object.</param>
    /// <returns>True if the exchange was successful, otherwise false.</returns>
    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
        exchanged = measureUnitElements.MeasureUnit switch
        {
            AreaUnit areaUnit => exchangeTo<IArea, AreaUnit>(areaUnit),
            ExtentUnit extentUnit => SimpleShape.ExchangeTo(extentUnit),
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

    /// <summary>
    /// Compares the current shape limit with another shape.
    /// </summary>
    /// <param name="other">The shape to compare with the current shape limit.</param>
    /// <returns>An integer that indicates the relative order of the shapes being compared.</returns>
    public override int CompareTo(IShape? other)
    {
        return SimpleShape.CompareTo(other);
    }

    /// <summary>
    /// Determines if the shape limit fits within another shape based on the specified limit mode.
    /// </summary>
    /// <param name="other">The shape to check against.</param>
    /// <param name="limitMode">The limit mode to use for the check.</param>
    /// <returns>True if the shape limit fits, otherwise false.</returns>
    public override bool? FitsIn(IShape? other, LimitMode? limitMode)
    {
        return SimpleShape.FitsIn(other, limitMode);
    }

    /// <summary>
    /// Gets the shape components of the shape limit.
    /// </summary>
    /// <returns>An enumerable of shape components.</returns>
    public override IEnumerable<IShapeComponent> GetShapeComponents()
    {
        return SimpleShape.GetShapeComponents();
    }
}
