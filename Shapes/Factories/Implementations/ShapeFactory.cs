using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations;

public abstract class ShapeFactory : IShapeFactory
{
    private protected ShapeFactory(ISpreadFactory spreadFactory, ITangentShapeFactory tangentShapeFactory)
    {
        SpreadFactory = NullChecked(spreadFactory, nameof(spreadFactory));
        TangentShapeFactory = NullChecked(tangentShapeFactory, nameof(tangentShapeFactory));
    }

    public ISpreadFactory SpreadFactory { get; init; }
    public ITangentShapeFactory TangentShapeFactory { get; init; }

    public abstract IBaseSpread Create(ISpreadMeasure spreadMeasure);
    public abstract IShape Create(params IExtent[] shapeExtents);

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

    public abstract int GetShapeComponentCount();

    public abstract ISpreadFactory GetSpreadFactory();
    //{
    //    return SpreadFactory;
    //}

    public abstract ITangentShapeFactory GetTangentShapeFactory();
    //{
    //    return TangentShapeFactory;
    //}
}
