using CsabaDu.FooVaria.Measurables.Enums;

namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

public abstract class ComplexShape : Shape, IComplexShape
{
    protected ComplexShape(IShape other) : base(other)
    {
    }

    protected ComplexShape(IShapeFactory factory, IShape baseShape) : base(factory, baseShape)
    {
    }

    protected ComplexShape(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    {
    }
}
