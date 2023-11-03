using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IHorizontalRotation
    {
        IExtent GetComparedShapeExtent(ComparisonCode comparisonCode);
    }

    public interface IHorizontalRotation<T> : IHorizontalRotation where T : class, IShape, IRectangularShape
    {
        T RotateHorizontally();
        T RotateHorizontallyWith(IRectangularShape other);
    }
}
