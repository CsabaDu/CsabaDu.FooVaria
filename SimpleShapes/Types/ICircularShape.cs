namespace CsabaDu.FooVaria.SimpleShapes.Types
{
    public interface ICircularShape : ITangentShape, IRadius
    {
    }

    public interface ICircularShape<out TSelf, out TTangent> : ICircularShape, ISimpleShape<TTangent>
        where TSelf : class, ISimpleShape, ICircularShape
        where TTangent : class, ISimpleShape, IRectangularShape
    {
        TTangent GetInnerTangentShape(IExtent innerTangentRectangleSide);
    }
}
