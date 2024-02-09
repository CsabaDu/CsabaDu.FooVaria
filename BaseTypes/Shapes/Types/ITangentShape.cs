namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface ITangentShape : IShape
{
    IShape GetTangentShape(SideCode sideCode);
}
