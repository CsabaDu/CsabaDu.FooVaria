namespace CsabaDu.FooVaria.SimpleShapes.Types
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation, ILength, IWidth
    {
    }

    public interface IRectangularShape<out TSelf, out TTangent> : IRectangularShape, ISimpleShape<TTangent>
        where TSelf : class, ISimpleShape, IRectangularShape
        where TTangent : class, ISimpleShape, ICircularShape
    {
        TTangent GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
