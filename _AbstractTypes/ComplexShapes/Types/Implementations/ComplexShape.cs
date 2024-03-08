namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types.Implementations;

public abstract class ComplexShape : Shape, IComplexShape
{
    protected ComplexShape(IComplexShape other) : base(other, nameof(other))
    {
    }

    protected ComplexShape(IComplexShapeFactory factory) : base(factory, nameof(factory))
    {
    }

    public abstract ISimpleShape GetBaseShape();

    public override sealed ISimpleShape GetShape()
    {
        return GetBaseShape();
    }
}
