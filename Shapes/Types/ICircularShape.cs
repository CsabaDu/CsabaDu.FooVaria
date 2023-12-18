namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICircularShape : ITangentShape, IRadius
    {
    }

    public interface ICircularShape<out TSelf, out TTangent> : ICircularShape, IShape<TTangent> where TSelf : class, IShape, ICircularShape where TTangent : class, IShape, IRectangularShape
    {
        TTangent GetInnerTangentShape(IExtent innerTangentRectangleSide);
    }
}
