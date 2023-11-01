using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IDryBody : IBaseFace, IHeight, IShape, IBody, IProjection
    {
        IVolume Volume { get; }
        IExtent Height { get; init; }
        IPlaneShape BaseFace { get; init; }

        IDryBody GetDryBody(IPlaneShape baseFace, IExtent height);
    }
}
