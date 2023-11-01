namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation
    {
        ICircularShape GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
