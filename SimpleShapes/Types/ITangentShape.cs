namespace CsabaDu.FooVaria.SimpleShapes.Types;

public interface ITangentShape : IBaseShape
{
    ISimpleShape GetTangentShape(SideCode sideCode);
}
