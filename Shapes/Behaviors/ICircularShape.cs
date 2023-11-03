using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ICircularShape : ITangentShape, IRadius
    {
    }

    public interface ICircularShape<T, U> : ICircularShape, ITangentShape<U> where T : class, IShape, ITangentShape where U : class, IShape, ITangentShape
    {
        U GetInnerTangentShape(IExtent innerTangentRectangleSide);
    }

}
