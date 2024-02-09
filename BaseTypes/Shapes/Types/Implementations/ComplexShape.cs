namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class ComplexSimpleShape : Shape, IComplexSimpleShape
{
    protected ComplexSimpleShape(IShape other) : base(other)
    {
    }

    protected ComplexSimpleShape(IShapeFactory factory, IShape shape) : base(factory, shape)
    {
    }

    protected ComplexSimpleShape(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    {
    }
}
