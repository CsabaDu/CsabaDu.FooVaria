namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public abstract class ShapeFactory : IShapeFactory
{
    #region Constructors
    protected ShapeFactory(ISpreadFactory spreadFactory, ITangentShapeFactory tangentShapeFactory)
    {
        SpreadFactory = NullChecked(spreadFactory, nameof(spreadFactory));
        TangentShapeFactory = NullChecked(tangentShapeFactory, nameof(tangentShapeFactory));
    }
    #endregion

    #region Properties
    public ISpreadFactory SpreadFactory { get; init; }
    public ITangentShapeFactory TangentShapeFactory { get; init; }
    #endregion

    #region Public methods
    public IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure)
    {
        return SpreadFactory.CreateBaseSpread(spreadMeasure);
    }

    public IBaseSpread CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        return SpreadFactory.CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    public IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity)
    {
        IExtent extent = (IExtent)GetMeasureFactory().Create(extentUnit, quantity);

        if (extent.GetDefaultQuantity() > 0) return (IExtent)extent;

        throw QuantityArgumentOutOfRangeException(quantity);
    }

    public IMeasureFactory GetMeasureFactory()
    {
        return SpreadFactory.MeasureFactory;
    }

    #region Virtual methods
    public virtual ISpreadFactory GetSpreadFactory()
    {
        return SpreadFactory;
    }

    public virtual ITangentShapeFactory GetTangentShapeFactory()
    {
        return TangentShapeFactory;
    }
    #endregion

    #region Abstract methods
    public abstract IBaseShape? CreateBaseShape(params IShapeComponent[] shapeComponents);
    #endregion
    #endregion

    #region Protected methods
    protected static TTangent CreateTangentShape<T, TTangent>(IShapeFactory<T, TTangent> factory, params IShapeComponent[] shapeComponents)
    where T : class, IShape, ITangentShape
    where TTangent : class, IShape, ITangentShape
    {
        return (TTangent)factory.TangentShapeFactory.CreateBaseShape(shapeComponents)!;
    }

    #region Static methods
    protected static int GetShapeComponentsCount(IShapeComponent[] shapeComponents)
    {
        return shapeComponents?.Length ?? 0;
    }

    protected static IEnumerable<IExtent>? GetShapeExtents(IShapeComponent[] shapeComponents)
    {
        if (shapeComponents.Any(x => x is not IExtent)) return null;

        IEnumerable<IExtent> shapeExtents = shapeComponents.Cast<IExtent>();

        return shapeExtents.All(x => x.GetDefaultQuantity() > 0) ?
            shapeExtents
            : null;
    }

    protected static IExtent? GetShapeExtent(IShapeComponent shapeComponent)
    {
        if (shapeComponent is not IExtent shapeExtent) return null;
        
        if (shapeExtent.GetDefaultQuantity() > 0) return shapeExtent;

        return null;
    }

    protected static TTangent CreateTangentShape<T, TTangent>(IShapeFactory<T, TTangent> factory, T shape, SideCode sideCode)
        where T : class, IShape, ITangentShape
        where TTangent : class, IShape, ITangentShape
    {
        return sideCode switch
        {
            SideCode.Outer => factory.CreateOuterTangentShape(shape),
            SideCode.Inner => factory.CreateInnerTangentShape(shape),

            _ => throw InvalidSideCodeEnumArgumentException(sideCode),
        };
    }
    #endregion
    #endregion
}
