using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ICircularShape : ITangentShape, IRadius
    {
    }

    public interface ICircularShape<out T, out U> : ICircularShape, ITangentShape<U> where T : class, IShape, ICircularShape where U : class, IShape, IRectangularShape
    {
        U GetInnerTangentShape(IExtent innerTangentRectangleSide);
    }
}
