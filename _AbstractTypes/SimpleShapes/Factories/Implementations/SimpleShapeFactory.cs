using CsabaDu.FooVaria.BaseTypes.BaseMeasures.Factories;

namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Factories.Implementations;

public abstract class SimpleShapeFactory : ISimpleShapeFactory
{
    #region Public methods
    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        return GetBulkSpreadFactory().CreateSpread(spreadMeasure);
    }

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        return GetBulkSpreadFactory().CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    public IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity)
    {
        IMeasureFactory factory = GetMeasureFactory();
        IMeasure measure = factory.Create(extentUnit, quantity);

        if (measure.GetDefaultQuantity() > 0) return (IExtent)measure;

        throw QuantityArgumentOutOfRangeException(quantity);
    }

    public ISpreadMeasure? CreateSpreadMeasure(Enum measureUnit, double quantity)
    {
        return GetBulkSpreadFactory().CreateSpreadMeasure(measureUnit, quantity);
    }

    public IMeasureFactory GetMeasureFactory()
    {
        return GetBulkSpreadFactory().MeasureFactory;
    }

    #region Abstract methods
    public abstract IShape? CreateShape(params IShapeComponent[] shapeComponents);
    public abstract IBulkSpreadFactory GetBulkSpreadFactory();
    public abstract ITangentShapeFactory GetTangentShapeFactory();
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static TTangent CreateTangentShape<T, TTangent>(ISimpleShapeFactory<T, TTangent> factory, params IShapeComponent[] shapeComponents)
    where T : class, ISimpleShape, ITangentShape
    where TTangent : class, ISimpleShape, ITangentShape
    {
        return (TTangent)factory.GetTangentShapeFactory().CreateShape(shapeComponents)!;
    }

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
