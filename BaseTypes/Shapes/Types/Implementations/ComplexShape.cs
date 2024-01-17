using CsabaDu.FooVaria.Measurables.Enums;

namespace CsabaDu.FooVaria.BaseShapes.Types.Implementations;

public abstract class ComplexShape : BaseShape, IComplexShape
{
    protected ComplexShape(IBaseShape other) : base(other)
    {
    }

    protected ComplexShape(IBaseShapeFactory factory, IBaseShape baseBaseShape) : base(factory, baseBaseShape)
    {
    }

    protected ComplexShape(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] baseShapeComponents) : base(factory, measureUnitCode, baseShapeComponents)
    {
    }
}
