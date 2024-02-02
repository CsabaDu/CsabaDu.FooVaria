namespace CsabaDu.FooVaria.PlaneShapes.Types;

public interface IPlaneShape : IShape, ISurface
{
    IArea Area { get; init; }
}
