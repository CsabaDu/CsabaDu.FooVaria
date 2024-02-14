namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors
{
    public interface IHorizontalRotation
    {
        IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode);
    }

    public interface IHorizontalRotation<TSelf> : IHorizontalRotation
        where TSelf : class, ISimpleShape, IRectangularShape
    {
        TSelf RotateHorizontally();
    }
}
