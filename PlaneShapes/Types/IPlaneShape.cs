namespace CsabaDu.FooVaria.PlaneShapes.Types;

public interface IPlaneShape : ISimpleShape, ISurface
{
    IArea Area { get; init; }
}
