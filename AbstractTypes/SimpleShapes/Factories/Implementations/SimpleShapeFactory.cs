namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Factories.Implementations;

public abstract class SimpleShapeFactory(IBulkSpreadFactory bulkSpreadFactory, ITangentShapeFactory tangentShapeFactory) : ISimpleShapeFactory
{
    #region Properties
    public IBulkSpreadFactory BulkSpreadFactory { get; init; } = NullChecked(bulkSpreadFactory, nameof(bulkSpreadFactory));
    public ITangentShapeFactory TangentShapeFactory { get; init; } = NullChecked(tangentShapeFactory, nameof(tangentShapeFactory));
    #endregion

    #region Public methods
    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        return BulkSpreadFactory.CreateSpread(spreadMeasure);
    }

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        return BulkSpreadFactory.CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    public IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity)
    {
        IExtent extent = (IExtent)GetMeasureFactory().Create(extentUnit, quantity);

        if (extent.GetDefaultQuantity() > 0) return (IExtent)extent;

        throw QuantityArgumentOutOfRangeException(quantity);
    }

    public IMeasureFactory GetMeasureFactory()
    {
        return BulkSpreadFactory.MeasureFactory;
    }

    #region Virtual methods
    public virtual IBulkSpreadFactory GetBulkSpreadFactory()
    {
        return BulkSpreadFactory;
    }

    public virtual ITangentShapeFactory GetTangentShapeFactory()
    {
        return TangentShapeFactory;
    }
    #endregion

    #region Abstract methods
    public abstract IShape? CreateShape(params IShapeComponent[] shapeComponents);
    #endregion
    #endregion

    #region Protected methods
    protected static TTangent CreateTangentShape<T, TTangent>(ISimpleShapeFactory<T, TTangent> factory, params IShapeComponent[] shapeComponents)
    where T : class, ISimpleShape, ITangentShape
    where TTangent : class, ISimpleShape, ITangentShape
    {
        return (TTangent)factory.TangentShapeFactory.CreateShape(shapeComponents)!;
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

    protected static TTangent CreateTangentShape<T, TTangent>(ISimpleShapeFactory<T, TTangent> factory, T simpleShape, SideCode sideCode)
        where T : class, ISimpleShape, ITangentShape
        where TTangent : class, ISimpleShape, ITangentShape
    {
        return sideCode switch
        {
            SideCode.Outer => factory.CreateOuterTangentShape(simpleShape),
            SideCode.Inner => factory.CreateInnerTangentShape(simpleShape),

            _ => throw InvalidSideCodeEnumArgumentException(sideCode),
        };
    }
    #endregion
    #endregion
}
