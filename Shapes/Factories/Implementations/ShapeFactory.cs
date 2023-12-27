﻿using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public abstract class ShapeFactory : IShapeFactory
{
    #region Constructors
    private protected ShapeFactory(ISpreadFactory spreadFactory, ITangentShapeFactory tangentShapeFactory)
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


    public IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity)
    {
        IRateComponent extent = GetMeasureFactory().Create(extentUnit, quantity);

        if (extent.DefaultQuantity > 0) return (IExtent)extent;

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
    #region Static methods
    protected static int GetShapeComponentsCount(IShapeComponent[] shapeComponents)
    {
        return shapeComponents?.Length ?? 0;
    }

    protected static IEnumerable<IExtent>? GetShapeExtents(IShapeComponent[] shapeComponents)
    {
        if (shapeComponents.Any(x => x is not IExtent)) return null;

        IEnumerable<IExtent> shapeExtents = shapeComponents.Cast<IExtent>();

        return shapeExtents.All(x => x.DefaultQuantity > 0) ?
            shapeExtents
            : null;
    }

    protected static IExtent? GetShapeExtent(IShapeComponent shapeComponent)
    {
        if (shapeComponent is not IExtent shapeExtent) return null;
        
        if (shapeExtent.DefaultQuantity > 0) return shapeExtent;

        return null;
    }
    #endregion
    #endregion
}
