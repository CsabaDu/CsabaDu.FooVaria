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

    public IBaseSpread Create(ISpreadMeasure spreadMeasure)
    {
        return SpreadFactory.Create(spreadMeasure);
    }

    public abstract IBaseShape Create(params IQuantifiable[] rateComponents);

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

    public virtual ISpreadFactory GetSpreadFactory()
    {
        return SpreadFactory;
    }

    public virtual ITangentShapeFactory GetTangentShapeFactory()
    {
        return TangentShapeFactory;
    }
}
