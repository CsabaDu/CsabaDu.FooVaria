namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseShape : BaseSpread, IBaseShape
{
    #region Constructors
    protected BaseShape(IBaseShape other) : base(other)
    {
    }

    protected BaseShape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
    {
    }

    protected BaseShape(IBaseSpreadFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitTypeCode, shapeComponents)
    {
    }
    #endregion

    #region Public methods
    public int GetShapeComponentCount()
    {
        return GetShapeComponents().Count();
    }

    #region Override methods
    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return base.GetMeasureUnitTypeCodes().Append(MeasureUnitTypeCode.ExtentUnit);
    }

    public override IBaseShapeFactory GetFactory()
    {
        return (IBaseShapeFactory)Factory;
    }
    #endregion

    #region Virtual methods
    public virtual bool Equals(IBaseShape? other)
    {
        return base.Equals(other)
            && GetShapeComponents().SequenceEqual(other.GetShapeComponents());
    }
    #endregion

    #region Abstract methods
    public abstract int CompareTo(IBaseShape? other);
    public abstract bool? FitsIn(IBaseShape? other, LimitMode? limitMode);
    public abstract void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
    public abstract IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);
    public abstract IEnumerable<IShapeComponent> GetShapeComponents();
    public abstract IBaseShape? GetBaseShape(params IShapeComponent[] shapeComponents);
    #endregion
    #endregion
}
