using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ITangentShape : IDimensions, IBaseShape
    {
        IShape GetOuterTangentShape();
        IShape GetInnerTangentShape();
    }
}
