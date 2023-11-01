using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IRectangle : IPlaneShape, IRectangularShape, IHorizontalRotation<IRectangle>
    {
        IExtent Length { get; init; }
        IExtent Width { get; init; }
    }
}
