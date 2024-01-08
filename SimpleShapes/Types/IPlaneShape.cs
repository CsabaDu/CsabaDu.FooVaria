namespace CsabaDu.FooVaria.SimpleShapes.Types;

public interface IPlaneShape : ISimpleShape, ISurface
{
    IArea Area { get; init; }
}
