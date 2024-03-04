namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class Shape : Spread, IShape
{
    #region Constructors
    protected Shape(IShape other) : base(other)
    {
    }

    protected Shape(IShapeFactory factory) : base(factory)
    {
    }

    //protected Shape(ISpreadFactory factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    //{
    //}
    #endregion

    #region Public methods
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
        if (GetValidShapeComponent(NullChecked(shapeComponent, paramName)) != null) return;

        throw ArgumentTypeOutOfRangeException(paramName, shapeComponent!);
    }

    #region Override methods
    public IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetMeasureUnitCode();
        yield return MeasureUnitCode.ExtentUnit;
    }

    public override IShapeFactory GetFactory()
    {
        return (IShapeFactory)Factory;
    }

    #region Sealed methods
    public override sealed int CompareTo(IQuantifiable? other)
    {
        if (other is IShape shape) return CompareTo(shape);

        return base.CompareTo(other);
    }

    public override sealed bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        if (other is IShape shape) return FitsIn(shape, limitMode);

        return base.FitsIn(other, limitMode);
    }

    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is IShape shape) return FitsIn(shape, limiter.GetLimitMode());

        return base.FitsIn(limiter);
    }

    public override sealed int GetHashCode()
    {
        HashCode hashCode = new();

        hashCode.Add(GetMeasureUnitCode());

        foreach (IShapeComponent item in GetShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
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
    public abstract IShapeComponent? GetValidShapeComponent(IBaseQuantifiable? shapeComponent);
    public abstract IEnumerable<IShapeComponent> GetShapeComponents();
    public abstract IShape GetShape();
    public abstract IShape? GetShape(params IShapeComponent[] shapeComponents);
    #endregion
    #endregion
}
