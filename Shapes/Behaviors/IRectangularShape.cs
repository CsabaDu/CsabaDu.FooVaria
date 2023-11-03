namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation, ILength, IWidth
    {
        ICircularShape GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
