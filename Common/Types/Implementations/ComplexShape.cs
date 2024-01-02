namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class ComplexShape : BaseShape, IComplexShape
{
    protected ComplexShape(IBaseShape other) : base(other)
    {
    }

    protected ComplexShape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
    {
    }

    protected ComplexShape(IBaseSpreadFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitTypeCode, shapeComponents)
    {
    }
}
