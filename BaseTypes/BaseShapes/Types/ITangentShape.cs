namespace CsabaDu.FooVaria.BaseTypes.BaseShapes.Types;

public interface ITangentShape : IBaseShape
{
    IBaseShape GetTangentShape(SideCode sideCode);
}
