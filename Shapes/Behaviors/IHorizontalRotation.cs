namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IHorizontalRotation
    {
        IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode);
    }

    public interface IHorizontalRotation<TSelf> : IHorizontalRotation
        where TSelf : class, IShape, IRectangularShape
    {
        TSelf RotateHorizontally();
    }
}
