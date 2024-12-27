namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Types.Implementations;

/// <summary>
/// Represents an abstract base class for simple shapes.
/// </summary>
public abstract class SimpleShape : Shape, ISimpleShape
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleShape"/> class by copying another simple shape.
    /// </summary>
    /// <param name="other">The other simple shape to copy.</param>
    protected SimpleShape(ISimpleShape other) : base(other, nameof(other))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleShape"/> class using a factory.
    /// </summary>
    /// <param name="factory">The factory to use for initialization.</param>
    protected SimpleShape(ISimpleShapeFactory factory) : base(factory, nameof(factory))
    {
    }
    #endregion

    #region Properties
    #region Abstract properties
    /// <summary>
    /// Gets the extent of the shape for the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The extent of the shape.</returns>
    public abstract IExtent? this[ShapeExtentCode shapeExtentCode] { get; }
    #endregion
    #endregion

    #region Public methods
    #region Static methods
    /// <summary>
    /// Gets the diagonal of a rectangular shape.
    /// </summary>
    /// <typeparam name="T">The type of the simple shape.</typeparam>
    /// <param name="simpleShape">The simple shape.</param>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The diagonal extent of the shape.</returns>
    public static IExtent GetRectangularShapeDiagonal<T>(T simpleShape, ExtentUnit extentUnit = default)
        where T : class, ISimpleShape, IRectangularShape
    {
        ValidateMeasureUnitByDefinition(extentUnit, nameof(extentUnit));

        IEnumerable<ShapeExtentCode> shapeExtentCodes = simpleShape.GetShapeExtentCodes();
        int i = 0;
        decimal quantitySquares = getDefaultQuantitySquare();

        for (i = 1; i < shapeExtentCodes.Count(); i++)
        {
            quantitySquares += getDefaultQuantitySquare();
        }

        double quantity = decimal.ToDouble(quantitySquares);
        quantity = Math.Sqrt(quantity);
        if (!IsDefaultMeasureUnit(extentUnit))
        {
            quantity /= decimal.ToDouble(GetExchangeRate(extentUnit, nameof(extentUnit)));
        }
        IExtent edge = getShapeExtent();

        return edge.GetMeasure(extentUnit, quantity);

        #region Local methods
        IExtent getShapeExtent()
        {
            return simpleShape.GetShapeExtent(shapeExtentCodes.ElementAt(i));
        }

        decimal getDefaultQuantitySquare()
        {
            IExtent shapeExtent = getShapeExtent();

            return GetDefaultQuantitySquare(shapeExtent);
        }
        #endregion
    }

    #region Abstract methods
    /// <summary>
    /// Gets the diagonal of the shape.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The diagonal extent of the shape.</returns>
    public abstract IExtent GetDiagonal(ExtentUnit extentUnit);
    #endregion
    #endregion

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Compares the current shape with another shape.
    /// </summary>
    /// <param name="other">The shape to compare with the current shape.</param>
    /// <returns>An integer that indicates the relative order of the shapes being compared.</returns>
    public override sealed int CompareTo(IShape? other)
    {
        if (other is null) return 1;

        const string paramName = nameof(other);
        ISimpleShape simpleShape = GetSimpleShape(other, paramName);

        ValidateMeasureUnitCode(simpleShape.GetMeasureUnitCode(), paramName);

        return Compare(this, simpleShape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    /// <summary>
    /// Determines whether the specified shape is equal to the current shape.
    /// </summary>
    /// <param name="other">The shape to compare with the current shape.</param>
    /// <returns>True if the specified shape is equal to the current shape, otherwise false.</returns>
    public override sealed bool Equals(IShape? other)
    {
        return other is ISimpleShape simpleShape
            && base.Equals(simpleShape);
    }

    /// <summary>
    /// Determines if the shape fits within another shape based on the specified limit mode.
    /// </summary>
    /// <param name="other">The shape to check against.</param>
    /// <param name="limitMode">The limit mode to use for the check.</param>
    /// <returns>True if the shape fits, otherwise false.</returns>
    public override sealed bool? FitsIn(IShape? other, LimitMode? limitMode)
    {
        if (other is null) return null;

        if (other is not ISimpleShape simpleShape)
        {
            simpleShape = (other.GetBaseShape() as ISimpleShape)!;
        }

        if (simpleShape?.IsExchangeableTo(GetMeasureUnitCode()) != true) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (simpleShape!.GetShapeComponentCount() != GetShapeComponentCount())
        {
            simpleShape = (ISimpleShape)(simpleShape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, simpleShape)?.FitsIn(limitMode);
    }

    /// <summary>
    /// Gets the base shape.
    /// </summary>
    /// <returns>The base shape.</returns>
    public override ISimpleShape GetBaseShape()
    {
        return this;
    }

    /// <summary>
    /// Gets the shape components of the shape.
    /// </summary>
    /// <returns>An enumerable of shape components.</returns>
    public override sealed IEnumerable<IShapeComponent> GetShapeComponents()
    {
        return GetShapeExtents();
    }

    /// <summary>
    /// Validates the quantity.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        object converted = NullChecked(quantity, paramName)
            .ToQuantity(TypeCode.Decimal)
            ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

        if ((decimal)converted > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the bulk spread factory.
    /// </summary>
    /// <returns>The bulk spread factory.</returns>
    public abstract IBulkSpreadFactory GetBulkSpreadFactory();

    /// <summary>
    /// Gets the tangent shape factory.
    /// </summary>
    /// <returns>The tangent shape factory.</returns>
    public abstract ITangentShapeFactory GetTangentShapeFactory();
    #endregion

    /// <summary>
    /// Gets the diagonal of the shape.
    /// </summary>
    /// <returns>The diagonal extent of the shape.</returns>
    public IExtent GetDiagonal()
    {
        return GetDiagonal(default);
    }

    /// <summary>
    /// Gets the dimensions of the shape.
    /// </summary>
    /// <returns>An enumerable of extents representing the dimensions of the shape.</returns>
    public IEnumerable<IExtent> GetDimensions()
    {
        return this is ICircularShape circularShape ?
            GetShapeComponents(circularShape.GetTangentShape(SideCode.Outer))!
            : GetShapeExtents();
    }

    /// <summary>
    /// Gets a simple shape with the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The simple shape with the specified extent unit.</returns>
    public ISimpleShape GetSimpleShape(ExtentUnit extentUnit)
    {
        return ExchangeTo(extentUnit) ?? throw InvalidMeasureUnitEnumArgumentException(extentUnit, nameof(extentUnit));
    }

    /// <summary>
    /// Gets a simple shape with the specified shape extents.
    /// </summary>
    /// <param name="shapeExtents">The shape extents.</param>
    /// <returns>The simple shape with the specified shape extents.</returns>
    public ISimpleShape GetSimpleShape(params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof(shapeExtents));

        return (ISimpleShape)GetShape(shapeExtents)!;
    }

    /// <summary>
    /// Gets the shape components of the specified shape.
    /// </summary>
    /// <param name="shape">The shape.</param>
    /// <returns>An enumerable of extents representing the shape components.</returns>
    public IEnumerable<IExtent> GetShapeComponents(IShape shape)
    {
        ISimpleShape simpleShape = GetSimpleShape(shape, nameof(shape));

        return simpleShape.GetShapeExtents();
    }

    /// <summary>
    /// Gets the extent of the shape for the specified shape extent code.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>The extent of the shape.</returns>
    public IExtent GetShapeExtent(ShapeExtentCode shapeExtentCode)
    {
        return this[shapeExtentCode] ?? throw InvalidShapeExtentCodeEnumArgumentException(shapeExtentCode);
    }

    /// <summary>
    /// Gets the extents of the shape.
    /// </summary>
    /// <returns>An enumerable of extents representing the shape extents.</returns>
    public IEnumerable<IExtent> GetShapeExtents()
    {
        foreach (ShapeExtentCode item in GetShapeExtentCodes())
        {
            yield return this[item]!;
        }
    }

    /// <summary>
    /// Gets the shape extent codes.
    /// </summary>
    /// <returns>An enumerable of shape extent codes.</returns>
    public IEnumerable<ShapeExtentCode> GetShapeExtentCodes()
    {
        return Enum.GetValues<ShapeExtentCode>().Where(x => this[x] is not null);
    }

    /// <summary>
    /// Gets the sorted dimensions of the shape.
    /// </summary>
    /// <returns>An enumerable of sorted extents representing the dimensions of the shape.</returns>
    public IEnumerable<IExtent> GetSortedDimensions()
    {
        return GetDimensions().OrderBy(x => x);
    }

    /// <summary>
    /// Determines whether the specified shape extent code is valid.
    /// </summary>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>True if the shape extent code is valid, otherwise false.</returns>
    public bool IsValidShapeExtentCode(ShapeExtentCode shapeExtentCode)
    {
        return GetShapeExtentCodes().Contains(shapeExtentCode);
    }

    /// <summary>
    /// Tries to get the shape extent code for the specified shape extent.
    /// </summary>
    /// <param name="shapeExtent">The shape extent.</param>
    /// <param name="shapeExtentCode">The shape extent code.</param>
    /// <returns>True if the shape extent code was found, otherwise false.</returns>
    public bool TryGetShapeExtentCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentCode? shapeExtentCode)
    {
        shapeExtentCode = null;

        if (shapeExtent is not null || !GetShapeExtents().Contains(shapeExtent)) return false;

        shapeExtentCode = GetShapeExtentCodes().FirstOrDefault(x => this[x] == shapeExtent);

        return true;
    }

    /// <summary>
    /// Validates the shape extent.
    /// </summary>
    /// <param name="shapeExtent">The shape extent to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    public void ValidateShapeExtent(IExtent? shapeExtent, string paramName)
    {
        decimal quantity = NullChecked(shapeExtent, paramName).GetDecimalQuantity();

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    /// <summary>
    /// Validates the shape extent count.
    /// </summary>
    /// <param name="count">The count to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    public void ValidateShapeExtentCount(int count, string paramName)
    {
        if (count == GetShapeComponentCount()) return;

        throw QuantityArgumentOutOfRangeException(paramName, count);
    }

    /// <summary>
    /// Validates the shape extents.
    /// </summary>
    /// <param name="shapeExtents">The shape extents to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName)
    {
        int count = NullChecked(shapeExtents, paramName).Count();

        ValidateShapeExtentCount(count, paramName);

        foreach (IExtent item in shapeExtents)
        {
            ValidateShapeExtent(item, paramName);
        }
    }
    #endregion

    #region Protected methods
    /// <summary>
    /// Gets the spread measure for the specified shape extents.
    /// </summary>
    /// <param name="shapeExtents">The shape extents.</param>
    /// <returns>The spread measure.</returns>
    protected ISpreadMeasure GetSpreadMeasure(IExtent[] shapeExtents)
    {
        return GetBulkSpreadFactory().Create(shapeExtents).GetSpreadMeasure();
    }
    #endregion

    #region Private methods
    #region Static methods
    /// <summary>
    /// Compares the current simple shape with another simple shape.
    /// </summary>
    /// <param name="simpleShape">The current simple shape.</param>
    /// <param name="other">The other simple shape to compare with.</param>
    /// <returns>An integer that indicates the relative order of the simple shapes being compared.</returns>
    private static int? Compare(SimpleShape simpleShape, ISimpleShape? other)
    {
        if (other is null) return null;

        int? comparison = 0;

        foreach (ShapeExtentCode item in simpleShape.GetShapeExtentCodes())
        {
            if (!comparison.HasValue) return null;

            int itemComparison = simpleShape[item]!.CompareTo(other[item]);
            comparison = comparison.Value.CompareToComparison(itemComparison);
        }

        return comparison;
    }

    /// <summary>
    /// Exchanges the current simple shape to the specified extent unit.
    /// </summary>
    /// <param name="extentUnit">The extent unit.</param>
    /// <returns>The exchanged simple shape.</returns>
    public ISimpleShape? ExchangeTo(ExtentUnit extentUnit)
    {
        if (!IsValidMeasureUnit(extentUnit)) return null;

        IEnumerable<IExtent> shapeExtents = GetShapeExtents();
        shapeExtents = shapeExtents.Select(x => x.ExchangeTo(extentUnit)!);

        return (ISimpleShape?)GetShape(shapeExtents.ToArray());
    }

    /// <summary>
    /// Gets the simple shape for the specified shape.
    /// </summary>
    /// <param name="shape">The shape.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <returns>The simple shape.</returns>
    private static ISimpleShape GetSimpleShape(IShape shape, string paramName)
    {
        if (NullChecked(shape, paramName).GetBaseShape() is ISimpleShape simpleShape) return simpleShape;

        throw ArgumentTypeOutOfRangeException(paramName, shape);
    }

    /// <summary>
    /// Determines whether the specified shape components are equal.
    /// </summary>
    /// <param name="x">The first shape component to compare.</param>
    /// <param name="y">The second shape component to compare.</param>
    /// <returns>True if the specified shape components are equal, otherwise false.</returns>
    public bool Equals(IShapeComponent? x, IShapeComponent? y)
    {
        return Equals<IShapeComponent>(x, y);
    }

    /// <summary>
    /// Returns the hash code for the specified shape component.
    /// </summary>
    /// <param name="shapeComponent">The shape component to get the hash code for.</param>
    /// <returns>A hash code for the specified shape component.</returns>
    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return GetHashCode<IShapeComponent>(shapeComponent);
    }
    #endregion
    #endregion
}
