using CsabaDu.FooVaria.Common.Behaviors;
using CsabaDu.FooVaria.RateComponents.Factories;

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
    public IBaseSpread Create(ISpreadMeasure spreadMeasure)
    {
        return SpreadFactory.Create(spreadMeasure);
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
    public abstract IBaseShape Create(params IQuantifiable[] shapeComponents);
    #endregion
    #endregion

    protected static int GetValidShapeComponentsCount(IQuantifiable[] shapeComponents)
    {
        int count = NullChecked(shapeComponents, nameof(shapeComponents)).Length;

        if (count != 0) return count;

        throw CountArgumentOutOfRangeException(count, nameof(shapeComponents));
    }

    protected static IEnumerable<IExtent> GetShapeExtents(IQuantifiable[] shapeComponents)
    {
        foreach (IQuantifiable item in shapeComponents)
        {
            if (item is not IExtent shapeExtent)
            {
                throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), item);
            }

            yield return shapeExtent;
        }
    }
}
