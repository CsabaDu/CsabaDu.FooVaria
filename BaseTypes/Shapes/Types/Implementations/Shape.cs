namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class Shape : Spread, IShape
{
    #region Constructors
    protected Shape(IShape other) : base(other)
    {
    }

    protected Shape(IShapeFactory factory, IShape shape) : base(factory, shape)
    {
    }

    protected Shape(ISpreadFactory factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    {
    }
    #endregion

    #region Public methods
    public int GetShapeComponentCount()
    {
        return GetShapeComponents().Count();
    }

    public void ValidateShapeComponent(IQuantifiable? shapeComponent, string paramName)
    {
        if (GetValidShapeComponent(NullChecked(shapeComponent, paramName)) != null) return;

        throw ArgumentTypeOutOfRangeException(paramName, shapeComponent!);
    }

    #region Override methods
    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Append(MeasureUnitCode.ExtentUnit);
    }

    public override IShapeFactory GetFactory()
    {
        return (IShapeFactory)Factory;
    }

    public override sealed int GetHashCode()
    {
        HashCode hashCode = new();

        hashCode.Add(MeasureUnitCode);

        foreach (IShapeComponent item in GetShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }

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
    public abstract IShapeComponent? GetValidShapeComponent(IQuantifiable? shapeComponent);
    public abstract IEnumerable<IShapeComponent> GetShapeComponents();
    public abstract IShape? GetShape(params IShapeComponent[] shapeComponents);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool IsValidShapeComponentOf<T>(IShape shape, T shapeComponent) where T : class, IQuantifiable, IShapeComponent
    {
        return shape?.GetShapeComponents() is IEnumerable<T>;
    }
    #endregion
    #endregion
}
