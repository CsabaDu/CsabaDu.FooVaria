using CsabaDu.FooVaria.BaseTypes.Common.Enums;

namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types.Implementations;

public abstract class ComplexShape : Shape, IComplexShape
{
    protected ComplexShape(IComplexShape other) : base(other, nameof(other))
    {
    }

    protected ComplexShape(IComplexShapeFactory factory) : base(factory, nameof(factory))
    {
    }

    public abstract ISimpleShape GetSimpleShape();
    public abstract IShape GetTangentShape(SideCode sideCode);

    public override sealed ISimpleShape GetBaseShape()
    {
        return GetSimpleShape();
    }
}
