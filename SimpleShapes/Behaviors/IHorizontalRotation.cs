namespace CsabaDu.FooVaria.SimpleShapes.Behaviors
{
    public interface IHorizontalRotation
    {
        IExtent GetComparedSimpleShapeExtent(ComparisonCode? comparisonCode);
    }

    public interface IHorizontalRotation<TSelf> : IHorizontalRotation
        where TSelf : class, ISimpleShape, IRectangularShape
    {
        TSelf RotateHorizontally();
    }
}
