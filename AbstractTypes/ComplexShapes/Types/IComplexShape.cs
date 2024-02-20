namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types;

public interface IComplexShape : IShape
{
    ISimpleShape BaseShape { get; init; }

    ISimpleShapeFactory GetSimpleShapeFactory();
}
