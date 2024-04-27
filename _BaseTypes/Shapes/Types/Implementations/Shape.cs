namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class Shape(IRootObject rootObject, string paramName) : Spread(rootObject, paramName), IShape
{
    #region Public methods
    #region Override methods
    public override int GetHashCode()
    {
        HashCode hashCode = new();

        hashCode.Add(base.GetHashCode());

        foreach (IShapeComponent item in GetShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }

    #region Sealed methods
    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        return limiter is IShape shape ?
            FitsIn(shape, limiter.GetLimitMode())
            : base.FitsIn(limiter);
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
            && GetBaseShape().GetShapeComponents().SequenceEqual(other.GetBaseShape().GetShapeComponents());
    }
    #endregion

    #region Abstract methods
    public abstract int CompareTo(IShape? other);
    public abstract bool? FitsIn(IShape? other, LimitMode? limitMode);
    public abstract IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable);
    public abstract IEnumerable<IShapeComponent> GetShapeComponents();
    public abstract IShape GetBaseShape();
    #endregion

    public bool Equals(IShape? x, IShape? y)
    {
        //if (x is null != y is null) return false;

        return x is not null
            && y is not null
            || y is null
            || x?.GetBaseShape().Equals(y.GetBaseShape()) == true;
    }

    public int GetHashCode([DisallowNull] IShape shape)
    {
        return shape.GetBaseShape().GetHashCode();
    }

    public IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetMeasureUnitCode();

        IShape baseShape = GetBaseShape();
        IEnumerable<IShapeComponent> shapeComponents = baseShape.GetShapeComponents();
        IShapeComponent first = shapeComponents.First();

        yield return (first as IQuantifiable)!.GetMeasureUnitCode();
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

    public void ValidateShapeComponent(IQuantifiable? quantifiable, string paramName)
    {
        if (GetValidShapeComponent(NullChecked(quantifiable, paramName)) is not null) return;

        throw ArgumentTypeOutOfRangeException(paramName, quantifiable!);
    }
    #endregion
}
