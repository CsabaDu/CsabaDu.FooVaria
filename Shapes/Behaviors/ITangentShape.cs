using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ITangentShape : IBaseShape
    {
        IShape GetTangentShape(SideCode sideCode);
    }

    public interface ITangentShape<out T> : IShape where T : class, IShape, ITangentShape
    {
        T GetOuterTangentShape();
        T GetInnerTangentShape();
    }
}
