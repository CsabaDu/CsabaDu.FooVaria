namespace CsabaDu.FooVaria.Shapes.Types;

public interface ITangentShape : IBaseShape
{
    IShape GetTangentShape(SideCode sideCode);
}
