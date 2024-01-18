using CsabaDu.FooVaria.Measurables.Enums;
using CsabaDu.FooVaria.Quantifiables.Enums;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseShape : BaseSpread, IBaseShape
{
    #region Constructors
    protected BaseShape(IBaseShape other) : base(other)
    {
    }

    protected BaseShape(IBaseShapeFactory factory, IBaseShape baseBaseShape) : base(factory, baseBaseShape)
    {
    }

    protected BaseShape(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] baseShapeComponents) : base(factory, measureUnitCode, baseShapeComponents)
    {
    }
    #endregion

    #region Public methods
    public int GetBaseShapeComponentCount()
    {
        return GetBaseShapeComponents().Count();
    }

    #region Override methods
    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Append(MeasureUnitCode.ExtentUnit);
    }

    public override IBaseShapeFactory GetFactory()
    {
        return (IBaseShapeFactory)Factory;
    }

    public override sealed int GetHashCode()
    {
        HashCode hashCode = new();

        hashCode.Add(MeasureUnitCode);

        foreach (IShapeComponent item in GetBaseShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }

    #endregion

    #region Virtual methods
    public virtual bool Equals(IBaseShape? other)
    {
        return base.Equals(other)
            && GetBaseShapeComponents().SequenceEqual(other.GetBaseShapeComponents());
    }
    #endregion

    #region Abstract methods
    public abstract int CompareTo(IBaseShape? other);
    public abstract bool? FitsIn(IBaseShape? other, LimitMode? limitMode);
    public abstract void ValidateBaseShapeComponent(IQuantifiable baseShapeComponent, string paramName);
    public abstract IQuantifiable? GetValidBaseShapeComponent(IShapeComponent baseShapeComponent);
    public abstract IEnumerable<IShapeComponent> GetBaseShapeComponents();
    public abstract IBaseShape? GetBaseBaseShape(params IShapeComponent[] baseShapeComponents);
    #endregion
    #endregion
}
