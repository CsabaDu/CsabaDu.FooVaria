//namespace CsabaDu.FooVaria.Common.Types.Implementations;

//public abstract class BaseShape : BaseSpread, IShape
//{
//    #region Constructors
//    protected BaseShape(IShape other) : base(other)
//    {
//    }

//    protected BaseShape(IShapeFactory factory, IShape baseShape) : base(factory, baseShape)
//    {
//    }

//    protected BaseShape(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
//    {
//    }
//    #endregion

//    #region Public methods
//    public int GetShapeComponentCount()
//    {
//        return GetShapeComponents().Count();
//    }

//    #region Override methods
//    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
//    {
//        return base.GetMeasureUnitCodes().Append(MeasureUnitCode.ExtentUnit);
//    }

//    public override IShapeFactory GetFactory()
//    {
//        return (IShapeFactory)Factory;
//    }

//    public override sealed int GetHashCode()
//    {
//        HashCode hashCode = new();

//        hashCode.Add(MeasureUnitCode);

//        foreach (IShapeComponent item in GetShapeComponents())
//        {
//            hashCode.Add(item);
//        }

//        return hashCode.ToHashCode();
//    }

//    #endregion

//    #region Virtual methods
//    public virtual bool Equals(IShape? other)
//    {
//        return base.Equals(other)
//            && GetShapeComponents().SequenceEqual(other.GetShapeComponents());
//    }
//    #endregion

//    #region Abstract methods
//    public abstract int CompareTo(IShape? other);
//    public abstract bool? FitsIn(IShape? other, LimitMode? limitMode);
//    public abstract void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
//    public abstract IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);
//    public abstract IEnumerable<IShapeComponent> GetShapeComponents();
//    public abstract IShape? GetBaseShape(params IShapeComponent[] shapeComponents);
//    #endregion
//    #endregion
//}
