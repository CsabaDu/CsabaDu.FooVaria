namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface ITangentShape : IShape
{
    IShape GetTangentShape(SideCode sideCode);

    public sealed IShape GetTangentShape()
    {
        return GetTangentShape(SideCode.Outer);
    }
}
