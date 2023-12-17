using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ICircularShape : ITangentShape, IRadius
    {
    }

    public interface ICircularShape<out TSelf, out TTangent> : ICircularShape, ITangentShape<TTangent> where TSelf : class, IShape, ICircularShape where TTangent : class, IShape, IRectangularShape
    {
        TTangent GetInnerTangentShape(IExtent innerTangentRectangleSide);
    }
}
