namespace CsabaDu.FooVaria.Shapes.Types;

public interface IPlaneShape : IShape, ISurface
{
    IArea Area { get; init; }
}
