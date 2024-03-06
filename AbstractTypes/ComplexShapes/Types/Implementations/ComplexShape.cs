namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types.Implementations;

public abstract class ComplexShape : Shape, IComplexShape
{
    protected ComplexShape(IComplexShape other) : base(other)
    {
    }

    protected ComplexShape(IComplexShapeFactory factory) : base(factory)
    {
    }

    public abstract ISimpleShape GetBaseShape();

    public override IComplexShapeFactory GetFactory()
    {
        return (IComplexShapeFactory)Factory;
    }

    public override sealed ISimpleShape GetShape()
    {
        return GetBaseShape();
    }
}
