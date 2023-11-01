using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICircle : IPlaneShape, ICircularShape
    {
        IExtent Radius { get; init; }
    }
}
