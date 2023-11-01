namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ICircularShape : ITangentShape, IRadius
    {
        IRectangularShape GetInnerTangentShape(IExtent innerTangentRectangleSide);
    }
}
