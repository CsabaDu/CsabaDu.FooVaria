namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types.Implementations;

public abstract class ComplexShape : Shape, IComplexShape
{
    protected ComplexShape(IComplexShape other) : base(other)
    {
        BaseShape = NullChecked(other, nameof(other)).BaseShape;
    }

    protected ComplexShape(IComplexShapeFactory factory, ISimpleShape baseShape) : base(factory)
    {
        BaseShape = NullChecked(baseShape, nameof(baseShape));
    }

    //protected ComplexShape(IComplexShapeFactory factory, params IExtent[] shapeExtents) : base(factory)
    //{
    //    BaseShape = getBaseShape();

    //    #region Local methods
    //    ISimpleShape getBaseShape()
    //    {
    //        string paramName = nameof(shapeExtents);

    //        foreach (IExtent item in shapeExtents)
    //        {
    //            double quantity = item.GetQuantity();

    //            if (quantity <= 0) throw QuantityArgumentOutOfRangeException(paramName, quantity);
    //        }

    //        ISimpleShapeFactory simpleShapeFactory = GetSimpleShapeFactory();

    //        IShape? shape = simpleShapeFactory.CreateShape(shapeExtents);

    //        if (shape is ISimpleShape baseShape) return baseShape;

    //        throw CountArgumentOutOfRangeException(shapeExtents.Length, paramName);

    //    }
    //    #endregion
    //}

    public ISimpleShape BaseShape { get; init; }

    public ISimpleShapeFactory GetSimpleShapeFactory()
    {
        return GetFactory().SimpleShapeFactory;
    }

    public override IComplexShapeFactory GetFactory()
    {
        return (IComplexShapeFactory)Factory;
    }
}
