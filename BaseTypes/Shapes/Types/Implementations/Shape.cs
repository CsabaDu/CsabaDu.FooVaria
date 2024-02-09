namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class Shape : BaseSpread, IShape
{
    #region Constructors
    protected Shape(IShape other) : base(other)
    {
    }

    protected Shape(IShapeFactory factory, IShape shape) : base(factory, shape)
    {
    }

    protected Shape(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    {
    }
    #endregion

    #region Public methods
    public int GetSimpleShapeComponentCount()
    {
        return GetShapeComponents().Count();
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

        foreach (ISimpleShapeComponent item in GetShapeComponents())
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
    public abstract void ValidateSimpleShapeComponent(IQuantifiable simpleShapeComponent, string paramName);
    public abstract ISimpleShapeComponent? GetValidSimpleShapeComponent(IQuantifiable? simpleShapeComponent);
    public abstract IEnumerable<ISimpleShapeComponent> GetShapeComponents();
    public abstract IShape? GetShape(params ISimpleShapeComponent[] shapeComponents);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool IsValidSimpleShapeComponentOf<T>(IShape shape, T simpleShapeComponent) where T : class, IQuantifiable, ISimpleShapeComponent
    {
        return shape?.GetShapeComponents() is IEnumerable<T>;
    }
    #endregion
    #endregion
}
