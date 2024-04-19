namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types;

public interface IComplexShape : IShape, ITangentShape
{
    ISimpleShape GetSimpleShape();
}
