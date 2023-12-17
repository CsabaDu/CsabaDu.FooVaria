using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ITangentShape : IBaseShape
    {
        IShape GetTangentShape(SideCode sideCode);
    }

    public interface ITangentShape<out TSelf> : IShape where TSelf : class, IShape, ITangentShape
    {
        TSelf GetOuterTangentShape();
        TSelf GetInnerTangentShape();
    }
}
