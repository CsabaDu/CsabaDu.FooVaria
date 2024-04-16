namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class Shape(IRootObject rootObject, string paramName) : Spread(rootObject, paramName), IShape
{
    #region Public methods
    #region Override methods
    #region Sealed methods
    public override sealed int CompareTo(IQuantifiable? other)
    {
        return other is IShape shape ?
            GetShape().CompareTo(shape.GetShape())
            : base.CompareTo(other);
    }

    public override sealed bool Equals(IQuantifiable? other)
    {
        return other is IShape shape ?
            GetShape().Equals(shape.GetShape())
            : base.Equals(other);
    }

    public override sealed bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        return other is IShape shape ?
            GetShape().FitsIn(shape.GetShape(), limitMode)
            : base.FitsIn(other, limitMode);
    }

    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        return limiter is IShape shape ?
            GetShape().FitsIn(shape.GetShape(), limiter.GetLimitMode())
            : base.FitsIn(limiter);
    }

    public override sealed int GetHashCode()
    {
        HashCode hashCode = new();

        hashCode.Add(base.GetHashCode());

        foreach (IShapeComponent item in GetShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }

    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    #endregion
    #endregion

    #region Virtual methods
    public virtual bool Equals(IShape? other)
    {
        return base.Equals(other)
            && GetShapeComponents().SequenceEqual(other.GetShapeComponents());
    }
    #endregion

    #region Abstract methods
    public abstract int CompareTo(IShape? other);
    public abstract bool? FitsIn(IShape? other, LimitMode? limitMode);
    public abstract IShapeComponent? GetValidShapeComponent(IBaseQuantifiable? baseQuantifiable);
    public abstract IEnumerable<IShapeComponent> GetShapeComponents();
    public abstract IShape GetShape();
    #endregion

    public IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetMeasureUnitCode();
        yield return GetValidShapeComponent(this)!.GetMeasureUnitCode();
    }

    public IShape? GetShape(params IShapeComponent[] shapeComponents)
    {
        IShapeFactory factory = (IShapeFactory)GetFactory();

        return factory.CreateShape(shapeComponents);
    }

    public int GetShapeComponentCount()
    {
        return GetShapeComponents().Count();
    }

    public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return IsValidMeasureUnitCode(this, measureUnitCode);
    }

    public void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        ValidateMeasureUnitCodes(this, baseQuantifiable, paramName);
    }

    public void ValidateShapeComponent(IBaseQuantifiable? shapeComponent, string paramName)
    {
        if (GetValidShapeComponent(NullChecked(shapeComponent, paramName)) is not null) return;

        throw ArgumentTypeOutOfRangeException(paramName, shapeComponent!);
    }
    #endregion
}
