namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface ITangentShape : IShape
{
    IShape GetTangentShape(SideCode sideCode);

    #region Default implementations
    public sealed IShape GetTangentShape()
    {
        return GetTangentShape(SideCode.Outer);
    }
    #endregion
}
