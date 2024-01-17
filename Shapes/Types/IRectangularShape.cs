namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation, ILength, IWidth
    {
    }

    public interface IRectangularShape<out TSelf, out TTangent> : IRectangularShape, IShape<TTangent>
        where TSelf : class, IShape, IRectangularShape
        where TTangent : class, IShape, ICircularShape
    {
        TTangent GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
