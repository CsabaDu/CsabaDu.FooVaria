using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ITangentShape : IDimensions, IBaseShape
    {
        ITangentShape GetOuterTangentShape();
        ITangentShape GetInnerTangentShape();
    }
}
