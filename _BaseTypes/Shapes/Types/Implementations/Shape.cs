namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

/// <summary>
/// Represents an abstract base class for shapes.
/// </summary>
/// <param name="rootObject">The root object.</param>
/// <param name="paramName">The parameter name.</param>
public abstract class Shape(IRootObject rootObject, string paramName) : Spread(rootObject, paramName), IShape
{
    #region Public methods
    #region Override methods
    #region Sealed methods

    /// <summary>
    /// Determines if the shape fits within the specified limiter.
    /// </summary>
    /// <param name="limiter">The limiter to check against.</param>
    /// <returns>True if the shape fits, otherwise false.</returns>
    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        return limiter is IShape shape ?
            FitsIn(shape, limiter.GetLimitMode())
            : base.FitsIn(limiter);
    }

    /// <summary>
    /// Checks if the shape has the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to check.</param>
    /// <returns>True if the shape has the measure unit code, otherwise false.</returns>
    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    /// <summary>
    /// Validates the measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        base.ValidateMeasureUnit(measureUnit, paramName);
    }
    #endregion

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        HashCode hashCode = new();

        hashCode.Add(GetMeasureUnitCode());
        hashCode.Add(GetDefaultQuantity());

        foreach (IShapeComponent item in GetShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }
    #endregion

    #region Virtual methods

    /// <summary>
    /// Determines whether the specified shape is equal to the current shape.
    /// </summary>
    /// <param name="other">The shape to compare with the current shape.</param>
    /// <returns>True if the specified shape is equal to the current shape, otherwise false.</returns>
    public virtual bool Equals(IShape? other)
    {
        return base.Equals(other)
            && GetShapeComponents().SequenceEqual(other.GetShapeComponents());
    }
    #endregion

    #region Abstract methods

    /// <summary>
    /// Compares the current shape with another shape.
    /// </summary>
    /// <param name="other">The shape to compare with the current shape.</param>
    /// <returns>An integer that indicates the relative order of the shapes being compared.</returns>
    public abstract int CompareTo(IShape? other);

    /// <summary>
    /// Determines if the shape fits within another shape based on the specified limit mode.
    /// </summary>
    /// <param name="other">The shape to check against.</param>
    /// <param name="limitMode">The limit mode to use for the check.</param>
    /// <returns>True if the shape fits, otherwise false.</returns>
    public abstract bool? FitsIn(IShape? other, LimitMode? limitMode);

    /// <summary>
    /// Gets a valid shape component for the specified quantifiable object.
    /// </summary>
    /// <param name="quantifiable">The quantifiable object.</param>
    /// <returns>A valid shape component.</returns>
    public abstract IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable);

    /// <summary>
    /// Gets the shape components of the shape.
    /// </summary>
    /// <returns>An enumerable of shape components.</returns>
    public abstract IEnumerable<IShapeComponent> GetShapeComponents();

    /// <summary>
    /// Gets the base shape.
    /// </summary>
    /// <returns>The base shape.</returns>
    public abstract IShape GetBaseShape();
    #endregion

    /// <summary>
    /// Determines whether the specified shapes are equal.
    /// </summary>
    /// <param name="x">The first shape to compare.</param>
    /// <param name="y">The second shape to compare.</param>
    /// <returns>True if the specified shapes are equal, otherwise false.</returns>
    public bool Equals(IShape? x, IShape? y)
    {
        return Equals<IShape>(x, y);
    }

    /// <summary>
    /// Gets the base shape components.
    /// </summary>
    /// <returns>An enumerable of base shape components.</returns>
    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        return GetBaseShape().GetShapeComponents();
    }

    /// <summary>
    /// Returns the hash code for the specified shape.
    /// </summary>
    /// <param name="shape">The shape to get the hash code for.</param>
    /// <returns>A hash code for the specified shape.</returns>
    public int GetHashCode([DisallowNull] IShape shape)
    {
        return GetHashCode<IShape>(shape);
    }

    /// <summary>
    /// Gets the measure unit codes for the shape.
    /// </summary>
    /// <returns>An enumerable of measure unit codes.</returns>
    public IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return [GetMeasureUnitCode(), GetBaseShapeComponents().First().GetMeasureUnitCode()];
    }

    /// <summary>
    /// Gets a shape with the specified shape components.
    /// </summary>
    /// <param name="shapeComponents">The shape components.</param>
    /// <returns>A shape with the specified shape components.</returns>
    public IShape? GetShape(params IShapeComponent[] shapeComponents)
    {
        IShapeFactory factory = (IShapeFactory)GetFactory();

        return factory.CreateShape(shapeComponents);
    }

    /// <summary>
    /// Gets the count of shape components.
    /// </summary>
    /// <returns>The count of shape components.</returns>
    public int GetShapeComponentCount()
    {
        return GetShapeComponents().Count();
    }

    /// <summary>
    /// Validates the measure unit codes.
    /// </summary>
    /// <param name="measureUnitCodes">The measure unit codes to validate.</param>
    /// <param name="paramName">The parameter name.</param>
    public void ValidateMeasureUnitCodes(IMeasureUnitCodes? measureUnitCodes, string paramName)
    {
        if (NullChecked(measureUnitCodes, paramName) is IShape shape)
        {
            ValidateMeasureUnitCodes(this, shape, paramName);
        }
        else
        {
            throw ArgumentTypeOutOfRangeException(paramName, measureUnitCodes!);
        }
    }

    /// <summary>
    /// Validates the shape component.
    /// </summary>
    /// <param name="quantifiable">The quantifiable object.</param>
    /// <param name="paramName">The parameter name.</param>
    public void ValidateShapeComponent(IQuantifiable? quantifiable, string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(quantifiable, paramName).GetMeasureUnitCode();

        if (quantifiable is not IShapeComponent)
        {
            throw ArgumentTypeOutOfRangeException(paramName, quantifiable!);
        }

        ValidateMeasureUnitCode(measureUnitCode, paramName);
    }
    #endregion
}
