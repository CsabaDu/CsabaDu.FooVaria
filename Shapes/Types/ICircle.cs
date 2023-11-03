using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICircle : IPlaneShape, ICircularShape<ICircle, IRectangle>
    {
        IExtent Radius { get; init; }
    }
}
