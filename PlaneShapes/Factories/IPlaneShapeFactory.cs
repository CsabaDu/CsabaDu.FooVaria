namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface IPlaneShapeFactory : ISimpleShapeFactory, ISurfaceFactory
{
    IBulkSurfaceFactory BulkSurfaceFactory { get; init; }
}
