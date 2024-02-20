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
