namespace CsabaDu.FooVaria.SimpleShapes.Types;

public interface ITangentShape : IShape
{
    ISimpleShape GetTangentShape(SideCode sideCode);
}
